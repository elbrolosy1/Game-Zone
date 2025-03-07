using System.ComponentModel.DataAnnotations;

namespace GamesZone.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(250)]
        public string Icon { get; set; }
    }
}
