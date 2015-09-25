using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ExerciseDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public static ExerciseDTO From(ScreeningAspect screeningAspect)
        {
            return new ExerciseDTO
            {
                Name = screeningAspect.Name
            };
        }

        public ScreeningAspect ToDomain()
        {
            return new ScreeningAspect(Name);
        }
    }
}