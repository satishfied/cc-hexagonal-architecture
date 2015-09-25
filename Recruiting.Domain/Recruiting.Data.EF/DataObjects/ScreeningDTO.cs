using System;
using System.Collections.Generic;
using System.Linq;
using Recruiting.Data.EF.DataObjects;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ScreeningDTO
    {
        public static ScreeningDTO From(Screening screening)
        {
            return new ScreeningDTO
            {
                ID = screening.ID,
                Candidate = screening.Candidate,
                Date = screening.Date,
                Exercises = screening.Exercises.Select(ExerciseDTO.From).ToList(),
                KnowledgeDomains = screening.Exercises.Select(KnowledgeDomainDTO.From).ToList()
            };
        }

        public Screening ToDomain()
        {
            Screening screening = new Screening(Date, Candidate)
            {
                ID = ID
            };

            foreach (ExerciseDTO exerciseDto in Exercises)
            {
                screening.AddExercise(exerciseDto.ToDomain());
            }

            foreach (KnowledgeDomainDTO knowledgeDomainDto in KnowledgeDomains)
            {
                screening.AddKnowledgeDomain(knowledgeDomainDto.ToDomain());
            }

            return screening;
        }

        public int ID { get; set; }

        public string Candidate
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public virtual List<ExerciseDTO> Exercises { get; set; }
        public virtual List<KnowledgeDomainDTO> KnowledgeDomains { get; set; }
    }
}
