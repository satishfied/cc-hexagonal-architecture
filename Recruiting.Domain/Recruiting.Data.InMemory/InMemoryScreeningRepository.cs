﻿using System;
using System.Collections.Generic;
using System.Linq;
using Recruiting.Domain;

namespace Recruiting.Data.InMemory
{
    public class InMemoryScreeningRepository : IScreeningRepository
    {
        #region  Fields

        private readonly List<Screening> screenings = new List<Screening>();

        #endregion

        #region Properties

        public List<Screening> Screenings
        {
            get
            {
                return this.screenings;
            }
        }

        #endregion

        #region Methods

        public string Add(Screening screening)
        {
            if (!this.Screenings.Contains(screening))
            {
                this.Screenings.Add(screening);

                screening.ID = this.Screenings.Count;

                return screening.ID.ToString();
            }

            return "NaN";
        }

        public IEnumerable<Screening> FindAll()
        {
            return this.Screenings;
        }

        public Screening FindById(string id)
        {
            int index = Int32.Parse(id) - 1;
            return this.Screenings.ElementAtOrDefault(index);
        }

        #endregion
    }
}