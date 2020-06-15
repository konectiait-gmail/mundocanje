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
    public class ParametrosController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Parametros
        public IQueryable<Parametros> GetParametros()
        {
            return db.Parametros;
        }

        // GET: api/Parametros/5
        [ResponseType(typeof(Parametros))]
        public IHttpActionResult GetParametros(int id)
        {
            Parametros parametros = db.Parametros.Find(id);
            if (parametros == null)
            {
                return NotFound();
            }

            return Ok(parametros);
        }

        // PUT: api/Parametros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParametros(int id, Parametros parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametros.Id)
            {
                return BadRequest();
            }

            db.Entry(parametros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrosExists(id))
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

        // POST: api/Parametros
        [ResponseType(typeof(Parametros))]
        public IHttpActionResult PostParametros(Parametros parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parametros.Add(parametros);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parametros.Id }, parametros);
        }

        // DELETE: api/Parametros/5
        [ResponseType(typeof(Parametros))]
        public IHttpActionResult DeleteParametros(int id)
        {
            Parametros parametros = db.Parametros.Find(id);
            if (parametros == null)
            {
                return NotFound();
            }

            db.Parametros.Remove(parametros);
            db.SaveChanges();

            return Ok(parametros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParametrosExists(int id)
        {
            return db.Parametros.Count(e => e.Id == id) > 0;
        }
    }
}