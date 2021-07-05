﻿using back.Classe;
using back.dbContext;
using back.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        }

        [HttpPost("ajouter")]
        public JsonResult AjouterPain(Pain[] _pain)
        {
            try
            {
                foreach (Pain item in _pain)
                {
                    context.pain.Add(item);
                }

                context.SaveChanges();

                return new JsonResult("Ajouts faient");
            }
            catch (Exception e)
            {
                return new JsonResult("pas de connexion");
            }
        }

        [HttpGet("lister")]
        public JsonResult Lister()
        {
            var listeReturn = from pain in context.pain orderby pain.nomPain descending
                              select pain;

            return new JsonResult(listeReturn);
        }

        [HttpPut("modifier")]
        public JsonResult Modifier(Pain _pain)
        {
            try
            {
                context.pain.Update(_pain);
                context.SaveChanges();

                return new JsonResult("fait");
            }
            catch(Exception e)
            {
                return new JsonResult("existe plus");
            }
        }

        [HttpDelete("supprimer/{_id}")]
        public JsonResult Supprimer(int _id)
        {
            var painDelete = (from pain in context.pain
                              where pain.idPain == _id
                              select pain).FirstOrDefault();

            try
            {
                context.pain.Remove(painDelete);
                context.SaveChanges();

                return new JsonResult("fait");
            }
            catch(Exception e)
            {
                return new JsonResult("existe plus");
            }
        }

    }
}
