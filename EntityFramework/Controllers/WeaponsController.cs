using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using AutoMapper;
using EntityFramework.Models.Weapon;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WeaponsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetWeaponDto>>> GetWeapons()
        {
            var weapons = await _context.Weapons.Include(weapon => weapon.Character).ToListAsync();
            var records = _mapper.Map<List<GetWeaponDto>>(weapons);
            return records;
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWeaponDto>> GetWeapon(int id)
        {
            var weapon = await _context.Weapons.Include(weapon => weapon.Character).SingleOrDefaultAsync(c => c.Id == id);

            if (weapon == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<GetWeaponDto>(weapon);

            return record;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(int id, UpdateWeaponDto updateWeaponDto)
        {
            if( id != updateWeaponDto.Id)
            {
                return BadRequest();
            }

            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _mapper.Map(updateWeaponDto, weapon);

            try
            {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException)
            {
                if(!WeaponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(updateWeaponDto);
        }

        // POST: api/Weapons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(CreateWeaponDto weaponDto)
        {
            try
            {
                var weapon = _mapper.Map<Weapon>(weaponDto);
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetWeapon", new { id = weapon.Id }, weapon);

            }catch(DbUpdateConcurrencyException e)
            {
                return BadRequest(e);
            }

        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponExists(int id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
