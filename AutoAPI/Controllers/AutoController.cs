using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoAPI.Models;
using AutoAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoController : ControllerBase
    {
        private readonly MyDBContext context;

        public AutoController(MyDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ontvang alle autos.
        /// </summary>
        /// <response code="200">Autos gevonden.</response>
        /// <response code="404">Autos niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IQueryable<CarDTO> GetAllCars(string merk,string model,int bouwjaar,string brandstof,string sort, string dir = "asc")
        {
            // DTO
            var q = from c in context.Car
                     select new CarDTO()
                     {
                         ID = c.ID,
                         Bouwjaar = c.Bouwjaar,
                         Brandstof = c.Brandstof,
                         prijs = c.prijs,
                         Verkocht = c.Verkocht,
                         Merk = c.Merk,
                         Model = c.Model,
                         person = new PersonDTO()
                             {
                             user_id = c.person.user_id,
                             nickname = c.person.nickname
                            }
                     };

            if (!string.IsNullOrWhiteSpace(merk))
                q = q.Where(d => d.Merk.Naam == merk);
            if (!string.IsNullOrWhiteSpace(model))
                q = q.Where(d => d.Model.Naam == model);
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
                            q = q.OrderBy(d => d.Merk.Naam);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Merk.Naam);
                        break;
                    case "model":
                        if (dir == "asc")
                            q = q.OrderBy(d => d.Model.Naam);
                        else if (dir == "desc")
                            q = q.OrderByDescending(d => d.Model.Naam);
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

            return q;
        }

        /// <summary>
        /// Maak een auto aan.
        /// </summary>
        /// <response code="200">Auto aangemaakt.</response>
        /// <response code="401">Niet geautoriseerd om een auto te creëren.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
           public async Task<IActionResult> CreateCar([FromBody] Car newCar)
            {
             var merkexist = await context.Car.AnyAsync(b => b.Merk.Naam == newCar.Merk.Naam);
             var modelexist = await context.Car.AnyAsync(b => b.Model.Naam == newCar.Model.Naam);
             var userexist = await context.Person.AnyAsync(b => b.user_id == newCar.person.user_id);

            // als er een merk is maar geen model dan koppelen we de model aan de bestaande merk
            if (merkexist && !modelexist)
            {
                var merk = context.m1.Where(merk => merk.Naam == newCar.Merk.Naam).FirstOrDefault();
                newCar.Merk = merk;
            }
            // als zowel de merk en de model bestaat nemen we beide bestaande data over
            if (merkexist && modelexist)
            {
                var merk = context.m1.Where(merk => merk.Naam == newCar.Merk.Naam).FirstOrDefault();
                var model = context.m2.Where(model => model.Naam == newCar.Model.Naam).FirstOrDefault();
                newCar.Merk = merk;
                newCar.Model = model;
            }
            if (userexist)
            {
                var person = context.Person.Where(p => p.user_id == newCar.person.user_id).FirstOrDefault();
                newCar.person = person;
            }
                newCar.Model.Merk = newCar.Merk;
                context.Car.Add(newCar);
                await context.SaveChangesAsync();
                return Created($"Je auto met ID {newCar.ID} is aangemaakt.", newCar);
            }

        /// <summary>
        /// Zoek een auto via ID.
        /// </summary>
        /// <response code="200">Auto gevonden.</response>
        /// <response code="404">Auto niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCar(int id)
        {
            // DTO
            var cc = from c in context.Car.Where(cc => cc.ID == id)
                     select new CarDTO()
                     {
                         ID = c.ID,
                         Bouwjaar = c.Bouwjaar,
                         Brandstof = c.Brandstof,
                         prijs = c.prijs,
                         Verkocht = c.Verkocht,
                         Merk = c.Merk,
                         Model = c.Model,
                         person = new PersonDTO()
                         {
                             user_id = c.person.user_id,
                             nickname = c.person.nickname
                         }
                     };

            if (cc == null)
                return NotFound();

            return Ok(cc);
        }

        /// <summary>
        /// Verwijder een auto via ID.
        /// </summary>
        /// <response code="200">Auto verwijderd.</response>
        /// <response code="404">Auto niet gevonden.</response>
        /// <response code="401">Niet geautoriseerd om een auto te verwijderen.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteCar(int id)
        {
            var car = context.Car.Find(id);
            if (car == null)
                return NotFound();

            context.Car.Remove(car);
            context.SaveChanges();

            return Ok(car);
        }

        /// <summary>
        /// Update een auto.
        /// </summary>
        /// <response code="200">Auto succesvol geupdate.</response>
        /// <response code="404">Auto niet gevonden.</response>
        /// <response code="401">Niet geautoriseerd om een auto te update.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut]
        public IActionResult UpdateCar([FromBody] Car updateCar)
        {
            var orgCar = context.Car.Find(updateCar.ID);

            if (orgCar == null)
                return NotFound();

            var model = context.m2.Where(p => p.ID == updateCar.ID).FirstOrDefault();
            var merk = context.m1.Where(p => p.ID == updateCar.ID).FirstOrDefault();
            var person = context.Person.Where(p => p.user_id == updateCar.person.user_id).FirstOrDefault();
            orgCar.Model = model;
            orgCar.Merk = merk;
            orgCar.Bouwjaar = updateCar.Bouwjaar;
            orgCar.Brandstof = updateCar.Brandstof;
            orgCar.person = person;
            orgCar.Verkocht = updateCar.Verkocht;

            context.SaveChanges();
            return Ok(orgCar);
        }
    }
}
