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
            catch (Exception)
            {
                return new JsonResult("pas de connexion");
            }
        }

        [HttpPost("rechercher/{_nomRechercher}")]
        public JsonResult Rechercher(string _nomRechercher)
        {
            try
            {
                var liste = D_Pain.Rechercher(Protection.XSS(_nomRechercher));

                return new JsonResult(liste);
            }
            catch (Exception)
            {
                return new JsonResult("erreur");
            }
        }

        [HttpGet("lister")]
        public JsonResult Lister()
        {
            var listeReturn = D_Pain.Lister();

            return new JsonResult(listeReturn);
        }

        [HttpPost("listerParPage/{_numPage}")]
        public JsonResult ListerParPage(int _numPage)
        {
            try
            {
                if (_numPage > 0)
                {
                    var liste = D_Pain.ListerParPage(_numPage);
                    return new JsonResult(liste);
                }
                else
                    return new JsonResult("le numéro de page ne peut pas etre négatif ou égale à 0");     
            }
            catch (Exception)
            {
                return new JsonResult("erreur");
            }
        }

        [HttpPut("modifier")]
        public JsonResult Modifier(Pain _pain)
        {
            try
            {
                D_Pain.Modifier(_pain);

                return new JsonResult("fait");
            }
            catch(Exception)
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
            catch(Exception)
            {
                return new JsonResult("existe pas");
            }
        }

    }
}
