using EntityFramework.Models.Backpack;
using EntityFramework.Models.Faction;
using EntityFramework.Models.Weapon;

namespace EntityFramework.Models.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetBackpackDto Backpack { get; set; }
        public List<GetFactionDto> Factions { get; set; }
        public List<GetWeaponDto> Weapons { get; set; }
    }
}
