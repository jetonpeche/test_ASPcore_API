using back.dbContext;
using back.table;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.dialogueBD;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandeController : Controller
    {
        private DataContext context;

        public CommandeController(DataContext _context)
        {
            context = _context;
            D_Commande.context = _context;
        }

        [HttpGet("listeGeneral")]
        public JsonResult Lister()
        {
            var listeCommande = D_Commande.ListerTouteCommandes();

            return new JsonResult(listeCommande);
        }

        [HttpPost("ajouterCommande")]
        public JsonResult Ajouter(Commande commande)
        {
            return new JsonResult("");
        }

        [HttpDelete("supprimer/{_id}")]
        public JsonResult Supprimer(int _id)
        {
            try
            {
                D_Commande.SupprimerCommande(_id);

                return new JsonResult(true);
            }
            catch(Exception e)
            {
                return new JsonResult(false);
            }
        }
    }
}
