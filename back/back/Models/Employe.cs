using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models
{
    public class Employe
    {
        public int idEmploye { get; set; }
        public string nomEmploye { get; set; }
        public string dateEmbauche { get; set; }
        public string nomPhoto { get; set; }

        public string departement { get; set; }
    }
}
