using back.dbContext;
using back.table;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using back.dialogueBD;
using back.model;
using back.Classe;
using Microsoft.Extensions.Configuration;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandeController : Controller
    {
        private readonly IConfiguration config;

        public CommandeController(IConfiguration _config, DataContext _context)
        {
            config = _config;
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

                const string SUJET = "Confirmation de la commande";
                const string MESSAGE = "Bonjour, \n votre commande à bien été enregistré. \n " +
                                        "Un mail vous sera envoyé au moment ou votre commande sera prete. \n " +
                                        "Cordialement";

                EnvoieMail(_commande.mailUtilisateur, SUJET, MESSAGE);

                return new JsonResult("ok");
            }
            catch (Exception)
            {
                return new JsonResult("erreur");
            }
        }

        [HttpPost("commandeTerminer")]
        public JsonResult CommandeTerminer(CommandeTermineModel _commande)
        {
            try
            {
                D_Commande.SupprimerCommande(_commande.idCommande);

                string _sujet = "Commande terminer";
                string _message = "Bonjour, \n Votre commande est terminer et va etre livrée. \n Merci de votre confiance. \n a bientôt";

                EnvoieMail(_commande.mailUtilisateur, _sujet, _message);

                return new JsonResult("OK");
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
            catch(Exception)
            {
                return new JsonResult(false);
            }
        }

        private void EnvoieMail(string _destinataire, string _sujet, string _message)
        {
            Mail _mail = new Mail(config["parametreMail:from"], config["parametreMail:mdp"], _destinataire, _sujet, _message);
            _mail.EnvoieMail();
        }
    }
}
