using System.ComponentModel.DataAnnotations;

namespace GamesZone.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        public ICollection<Games> Games { get; set; } = new List<Games>();
    }
}
