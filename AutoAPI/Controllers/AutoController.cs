using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        [Route("ford")]
        [HttpGet]
        public List<Car> GetPerson()
        {
            var lijst = new List<Car>();

            lijst.Add(new Car()
            {
                Merk = "Ford",
                Model = "Focus",
                Bouwjaar = 2002
            }); ;
            return lijst;
        }
    }
}
