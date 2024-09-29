using System.ComponentModel.DataAnnotations;

namespace Demo.API.DTOs
{
    public class VillaNumberUpdateDTO
    {
        public int Id { get; set; }
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
