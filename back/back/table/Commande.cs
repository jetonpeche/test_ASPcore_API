using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back.table
{
    public class Commande
    {
        [Key]
        public int idCommande { get; set; }

        [ForeignKey("utilisateur")][Required]
        public int idUtilisateur { get; set; }

        [Column(TypeName = "datetime")][Required]
        public DateTime dateLivraisonCommande { get; set; }

        public List<PainCommande> listePainCommande { get; set; }

        public Utilisateur utilisateur { get; set; }
    }
}
