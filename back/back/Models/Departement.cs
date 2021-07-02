using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models
{
    public class Departement
    {
        public int idDep { get; set; }
        public string nomDep { get; set; }

        public Departement(int _id, string _nom)
        {
            idDep = _id;
            nomDep = _nom;
        }
    }
}
