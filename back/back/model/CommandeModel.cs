using back.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.model
{
    public class CommandeModel
    {
        public int idUtilisateur { get; set; }

        public DateTime dateLivraisonCommande { get; set; }

        public List<PainCommande> listePain { get; set; }
    }
}
