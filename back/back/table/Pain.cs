using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back.table
{
    public class Pain
    {
        [Key]
        public int idPain { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string nomPain { get; set; } 

        public List<PainCommande> listePainCommande { get; set; }
    }
}
