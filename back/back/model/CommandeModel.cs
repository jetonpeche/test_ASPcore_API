using back.table;
using System;
using System.Collections.Generic;

namespace back.model
{
    public class CommandeModel
    {
        public int idUtilisateur { get; }

        public string mailUtilisateur { get; }

        public DateTime dateLivraisonCommande { get; }

        public List<PainCommande> listePain { get; }
    }
}
