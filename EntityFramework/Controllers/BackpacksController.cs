using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using EntityFramework.Models.Backpack;
using AutoMapper;
using System.Data.Common;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackpacksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BackpacksController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Backpacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBackpackDto>>> GetBackpacks()
        {
            var backpackDto = await _context.Backpacks.Include(backpack => backpack.Character).ToListAsync();
            var records = _mapper.Map<List<GetBackpackDto>>(backpackDto);
            return records;
        }

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBackpackDto>> GetBackpack(int id)
        {
            var backpack = await _context.Backpacks.Include(backpack => backpack.Character).FirstOrDefaultAsync(c => c.Id == id);
            if (backpack == null)
            {
                return NotFound($"Backpack with ID {id} does not exists");
            }
            var record = _mapper.Map<GetBackpackDto>(backpack);

            return record;
        }

        // PUT: api/Backpacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack(int id, UpdateBackpackDto updateBackpackDto)
        {
            if (id != updateBackpackDto.Id)
            {
                return BadRequest();
            }

            var backpack = await _context.Backpacks.FindAsync(id);

            if(backpack == null)
            {
                return NotFound($"Backpack with {id} does not exists");
            }

            _mapper.Map(updateBackpackDto, backpack);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackpackExists(id))
                {
                    return NotFound($"Backpack with {id} does not exists");
                }
                else
                {
                    throw;
                }
            }
            catch(DbUpdateException)
            {
                return BadRequest("Error inserting a duplicate key  ");
            }

            return Ok(updateBackpackDto);
        }

        // POST: api/Backpacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Backpack>> PostBackpack(CreateBackpackDto backpackDto)
        {
            try
            {
                var backpack = _mapper.Map<Backpack>(backpackDto);
                _context.Backpacks.Add(backpack);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBackpack", new { id = backpack.Id }, backpack);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e);
            }

        }

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack(int id)
        {
            var backpack = await _context.Backpacks.FindAsync(id);
            if (backpack == null)
            {
                return NotFound();
            }

            _context.Backpacks.Remove(backpack);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BackpackExists(int id)
        {
            return _context.Backpacks.Any(e => e.Id == id);
        }
    }
}
