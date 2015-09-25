using Recruiting.Domain;

namespace Recruiting.Data.EF.DataObjects
{
    public class KnowledgeDomainDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public static KnowledgeDomainDTO From(ScreeningAspect screeningAspect)
        {
            return new KnowledgeDomainDTO
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