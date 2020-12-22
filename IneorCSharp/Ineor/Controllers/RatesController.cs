using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ineor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private const string url = "https://euvatrates.com/rates.json";

        public RatesController()
        {
        }

        [HttpGet]
        public String Get()
        {
            return "JAO";
        }
    }
}
