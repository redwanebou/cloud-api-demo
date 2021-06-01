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
    public class ModelController : ControllerBase
    {
        private readonly MyDBContext context;

        public ModelController(MyDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Ontvang alle modellen.
        /// </summary>
        /// <response code="200">Modellen gevonden.</response>
        /// <response code="404">Modellen niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Model), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public List<Model> GetAllModel(string naam, string sort, string dir = "asc")
        {
            // PAGING 
            IQueryable<Model> mm = context.m2;

            if (!string.IsNullOrWhiteSpace(naam))
                mm = mm.Where(d => d.Naam == naam);

            // SORTING
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "naam":
                        if (dir == "asc")
                            mm = mm.OrderBy(d => d.Naam);
                        else if (dir == "desc")
                            mm = mm.OrderByDescending(d => d.Naam);
                        break;
                }
            }
            mm = mm
            .Include(d => d.Merk);

            return mm.ToList();
        }

        /// <summary>
        /// Zoek een model via ID.
        /// </summary>
        /// <response code="200">Model gevonden.</response>
        /// <response code="404">Model niet gevonden.</response>
        /// <response code="500">De server heeft een onbekende fout aangetroffen.</response>
        [ProducesResponseType(typeof(Model), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetMerk(int id)
        {
            IQueryable<Model> mm = context.m2.Where(cc => cc.ID == id);

            if (mm == null)
                return NotFound();

            mm = mm
           .Include(d => d.Merk);

            return Ok(mm);
        }
    }
}
