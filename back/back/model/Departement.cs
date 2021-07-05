using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back.model
{
    public class Departement
    {
        [Key]
        public int idDep { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string nomDep { get; set; }
    }
}
