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
        [HttpGet]
        public List<Car> GetCar()
        {
            var lijst = new List<Car>();

            lijst.Add(new Car()
            {
                Merk = "Ford",
                Model = "Focus",
                Bouwjaar = 2002,
                Brandstof = "Diesel"
            });;

            lijst.Add(new Car()
            {
                Merk = "Fiat",
                Model = "Punto",
                Bouwjaar = 2012,
                Brandstof = "Diesel"
            });;

            lijst.Add(new Car()
            {
                Merk = "Volkswagen",
                Model = "Polo",
                Bouwjaar = 2012,
                Brandstof = "Diesel"
            });;
            return lijst;
        }
    }
}
