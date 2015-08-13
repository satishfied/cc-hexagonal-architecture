using System.Collections.Generic;

namespace Recruiting.Domain
{
    public class ScreeningAspect
    {
        public string Name { get; private set; }
        public IList<Score> Scores { get; private set; }

        public ScreeningAspect(string name)
        {
            this.Name = name;
            Scores = new List<Score>();
        }
    }
}
