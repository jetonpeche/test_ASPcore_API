using back.Classe;
using back.dbContext;
using back.dialogueBD;
using back.table;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private DataContext context;
        private IConfiguration config;

        public UtilisateurController(IConfiguration _config, DataContext _context)
        {
            config = _config;
            context = _context;
            D_Utilisateur.context = context;
        }

        [HttpPost("inscription")]
        public JsonResult Inscription(Utilisateur _utilisateur)
        {
            try
            {
                bool ok = D_Utilisateur.Incription(_utilisateur);

                if (!ok)
                    return new JsonResult("le compte existe deja");
            }
            catch(Exception)
            {
                return new JsonResult("Pas d'inscription");
            }

            // envoie mail
            try
            {
                string _sujet = "Inscription";
                string _message = $"Bonjour, { _utilisateur.nomUtilisateur } { _utilisateur.prenomUtilisateur } \n \n  Confirmation de votre incription !";

                Mail mail = new Mail(config["parametreMail:from"], config["parametreMail:mdp"], _utilisateur.mailUtilisateur, _sujet, _message);
                mail.EnvoieMail();

                return new JsonResult("ok");
            }
            catch (Exception)
            {
                return new JsonResult("pas d'envoie de mail");
            }
        }

        [HttpPost("connexion")]
        public JsonResult Connexion(Utilisateur _utilisateur)
        {
            var resultat = D_Utilisateur.Connexion(_utilisateur);

            return new JsonResult(resultat);
        }
    }
}
