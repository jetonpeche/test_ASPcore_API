using back.Classe;
using back.dbContext;
using back.dialogueBD;
using back.table;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PainController : ControllerBase
    {
        private readonly DataContext context;

        public PainController(DataContext _context)
        {
            context = _context;
            D_Pain.context = context;
        }

        [HttpPost("ajouter")]
        public JsonResult Ajouter(Pain[] _listePain)
        {
            try
            {
                D_Pain.Ajouter(_listePain);

                return new JsonResult("Ajouts faient");
            }
            catch (Exception e)
            {
                return new JsonResult("pas de connexion");
            }
        }

        [HttpGet("lister")]
        public JsonResult Lister()
        {
            var listeReturn = D_Pain.Lister();

            return new JsonResult(listeReturn);
        }

        [HttpPut("modifier")]
        public JsonResult Modifier(Pain _pain)
        {
            try
            {
                D_Pain.Modifier(_pain);

                return new JsonResult("fait");
            }
            catch(Exception e)
            {
                return new JsonResult("existe plus");
            }
        }

        [HttpDelete("supprimer/{_id}")]
        public JsonResult Supprimer(int _id)
        {
            try
            {
                D_Pain.Supprimer(_id);

                return new JsonResult("fait");
            }
            catch(Exception e)
            {
                return new JsonResult("existe pas");
            }
        }

    }
}
