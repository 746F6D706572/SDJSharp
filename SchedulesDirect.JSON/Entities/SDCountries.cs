using System;
using System.Collections.Generic;

namespace SchedulesDirect {
    /// <summary>
    /// Countries response. Provides list of continents/countries for which service is available
    /// </summary>
    public class SDCountries : SDCachedElement {
        public List<Continent> Continents;

        public SDCountries() {
            Continents = new List<Continent>();
            cacheDate = DateTime.UtcNow;
        }

        public class Continent : SDCachedElement {
            public string ContinentName;
            public List<Country> Countries;

            public Continent() {
                Countries = new List<Country>();
            }
        }

        public class Country : SDCachedElement {
            public string FullName;
            public string ShortName;
            public string PostalCodeExample;
            public string PostalCode;
            public bool OnePostalCode;
        }
    }
}