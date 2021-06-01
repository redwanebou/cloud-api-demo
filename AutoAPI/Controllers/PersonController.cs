using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Web.Http.Description;
using AutoAPI.Models.DTO;

namespace AutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly MyDBContext context;

        public PersonController(MyDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ontvang alle personen.
        /// </summary>
        /// <response code="200">Personen gevonden.</response>
        /// <response code="404">Personen niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IQueryable<PersonDTO> GetAllPerson(string nickname, string sort, string dir = "asc")
        {
            // DTO
            var pp = from p in context.Person
                     select new PersonDTO()
                     {
                         user_id = p.user_id,
                         nickname = p.nickname
                     };

            // PAGING 
            if (!string.IsNullOrWhiteSpace(nickname))
                pp = pp.Where(d => d.nickname == nickname);

            // SORTING
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "email":
                        if (dir == "asc")
                            pp = pp.OrderBy(d => d.nickname);
                        else if (dir == "desc")
                            pp = pp.OrderByDescending(d => d.nickname);
                        break;
                }
            }

            return pp;
        }

        /// <summary>
        /// Maak een persoon aan.
        /// </summary>
        /// <response code="200">Persoon aangemaakt.</response>
        /// <response code="401">Niet geautoriseerd om een persoon te creëren.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Person.Add(newPerson);
            context.SaveChangesAsync();
            return Created($"Je auto met ID {newPerson.user_id} is aangemaakt.", newPerson);
        }

        /// <summary>
        /// Update een persoon.
        /// </summary>
        /// <response code="200">Persoon succesvol geupdate.</response>
        /// <response code="404">Persoon niet gevonden.</response>
        /// <response code="401">Niet geautoriseerd om een persoon te update.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut]
        public IActionResult UpdatePerson([FromBody] Person updatePerson)
        {
            var orgPerson = context.Person.Find(updatePerson.user_id);
            if (orgPerson == null)
                return NotFound();

            orgPerson.email = updatePerson.email;
            orgPerson.nickname = updatePerson.nickname;
            orgPerson.geld = updatePerson.geld;

            context.SaveChanges();
            return Ok(orgPerson);
        }

        /// <summary>
        /// Zoek een persoon via ID.
        /// </summary>
        /// <response code="200">Persoon gevonden.</response>
        /// <response code="404">Persoon niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetPerson(string id)
        {
            var person = context.Person.Find(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }
    }
}
