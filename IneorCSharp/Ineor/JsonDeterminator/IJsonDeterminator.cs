using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ineor
{
    public interface IJsonDeterminator
    {
        public JToken GetCountry(string urlJsonString, string countryCode);
        public List<Country> GetVatArray(JToken rates, string ratesValue);
    }
}
