using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using AutoMapper;
using EntityFramework.Models.Faction;
using EntityFramework.Models.Backpack;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FactionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Factions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetFactionDto>>> GetFactions()
        {
            var factionsDto = await _context.Factions.ToListAsync();
            var records = _mapper.Map<List<GetFactionDto>>(factionsDto);

            return records;
        }

        // GET: api/Factions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetFactionDto>> GetFaction(int id)
        {
            var faction = await _context.Factions.FindAsync(id);

            if (faction == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetFactionDto>(faction);

            return record;
        }

        // PUT: api/Factions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaction(int id, UpdateFactionDto updateFactionDto)
        {
            if (id != updateFactionDto.Id)
            {
                return BadRequest();
            }

            var faction = await _context.Factions.FindAsync(id);

            if (faction == null)
            {
                return NotFound();
            }

            _mapper.Map(updateFactionDto, faction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(updateFactionDto);
        }

        // POST: api/Factions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateFactionDto>> PostFaction(CreateFactionDto createFactionDto)
        {
            try
            {
                var faction = _mapper.Map<Faction>(createFactionDto);
                _context.Factions.Add(faction);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetFaction", new { id = faction.Id }, faction);
            }catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Factions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaction(int id)
        {
            var faction = await _context.Factions.FindAsync(id);
            if (faction == null)
            {
                return NotFound();
            }

            _context.Factions.Remove(faction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactionExists(int id)
        {
            return _context.Factions.Any(e => e.Id == id);
        }
    }
}
