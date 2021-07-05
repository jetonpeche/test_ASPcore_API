using back.Classe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OutilController : ControllerBase
    {
        private readonly MySqlConnection connection;

        public OutilController(IConfiguration _config)
        {
            connection = new MySqlConnection(_config.GetConnectionString("bdd"));
        }

        [HttpGet("lister")]
        public JsonResult Get()
        {
            connection.Open();

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM outils";

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            DataTable _table = new DataTable();

            using (var reader = cmd.ExecuteReader())
            {
                _table.Load(reader);

                reader.Close();
                connection.Close();
            }

            return new JsonResult(_table);
        }

        [HttpPost("ajouter")]
        public JsonResult Post(dynamic _outil)
        {
            connection.Open(); 

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO outils (nomOutil) VALUES (@nom)";

            cmd.Parameters.AddWithValue("@nom", Protection.XSS(_outil.nomOutil));

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            connection.Close();

            return new JsonResult("Ajouté");
        }

        [HttpPost("modifier")]
        public JsonResult Post2(dynamic _outil)
        {
            int _nb;

            if(!int.TryParse(_outil.idOutil, out _nb))
            {
                return new JsonResult("pas nombre");
            }
            else
            {
                connection.Open();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE outils SET nomOutil = @nom WHERE idOutil = @id";

                cmd.Parameters.AddWithValue("@nom", Protection.XSS(_outil.nomOutil));
                cmd.Parameters.AddWithValue("@id", _outil.idOutil);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                connection.Close();

                return new JsonResult("Modifier");
            }
        }
    }
}
