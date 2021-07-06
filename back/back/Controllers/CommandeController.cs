using back.dbContext;
using back.table;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        [HttpGet("listeGeneral")]
        public JsonResult Lister()
        {
            var listeCommande = from commande in context.commande

                                join uti in context.utilisateur
                                on commande.utilisateur.idUtilisateur equals uti.idUtilisateur

                                orderby commande.dateLivraisonCommande ascending
                                select new
                                {
                                    commande.idCommande,
                                    commande.dateLivraisonCommande,
                                    uti.adresseUtilisateur,

                                    // selection des infos de l'utilisateur dans des { }
                                    utilisateur = new { uti.nomUtilisateur, uti.prenomUtilisateur, uti.mailUtilisateur },

                                    // sous requete liste de des pains de la commande
                                    liste = (from p in context.painCommande
                                             join pain in context.pain
                                             on p.idPain equals pain.idPain
                                             where p.commande.utilisateur.idUtilisateur == uti.idUtilisateur
                                             select new { pain.idPain, pain.nomPain, p.qte }) as object
                                };

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
                var painCommandeDelete = (from pc in context.painCommande
                                          where pc.idCommande == _id
                                          select pc).ToArray();

                var commandeDelete = (from c in context.commande
                                      where c.idCommande == _id
                                      select c).FirstOrDefault();

                context.painCommande.RemoveRange(painCommandeDelete);
                context.commande.Remove(commandeDelete);

                context.SaveChanges();

                return new JsonResult(true);
            }
            catch(Exception e)
            {
                return new JsonResult(false);
            }
        }
    }
}
