using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace back.table
{
    public class Utilisateur
    {
        [Key]
        public int idUtilisateur { get; set; }

        [Column(TypeName = "nvarchar(50)")][Required]
        public string nomUtilisateur { get; set; }

        [Column(TypeName = "nvarchar(50)")][Required]
        public string prenomUtilisateur { get; set; }

        [Column(TypeName = "nvarchar(200)")][Required]
        public string adresseUtilisateur { get; set; }

        [Column(TypeName = "nvarchar(100)")][Required]
        public string mailUtilisateur { get; set; }

        [Column(TypeName = "nvarchar(200)")][Required]
        public string mdpUtilisateur { get; set; }
    }
}
