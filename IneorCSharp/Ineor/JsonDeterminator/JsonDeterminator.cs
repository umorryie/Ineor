using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ineor
{
    public class JsonDeterminator: IJsonDeterminator
    {
        public List<Country> GetVatArray(JToken rates, string ratesValue)
        {
            var countryList = new List<Country> { };
            IList<string> keys = JObject.Parse(rates.ToString()).Properties().Select(p => p.Name).ToList();

            foreach (var key in keys)
            {
                countryList.Add(rates[key].ToObject<Country>());
            }

            // order list from lowest to highest VAT
            countryList = countryList.OrderBy(o => o.standard_rate).ToList(); ;

            if (ratesValue == "lowest")
            {
                return countryList.GetRange(0, 3);
            }
            else
            {
                return countryList.GetRange(countryList.Count - 3, 3);
            }
        }

        public JToken GetCountry(string urlJsonString, string countryCode)
        {
            var jsonFromUrl = JObject.Parse(urlJsonString);

            return jsonFromUrl["rates"][countryCode];
        }
    }
}
