using back.Classe;
using back.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PainController : ControllerBase
    {
        private SqlConnection connection;
        private readonly IConfiguration config;

        public PainController(IConfiguration _config)
        {
            config = _config;

            // bdd => connexion a la BDD mySQL
            connection = new SqlConnection(_config.GetConnectionString("bddServer"));
        }

        [HttpPost("ajouter")]
        public JsonResult AjouterPain(Pain[] _pain)
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO pain (nomPain) VALUES (@nom)";
            

            foreach (Pain element in _pain)
            {   
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nom", Protection.XSS(element.nomPain));

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            connection.Close();

            return new JsonResult("Ajouté");
        }

        [HttpGet("lister")]
        public JsonResult Lister()
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM dbo.pain ORDER BY nomPain";

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            DataTable _table = new DataTable();

            using (var reader = cmd.ExecuteReader())
            {
                // resultat de la requete SQL
                _table.Load(reader);

                reader.Close();
                connection.Close();
            }

            return new JsonResult(_table);
        }

        [HttpPut("modifier")]
        public JsonResult Modifier(Pain _pain)
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE dbo.pain SET nomPain = @nom WHERE idPain = @id";

            cmd.Parameters.AddWithValue("@nom", Protection.XSS(_pain.nomPain));
            cmd.Parameters.AddWithValue("@id", _pain.idPain);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            connection.Close();

            return new JsonResult(true);
        }

        [HttpDelete("supprimer/{_id}")]
        public JsonResult Supprimer(int _id)
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM dbo.pain WHERE idPain = @id";

            cmd.Parameters.AddWithValue("@id", _id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            connection.Close();

            return new JsonResult(true);
        }

    }
}
