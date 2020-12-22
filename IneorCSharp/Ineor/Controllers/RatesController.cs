using Microsoft.AspNetCore.Cors;
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
        protected IJsonDeterminator jsonDeterminator { get; set; }

        public RatesController(IJsonDeterminator determineJson)
        {
            url = "https://euvatrates.com/rates.json";
            jsonDeterminator = determineJson;
        }

        [EnableCors()]
        [HttpPost]
        [Route("countryVat/{countryCode}")]
        public IActionResult  GetSpecificCountryInfo(string countryCode)
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonRates = jsonDeterminator.GetCountry(urlString, countryCode);

            if (jsonRates == null)
            {
                return Ok( new { message = "Wrong country code. Try again." });
            }

            return Ok( jsonRates.ToObject<Country>() );
        }

        [EnableCors()]
        [HttpGet]
        [Route("lowestVat")]
        public IActionResult GetThreeLowestVatRates()
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonFromUrl = JObject.Parse(urlString);
            var jsonRates = jsonFromUrl["rates"];

            return Ok(jsonDeterminator.GetVatArray(jsonRates, "lowest"));
        }

        [EnableCors()]
        [HttpGet]
        [Route("highestVat")]
        public IActionResult GetThreeHighestVatRates()
        {
            var urlString = new WebClient().DownloadString(url);
            var jsonFromUrl = JObject.Parse(urlString);
            var jsonRates = jsonFromUrl["rates"];

            return Ok(jsonDeterminator.GetVatArray(jsonRates, "highest"));
        }
    }
}
