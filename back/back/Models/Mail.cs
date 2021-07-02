using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Models
{
    public class Mail
    {
        public string destinataire { get; set; }
        public string expediteur { get; set; }
        public string sujet { get; set; }
        public string message { get; set; }
    }
}
