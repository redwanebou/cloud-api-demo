using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoDBController : ControllerBase
    {
        private readonly CarContext context;

        public AutoDBController(CarContext context)
        {
            this.context = context;
        }

        // GET ALL CARS //
        [HttpGet]
        public List<Car> GetAllCars(string merk,string model,int bouwjaar,string brandstof,string sort, string dir = "asc")
        {
            // PAGING 
            IQueryable<Car> q = context.Car;

            if (!string.IsNullOrWhiteSpace(merk))
                q = q.Where(d => d.Merk == merk);
            if (!string.IsNullOrWhiteSpace(model))
                q = q.Where(d => d.Model == model);
            if (bouwjaar > 0)
                q = q.Where(d => d.Bouwjaar == bouwjaar);
            if (!string.IsNullOrWhiteSpace(brandstof))
                q = q.Where(d => d.Brandstof == brandstof);

            // SORTING
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "merk":
                        if (dir == "asc")
                            q = q.OrderBy(d => d.Merk);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Merk);
                        break;
                    case "model":
                        if (dir == "asc")
                            q = q.OrderBy(d => d.Model);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Model);
                        break;
                    case "bouwjaar":
                        if (dir == "asc")
                            q = q.OrderBy(d => d.Bouwjaar);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Bouwjaar);
                        break;
                    case "brandstof":
                        if (dir == "asc")
                            q = q.OrderBy(d => d.Brandstof);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Brandstof);
                        break;
                }
            }

            return q.ToList();
        }

        // CREATE CAR
        [HttpPost]
        public IActionResult CreateCar([FromBody] Car newCar)
        {
            context.Car.Add(newCar);
            context.SaveChanges();
            return Created($"Je auto met ID {newCar.ID} is aangemaakt.",newCar);
        }

        // FIND CAR WITH ID
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCar(int id)
        {
            var car = context.Car.Find(id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // DELETE A CAR
        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteCar(int id)
        {
            var car = context.Car.Find(id);
            if (car == null)
                return NotFound();

            context.Car.Remove(car);
            context.SaveChanges();

            return Content($"Je hebt succesvol je auto met ID {id} verwijderd.");
        }

        // UPDATE A CAR
        [Authorize]
        [HttpPut]
        public IActionResult UpdateCar([FromBody] Car updateCar)
        {
            var orgCar = context.Car.Find(updateCar.ID);
            if (orgCar == null)
                return NotFound();

            orgCar.Model = updateCar.Model;
            orgCar.Merk = updateCar.Merk;
            orgCar.Bouwjaar = updateCar.Bouwjaar;
            orgCar.Brandstof = updateCar.Brandstof;

            context.SaveChanges();
            return Ok(orgCar);
        }
    }
}
