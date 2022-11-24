using System;
using System.Collections.Generic;

namespace SchedulesDirect {
    /// <summary>
    /// Countries response. Provides list of continents/countries for which service is available
    /// </summary>
    public class SDCountries : SDCachedElement {
        public List<Continent> continents;

        public SDCountries() {
            continents = new List<Continent>();
            cacheDate = DateTime.UtcNow;
        }

        public class Continent : SDCachedElement {
            public string continentname;
            public List<Country> countries;

            public Continent() {
                countries = new List<Country>();
            }
        }

        public class Country : SDCachedElement {
            public string fullName;
            public string shortName;
            public string postalCodeExample;
            public string postalCode;
            public bool onePostalCode;
        }
    }
}
