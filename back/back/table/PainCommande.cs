using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back.table
{
    public class PainCommande
    {
        [Key]
        public int idCommande { get; set; }

        [Key]
        public int idPain { get; set; }

        [Column(TypeName = "int")][Required]
        public int qte { get; set; }

        public Commande commande { get; set; }
        public Pain pain { get; set; }
    }
}
