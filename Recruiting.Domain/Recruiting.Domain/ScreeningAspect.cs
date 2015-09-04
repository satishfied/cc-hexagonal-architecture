using System.Collections.Generic;

namespace Recruiting.Domain
{
    public class ScreeningAspect
    {
        public string Name { get; set; }
        public IList<Score> Scores { get; set; }

        public ScreeningAspect()
        {
            
        }

        public ScreeningAspect(string name)
        {
            this.Name = name;
            Scores = new List<Score>();
        }
    }
}
