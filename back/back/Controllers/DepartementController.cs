#region using
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using back.Classe;
using System.Data.SqlClient;
using back.dbContext;
#endregion

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private SqlConnection connection;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        private DataContext context;

        public DepartementController(IConfiguration _config, IWebHostEnvironment _env, DataContext _context)
        {
            config = _config;
            env = _env;
            context = _context;

            // bdd => connexion a la BDD SQL server
            connection = new SqlConnection(_config.GetConnectionString("bddServer"));
        }

        // methode GET + route => Departement/lister
        [HttpGet]
        public JsonResult ListeDepartement()
        {
            var res = from d in context.departement
                      select d;

            return new JsonResult(res);
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
    }
}
