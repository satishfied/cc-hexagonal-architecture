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
                (from sc in ObjectContextFactory.Create().DatabaseScreenings select sc).ToList();

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
                Aspects = new List<DatabaseSceeningAspect>()
            };

            foreach (var excercise in domainType.Excercises)
            {
                if (!excercise.Evaluations.Any())
                {
                    databaseScreening.Aspects.Add(new DatabaseSceeningAspect
                    {
                        AspectType = (int) DatabaseSceeningAspect.AspectTypes.Excercise,
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
                    databaseScreening.Aspects.Add(new DatabaseSceeningAspect
                    {
                        AspectType = (int) DatabaseSceeningAspect.AspectTypes.KnwoledgeDomain,
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

        private static DatabaseSceeningAspect CreateDatabaseKnowledgeScreeningAspect(KnowledgeDomain knowledgeDomain,
            Evaluation evaluation)
        {
            return new DatabaseSceeningAspect
            {
                AspectType = (int) DatabaseSceeningAspect.AspectTypes.KnwoledgeDomain,
                Name = knowledgeDomain.Name,
                Score = (int) evaluation.Score,
                Remark = evaluation.Remark
            };
        }

        private static DatabaseSceeningAspect CreateDatabaseExerciceScreeningAspect(Excercise excercise,
            Evaluation evaluation)
        {
            return new DatabaseSceeningAspect
            {
                AspectType = (int) DatabaseSceeningAspect.AspectTypes.Excercise,
                Name = excercise.Name,
                Score = (int) evaluation.Score,
                Remark = evaluation.Remark
            };
        }

        private Screening ConvertToDomain(DatabaseScreening databaseScreening)
        {
            var screening = new Screening
            {
                Id = databaseScreening.Id,
                Candidate = databaseScreening.Candidate,
                Recruiter = databaseScreening.Recruiter,
                Location = databaseScreening.Location,
                Date = databaseScreening.Date,
                Remark = databaseScreening.Remark,
                GlobalEvaluation = databaseScreening.GlobalEvaluation
            };

            var excercices = new Dictionary<string, Excercise>();
            var knowledgeDomains = new Dictionary<string, KnowledgeDomain>();

            foreach (var databaseSceeningAspect in databaseScreening.Aspects)
            {
                if ((DatabaseSceeningAspect.AspectTypes) databaseSceeningAspect.AspectType ==
                    DatabaseSceeningAspect.AspectTypes.Excercise)
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

        private static KnowledgeDomain CreateKnowledgeDomain(DatabaseSceeningAspect databaseSceeningAspect)
        {
            var knowledgeDomain = new KnowledgeDomain
            {
                Name = databaseSceeningAspect.Name
            };

            if (databaseSceeningAspect.Remark != null)
            {
                knowledgeDomain.AddEvaluation(CreateEvaluation(databaseSceeningAspect));
            }
            return knowledgeDomain;
        }

        private static Excercise CreateExcercise(DatabaseSceeningAspect databaseSceeningAspect)
        {
            var excercise = new Excercise
            {
                Name = databaseSceeningAspect.Name
            };

            if (databaseSceeningAspect.Remark != null)
            {
                excercise.AddEvaluation(CreateEvaluation(databaseSceeningAspect));
            }
            return excercise;
        }

        private static Evaluation CreateEvaluation(DatabaseSceeningAspect databaseSceeningAspect)
        {
            var evalution = new Evaluation
            {
                Remark = databaseSceeningAspect.Remark,
                Score = (Evaluation.EvaluationScores) databaseSceeningAspect.Score
            };
            return evalution;
        }
    }
}