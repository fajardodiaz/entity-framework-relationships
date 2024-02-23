using EntityFramework.Models.Character;

namespace EntityFramework.Models.Backpack
{
    public class GetBackpackDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CharacterName { get; set; }
    }
}
