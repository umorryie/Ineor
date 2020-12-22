using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ineor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private string url { get; set; }

        public RatesController()
        {
            url = "https://euvatrates.com/rates.json";
        }

        [HttpPost]
        [Route("countryVat/{countryCode}")]
        public IActionResult  GetSpecificCountryInfo(string countryCode)
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonFromUrl = JObject.Parse(urlString);
            var jsonRates = jsonFromUrl["rates"][countryCode];

            if (jsonRates == null)
            {
                return NotFound( new { message = "Wrong country code. Try again." });
            }

            return Ok( jsonRates.ToObject<Country>() );
        }

        [HttpGet]
        [Route("lowestVat")]
        public IActionResult GetThreeLowestVatRates()
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonFromUrl = JObject.Parse(urlString);
            var jsonRates = jsonFromUrl["rates"];

            return Ok(GetVatArray(jsonRates, "lowest"));
        }

        [HttpGet]
        [Route("highestVat")]
        public IActionResult GetThreeHighestVatRates()
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonFromUrl = JObject.Parse(urlString);
            var jsonRates = jsonFromUrl["rates"];

            return Ok(GetVatArray(jsonRates, "highest"));
        }

        private List<Country> GetVatArray(JToken rates, string ratesValue)
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
    }
}
