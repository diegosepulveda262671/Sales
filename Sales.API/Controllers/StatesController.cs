using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("/api/states")]
	public class StatesController: ControllerBase
	{
		private readonly DataContext _context;
		public StatesController(DataContext context)
		{
			_context = context;
		}
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.States.Include(x => x.Cities).ToListAsync());
        }

     

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.States.Include(x=>x.Cities).FirstOrDefaultAsync(x => x.Id == id);
            if (state is null)
            {
                return NotFound();
            }
            return Ok(state);
        }

 


        [HttpPost]
        public async Task<ActionResult> Save(State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado con el mismo nombre");
                }

                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException!.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(State state)
        {
            try
            {
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado/departamento con el mismo nombre");
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
            var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state is null)
            {
                return NotFound();
            }

            _context.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();


        }
    }
}


