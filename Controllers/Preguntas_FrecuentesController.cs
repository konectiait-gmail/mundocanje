using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MundoCanjeWeb.Models;
using System.Web.Http.Cors;

namespace MundoCanjeWeb.Controllers
{
    [EnableCors(origins: "http://mundocanje.tk,http://localhost:51199,http://localhost:8100,http://localhost:8000", headers: "*", methods: "*")]
    //[EnableCors(origins: "http://w1021886.ferozo.com", headers: "*", methods: "*")]
    public class Preguntas_FrecuentesController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Preguntas_Frecuentes
        public IQueryable<Preguntas_Frecuentes> GetPreguntas_Frecuentes()
        {
            return db.Preguntas_Frecuentes;
        }

        // GET: api/Preguntas_Frecuentes/5
        [ResponseType(typeof(Preguntas_Frecuentes))]
        public IHttpActionResult GetPreguntas_Frecuentes(int id)
        {
            Preguntas_Frecuentes preguntas_Frecuentes = db.Preguntas_Frecuentes.Find(id);
            if (preguntas_Frecuentes == null)
            {
                return NotFound();
            }

            return Ok(preguntas_Frecuentes);
        }

        // PUT: api/Preguntas_Frecuentes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreguntas_Frecuentes(int id, Preguntas_Frecuentes preguntas_Frecuentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preguntas_Frecuentes.Id)
            {
                return BadRequest();
            }

            db.Entry(preguntas_Frecuentes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Preguntas_FrecuentesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Preguntas_Frecuentes
        [ResponseType(typeof(Preguntas_Frecuentes))]
        public IHttpActionResult PostPreguntas_Frecuentes(Preguntas_Frecuentes preguntas_Frecuentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preguntas_Frecuentes.Add(preguntas_Frecuentes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Preguntas_FrecuentesExists(preguntas_Frecuentes.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = preguntas_Frecuentes.Id }, preguntas_Frecuentes);
        }

        // DELETE: api/Preguntas_Frecuentes/5
        [ResponseType(typeof(Preguntas_Frecuentes))]
        public IHttpActionResult DeletePreguntas_Frecuentes(int id)
        {
            Preguntas_Frecuentes preguntas_Frecuentes = db.Preguntas_Frecuentes.Find(id);
            if (preguntas_Frecuentes == null)
            {
                return NotFound();
            }

            db.Preguntas_Frecuentes.Remove(preguntas_Frecuentes);
            db.SaveChanges();

            return Ok(preguntas_Frecuentes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool Preguntas_FrecuentesExists(int id)
        {
            return db.Preguntas_Frecuentes.Count(e => e.Id == id) > 0;
        }
    }
}