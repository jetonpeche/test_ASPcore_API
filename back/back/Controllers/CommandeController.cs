using back.dbContext;
using back.table;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.dialogueBD;
using back.model;

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
            IQueryable listeCommande = D_Commande.ListerTouteCommandes();

            return new JsonResult(listeCommande);
        }

        [HttpGet("listeCommandeUtilisateur/{_id}")]
        public JsonResult ListeUtilisateur(int _id)
        {
            IQueryable liste = D_Commande.ListerCommandeUtilisateur(_id);

            return new JsonResult(liste);
        }

        [HttpGet("listeCommandeJour")]
        public JsonResult ListeJour()
        {
            IQueryable liste = D_Commande.ListerCommandeJour();

            return new JsonResult(liste);
        }

        [HttpPost("ajouterCommande")]
        public JsonResult Ajouter(CommandeModel _commande)
        {
            try
            {
                D_Commande.AjouterCommande(_commande);
                return new JsonResult("ok");
            }
            catch (Exception)
            {
                return new JsonResult("erreur");
            }
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
