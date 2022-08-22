using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}",Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);

            if (celestialObject == null)
            {
                return NotFound();
            }
            else
            {
                List<CelestialObject> satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == celestialObject.Id).ToList();
                celestialObject.Satellites = satellites;
                return Ok(celestialObject);
            }

        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjectList = _context.CelestialObjects.Where(x => x.Name == name);

            if (!celestialObjectList.Any())
            {
                return NotFound();
            }
            else
            {
                

                foreach (var item in celestialObjectList)
                {
                    item.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == item.Id).ToList();
                }

                return Ok(celestialObjectList);
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjectList = _context.CelestialObjects;

            if (!celestialObjectList.Any())
            {
                return NotFound();
            }
            else
            {


                foreach (var item in celestialObjectList)
                {
                    item.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == item.Id).ToList();
                }

                return Ok(celestialObjectList);
            }
        }
    }
}
