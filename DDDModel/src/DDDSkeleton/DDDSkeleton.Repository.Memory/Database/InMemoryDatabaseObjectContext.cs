using System;
using System.Collections.Generic;
using System.Linq;
using DDDSkeleton.Domain;

namespace DDDSkeleton.Repository.Memory.Database
{
    public class InMemoryDatabaseObjectContext
    {
        public InMemoryDatabaseObjectContext()
        {
            Seed();
        }

        public List<DatabaseScreening> DatabaseScreenings { get; set; }

        public static InMemoryDatabaseObjectContext Instance
        {
            get { return Nested.instance; }
        }

        public void AddEntity<T>(T databaseEntity)
        {
            var screening = databaseEntity as DatabaseScreening;
            if (screening != null)
            {
                screening.Id = DatabaseScreenings.Count + 1;
                DatabaseScreenings.Add(screening);
            }
        }

        public void UpdateEntity<T>(T databaseEntity)
        {
            var screening = databaseEntity as DatabaseScreening;

            if (screening != null)
            {
                var existingScreening = (from sc in DatabaseScreenings
                    where sc.Id == screening.Id
                    select sc).First();

                existingScreening.Candidate = screening.Candidate;
                existingScreening.Recruiter = screening.Recruiter;
                existingScreening.Date = screening.Date;
                existingScreening.Location = screening.Location;
                existingScreening.Remark = screening.Remark;
                existingScreening.GlobalEvaluation = screening.GlobalEvaluation;
                
                UpdateScreeningAspects(screening, existingScreening);
            }
        }

        private static void UpdateScreeningAspects(DatabaseScreening screening, DatabaseScreening existingScreening)
        {
            existingScreening.Aspects.Clear();

            foreach (var databaseSceeningAspect in screening.Aspects)
            {
                var aspect = new DatabaseScreeningAspect
                {
                    Name = databaseSceeningAspect.Name,
                    Remark = databaseSceeningAspect.Remark,
                    Score = databaseSceeningAspect.Score,
                    AspectType = databaseSceeningAspect.AspectType
                };

                existingScreening.Aspects.Add(aspect);
            }
        }

        public void DeleteEntity<T>(T databaseEntity)
        {
            var screening = databaseEntity as DatabaseScreening;
            if (screening != null)
            {
                var dBScreening = (from sc in DatabaseScreenings
                    where sc.Id == screening.Id
                    select sc).FirstOrDefault();

                DatabaseScreenings.Remove(dBScreening);
            }
        }

        private void Seed()
        {
            var screening1 = StubFirstScreening();
            var screening2 = StubSecondScreening();
            var screening3 = StubThirdScreening();

            DatabaseScreenings = new List<DatabaseScreening> {screening1, screening2, screening3};
        }

        private static DatabaseScreening StubFirstScreening()
        {
            var aspects = new List<DatabaseScreeningAspect>
            {
                new DatabaseScreeningAspect
                {
                    Name = "Sum of squares",
                    AspectType = (int) DatabaseScreeningAspect.AspectTypes.Excercise,
                    Remark = "Kennis van linq beperkt",
                    Score = (int) Evaluation.EvaluationScores.Bad
                },
                new DatabaseScreeningAspect
                {
                    Name = "Sum of squares",
                    AspectType = (int) DatabaseScreeningAspect.AspectTypes.Excercise,
                    Remark = "Kent POW",
                    Score = (int) Evaluation.EvaluationScores.Good
                }
            };

            var screening = new DatabaseScreening
            {
                Id = 1,
                Candidate = "Van Baelen Raf",
                Date = new DateTime(2005, 7, 1, 9, 0, 0),
                GlobalEvaluation = "Junior met capaciteiten",
                Recruiter = "Verbraeken Marc",
                Remark = "OO zeer goed",
                Location = "Cegeka Hasselt HealthCare",
                Aspects = aspects
            };

            return screening;
        }

        private static DatabaseScreening StubSecondScreening()
        {
            var aspects = new List<DatabaseScreeningAspect>
            {
                new DatabaseScreeningAspect
                {
                    Name = "Oracle",
                    AspectType = (int) DatabaseScreeningAspect.AspectTypes.KnowledgeDomain,
                    Remark = "Goede kennis van SQL",
                    Score = (int) Evaluation.EvaluationScores.Good
                }
            };

            var screening = new DatabaseScreening
            {
                Id = 2,
                Candidate = "Royer Yves",
                Date = new DateTime(2005, 9, 30, 13, 0, 0),
                GlobalEvaluation = "Junior met goede kennis van db",
                Recruiter = "Genoe Steven",
                Remark = "Gedreven",
                Location = "Cegeka Hasselt HealthCare",
                Aspects = aspects
            };

            return screening;
        }

        private static DatabaseScreening StubThirdScreening()
        {
            var aspects = new List<DatabaseScreeningAspect>
            {
                new DatabaseScreeningAspect
                {
                    Name = "REST",
                    AspectType = (int) DatabaseScreeningAspect.AspectTypes.KnowledgeDomain,
                    Remark = "Goede kennis van Web Api",
                    Score = (int) Evaluation.EvaluationScores.Good
                },
                new DatabaseScreeningAspect
                {
                    Name = "REST",
                    AspectType = (int) DatabaseScreeningAspect.AspectTypes.KnowledgeDomain,
                    Remark = "Kent het RMM model met HATEOAS",
                    Score = (int) Evaluation.EvaluationScores.Good
                }
            };

            var screening = new DatabaseScreening
            {
                Id = 3,
                Candidate = "Boutsen Koenmans",
                Date = new DateTime(2012, 3, 1, 7, 0, 0),
                GlobalEvaluation = "Medior met goede kennis van MVC",
                Recruiter = "Van Baelen Raf",
                Remark = "Verlegen",
                Location = "Cegeka Hasselt HealthCare",
                Aspects = aspects
            };

            return screening;
        }

        private class Nested
        {
            internal static readonly InMemoryDatabaseObjectContext instance = new InMemoryDatabaseObjectContext();

            static Nested()
            {
            }
        }
    }
}