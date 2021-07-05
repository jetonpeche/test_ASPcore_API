using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.model
{
    public class Pain
    {
        public string idPain { get; set; }
        public string nomPain { get; set; } 

        public Pain(string _id, string _nom)
        {
            idPain = _id;
            nomPain = _nom;
        }
    }
}
