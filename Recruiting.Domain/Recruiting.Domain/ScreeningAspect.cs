using System.Collections.Generic;

namespace Recruiting.Domain
{
    using Recruiting.Domain.Core;

    public class ScreeningAspect : Entity
    {
        public string Name { get; protected set; }
        public IList<Score> Scores { get; protected set; }

        //EF:change:protected 
        protected ScreeningAspect()
        {
            
        }

        public ScreeningAspect(string name)
        {
            this.Name = name;
            Scores = new List<Score>();
        }
    }
}
