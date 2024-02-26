using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using AutoMapper;
using EntityFramework.Models.Character;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCharacterDto>>> GetCharacters()
        {
            var charactersDto = await _context.Characters.ToListAsync();
            var records = _mapper.Map<List<GetCharacterDto>>(charactersDto);

            return records;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDto>> GetCharacter(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetCharacterDto>(character);

            return Ok(record);
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, UpdateCharacterDto updateCharacterDto)
        {
            if (id != updateCharacterDto.Id)
            {
                return BadRequest();
            }

            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCharacterDto, character);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }catch(DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(updateCharacterDto);
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CreateCharacterDto characterDto)
        {
            try
            {
                var character = _mapper.Map<Character>(characterDto);
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
            }catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
