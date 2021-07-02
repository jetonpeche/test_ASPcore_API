using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models
{
    public class Outil
    {
        public string idOutil { get; set; }
        public string nomOutil { get; set; }

        public Outil(string _id, string _nom)
        {
            idOutil = _id;
            nomOutil = _nom;
        }
    }
}
