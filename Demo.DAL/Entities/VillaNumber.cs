using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class VillaNumber : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public int VillaId { get; set; }
        public virtual Villa Villa { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
