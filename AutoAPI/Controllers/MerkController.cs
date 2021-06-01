using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MerkController : ControllerBase
    {
        private readonly MyDBContext context;

        public MerkController(MyDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ontvang alle merken.
        /// </summary>
        /// <response code="200">Merken gevonden.</response>
        /// <response code="404">Merken niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Merk), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public List<Merk> GetAllMerk(string naam, string sort, string dir = "asc")
        {
            // PAGING 
            IQueryable<Merk> m = context.m1;

            if (!string.IsNullOrWhiteSpace(naam))
                m = m.Where(d => d.Naam == naam);

            // SORTING
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "naam":
                        if (dir == "asc")
                            m = m.OrderBy(d => d.Naam);
                        else if (dir == "desc")
                            m = m.OrderByDescending(d => d.Naam);
                        break;
                }
            }

            return m.ToList();
        }

        /// <summary>
        /// Zoek een merk via ID.
        /// </summary>
        /// <response code="200">Merk gevonden.</response>
        /// <response code="404">Merk niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Merk), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetMerk(int id)
        {
            var merk = context.m1.Find(id);
            if (merk == null)
                return NotFound();

            return Ok(merk);
        }
    }
}
