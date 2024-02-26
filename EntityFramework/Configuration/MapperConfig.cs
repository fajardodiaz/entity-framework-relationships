using AutoMapper;
using EntityFramework.Data;
using EntityFramework.Models.Backpack;
using EntityFramework.Models.Character;
using EntityFramework.Models.Faction;
using EntityFramework.Models.Weapon;

namespace EntityFramework.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // BackpackDto
            CreateMap<Backpack, CreateBackpackDto>().ReverseMap();
            CreateMap<Backpack, GetBackpackDto>().ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Character.Name)).ReverseMap();
            CreateMap<Backpack, UpdateBackpackDto>().ReverseMap();

            // Weapon Dto
            CreateMap<Weapon, CreateWeaponDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponDto>().ForMember(dest=> dest.CharacterName, opt => opt.MapFrom(src => src.Character.Name)).ReverseMap();
            CreateMap<Weapon, UpdateWeaponDto>().ReverseMap();

            // Faction Dto
            CreateMap<Faction, CreateFactionDto>().ReverseMap();
            CreateMap<Faction, UpdateFactionDto>().ReverseMap();
            CreateMap<Faction, GetFactionDto>().ReverseMap();

            // Character Dto
            CreateMap<Character, CreateCharacterDto>().ReverseMap();
            CreateMap<Character, UpdateCharacterDto>().ReverseMap();
            CreateMap<Character, GetCharacterDto>().ReverseMap();

        }
    }
}
