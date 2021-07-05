#region using
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using back.Classe;
using System.Net.Mail;
using System.Net;
#endregion

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private MySqlConnection connection;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public DepartementController(IConfiguration _config, IWebHostEnvironment _env)
        {
            config = _config;
            env = _env;

            // bdd => connexion a la BDD mySQL
            connection = new MySqlConnection(_config.GetConnectionString("bdd"));
        }

        // methode GET + route => Departement/lister
        [HttpGet("lister")]
        public JsonResult ListeDepartement()
        {
            List<dynamic> liste = new List<dynamic>();

            connection.Open();

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM departement";

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            DataTable _table = new DataTable();

            using(var reader = cmd.ExecuteReader())
            {
                // resultat de la requete SQL
                _table.Load(reader);

                reader.Close();
                connection.Close();
            }

            // parcours le resultat de la requete dans un DataTable
            foreach (DataRow item in _table.Rows)
            {
                //liste.Add(new Departement(int.Parse(item["idDep"].ToString()), item["nomDep"].ToString()));
            }

            return new JsonResult(liste);
        }


        // envoie JSON sans type
        // POST par defaut => /Departement + JSON
        [HttpPost]
        public JsonResult post(dynamic obj)
        {
            // tableau de valeur dans JSON
            /*
             * nb element dans tableau 
             * obj.liste.Count
             * "liste": [1, 2, 3, 23, 56]
             */

            // acces aux elements du JSON
            /*
             * "nom": "jetonpeche"
             * obj.nom
             */

            // parcours le tableau dans le JSON
            List<string> listeReturn = new List<string>();
            foreach(string item in obj.liste)
            {
                listeReturn.Add(item);
            }

            return new JsonResult(listeReturn);
        }

        [HttpPost("saveFichier")]
        public JsonResult post2()
        {
            string[] listeExtension = { ".pdf", ".png" };

            // Request.Form => $_POST en PHP
            var httpRequest = Request.Form;

            // httpRequest.Files[0] => $_FILES
            IFormFile fichier = httpRequest.Files[0];

            string msg = Protection.Image(fichier, 8000, listeExtension);

            if (msg == "ok")
            {
                string nomFichier = fichier.FileName.Trim();

                var cheminDossier = env.ContentRootPath + "/image/" + nomFichier;

                // upload dans le dossier image
                using (var stream = new FileStream(cheminDossier, FileMode.Create))
                {
                    fichier.CopyTo(stream);
                }
            }
            else
            {
                return new JsonResult(msg);
            }

            // convertion string en tableau int
            // httpRequest["t"] => $_POST["t"]
            int[] res = JsonConvert.DeserializeObject<int[]>(httpRequest["t"]);

            foreach (var item in res)
            {

            }

            // equivalent isset() PHP
            if(String.IsNullOrEmpty(httpRequest["r"]))
            {
                return new JsonResult("existe pas");
            }
            else
            {
                return new JsonResult("existe");
            }
        }

        [HttpPost("envoieMail")]
        public void post3(dynamic _mail)
        {
            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(config["parametreMail:from"], config["parametreMail:mdp"])
            };

            MailMessage msg = new MailMessage(Protection.XSS(_mail.expediteur), Protection.XSS(_mail.destinataire), _mail.sujet, _mail.message)
            {
                Priority = MailPriority.Normal,
                BodyEncoding = System.Text.Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(msg);

            return;
        }
    }
}
