using System.ComponentModel.DataAnnotations;

namespace Demo.API.DTOs
{
    public class VillaNumberDTO
    {
        public int Id { get; set; }
        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public virtual VillaDTO Villa { get; set; }
    }
}
