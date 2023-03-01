using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("/api/cities")]
	public class Citiescontroller: ControllerBase
	{
		
        private readonly DataContext _context;
        public Citiescontroller(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Cities.ToListAsync());
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (country is null)
            {
                return NotFound();
            }
            return Ok(country);
        }


        [HttpPost]
        public async Task<ActionResult> Save(City city)
        {
            try
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre");
                }

                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException!.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(City city)
        {
            try
            {
                _context.Update(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre");
                }

                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException!.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city is null)
            {
                return NotFound();
            }

            _context.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();


        }
    }
}

