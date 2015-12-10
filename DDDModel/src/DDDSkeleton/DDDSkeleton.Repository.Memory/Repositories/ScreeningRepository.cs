using System.Collections.Generic;
using System.Linq;
using DDDSkeleton.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;
using DDDSkeleton.Repository.Memory.Database;

namespace DDDSkeleton.Repository.Memory.Repositories
{
    public class ScreeningRepository : Repository<Screening, int, DatabaseScreening>, IScreeningRepository
    {
        public ScreeningRepository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
            : base(unitOfWork, objectContextFactory)
        {
        }

        public override Screening FindBy(int id)
        {
            var databaseScreening =
                (from sc in ObjectContextFactory.Create().DatabaseScreenings
                 where sc.Id == id
                 select sc).FirstOrDefault();

            return databaseScreening != null ? ConvertToDomain(databaseScreening) : null;
        }

        public IEnumerable<Screening> FindAll()
        {
            var allDatabaseScreenings =
                (from sc in ObjectContextFactory.Create().DatabaseScreenings
                 select sc).ToList();

            return allDatabaseScreenings.Select(ConvertToDomain).ToList();
        }

        public override DatabaseScreening ConvertToDatabaseType(Screening domainType)
        {
            var databaseScreening = new DatabaseScreening
            {
                Id = domainType.Id,
                Candidate = domainType.Candidate,
                Recruiter = domainType.Recruiter,
                Location = domainType.Location,
                Date = domainType.Date,
                Remark = domainType.Remark,
                GlobalEvaluation = domainType.GlobalEvaluation,
                Aspects = new List<DatabaseScreeningAspect>()
            };

            foreach (var excercise in domainType.Excercises)
            {
                if (!excercise.Evaluations.Any())
                {
                    databaseScreening.Aspects.Add(new DatabaseScreeningAspect
                    {
                        AspectType = (int)DatabaseScreeningAspect.AspectTypes.Excercise,
                        Name = excercise.Name
                    });
                }
                else
                {
                    foreach (var evaluation in excercise.Evaluations)
                    {
                        var databaseScreeningAspectc = CreateDatabaseExerciceScreeningAspect(excercise, evaluation);

                        databaseScreening.Aspects.Add(databaseScreeningAspectc);
                    }
                }
            }

            foreach (var knowledgeDomain in domainType.KnowledgeDomains)
            {
                if (!knowledgeDomain.Evaluations.Any())
                {
                    databaseScreening.Aspects.Add(new DatabaseScreeningAspect
                    {
                        AspectType = (int)DatabaseScreeningAspect.AspectTypes.KnowledgeDomain,
                        Name = knowledgeDomain.Name
                    });
                }
                else
                {
                    foreach (var evaluation in knowledgeDomain.Evaluations)
                    {
                        var databaseScreeningAspect = CreateDatabaseKnowledgeScreeningAspect(knowledgeDomain, evaluation);

                        databaseScreening.Aspects.Add(databaseScreeningAspect);
                    }
                }
            }

            return databaseScreening;
        }

        private static DatabaseScreeningAspect CreateDatabaseKnowledgeScreeningAspect(KnowledgeDomain knowledgeDomain,
            Evaluation evaluation)
        {
            return new DatabaseScreeningAspect
            {
                AspectType = (int)DatabaseScreeningAspect.AspectTypes.KnowledgeDomain,
                Name = knowledgeDomain.Name,
                Score = (int)evaluation.Score,
                Remark = evaluation.Remark
            };
        }

        private static DatabaseScreeningAspect CreateDatabaseExerciceScreeningAspect(Excercise excercise,
            Evaluation evaluation)
        {
            return new DatabaseScreeningAspect
            {
                AspectType = (int)DatabaseScreeningAspect.AspectTypes.Excercise,
                Name = excercise.Name,
                Score = (int)evaluation.Score,
                Remark = evaluation.Remark
            };
        }

        private Screening ConvertToDomain(DatabaseScreening databaseScreening)
        {
            var screening = Screening.Load(databaseScreening.Id, databaseScreening.Candidate);
            screening.Recruiter = databaseScreening.Recruiter;
            screening.Location = databaseScreening.Location;
            screening.Date = databaseScreening.Date;
            screening.Remark = databaseScreening.Remark;
            screening.GlobalEvaluation = databaseScreening.GlobalEvaluation;

            var excercices = new Dictionary<string, Excercise>();
            var knowledgeDomains = new Dictionary<string, KnowledgeDomain>();

            foreach (var databaseSceeningAspect in databaseScreening.Aspects)
            {
                if ((DatabaseScreeningAspect.AspectTypes) databaseSceeningAspect.AspectType ==
                    DatabaseScreeningAspect.AspectTypes.Excercise)
                {
                    if (!excercices.ContainsKey(databaseSceeningAspect.Name))
                    {
                        excercices.Add(databaseSceeningAspect.Name, CreateExcercise(databaseSceeningAspect));
                    }
                    else
                    {
                        Excercise excercise = excercices[databaseSceeningAspect.Name];
                        excercise.AddEvaluation(CreateEvaluation(databaseSceeningAspect));
                    }
                }
                else
                {
                    if (!knowledgeDomains.ContainsKey(databaseSceeningAspect.Name))
                    {
                        knowledgeDomains.Add(databaseSceeningAspect.Name, CreateKnowledgeDomain(databaseSceeningAspect));
                    }
                    else
                    {
                        KnowledgeDomain domain = knowledgeDomains[databaseSceeningAspect.Name];
                        domain.AddEvaluation(CreateEvaluation(databaseSceeningAspect));
                    }
                }
            }
            foreach (var knowledgeDomain in knowledgeDomains.Values)
            {
                screening.AddKnowLedgeDomain(knowledgeDomain);
            }
            
            foreach (var excercise in excercices.Values)
            {
                screening.AddExcercise(excercise);
            }
            return screening;
        }

        private static KnowledgeDomain CreateKnowledgeDomain(DatabaseScreeningAspect databaseScreeningAspect)
        {
            var knowledgeDomain = KnowledgeDomainBuilder.Create(databaseScreeningAspect.Name)
                .Build();

            if (databaseScreeningAspect.Remark != null)
            {
                knowledgeDomain.AddEvaluation(CreateEvaluation(databaseScreeningAspect));
            }
            return knowledgeDomain;
        }

        private static Excercise CreateExcercise(DatabaseScreeningAspect databaseScreeningAspect)
        {
            var excercise = ExcerciseBuilder.Create(databaseScreeningAspect.Name)
                .Build();

            if (databaseScreeningAspect.Remark != null)
            {
                excercise.AddEvaluation(CreateEvaluation(databaseScreeningAspect));
            }

            return excercise;
        }

        private static Evaluation CreateEvaluation(DatabaseScreeningAspect databaseScreeningAspect)
        {
            var evalution = EvaluationBuilder.Create()
                .WithRemark(databaseScreeningAspect.Remark)
                .WithScores((Evaluation.EvaluationScores) databaseScreeningAspect.Score)
                .Build();

            return evalution;
        }
    }
}