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
    public class TerminosController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Terminos
        public IQueryable<Terminos> GetTerminos()
        {
            return db.Terminos;
        }

        // GET: api/Terminos/5
        [ResponseType(typeof(Terminos))]
        public IHttpActionResult GetTerminos(int id)
        {
            Terminos terminos = db.Terminos.Find(id);
            if (terminos == null)
            {
                return NotFound();
            }

            return Ok(terminos);
        }

        // PUT: api/Terminos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTerminos(int id, Terminos terminos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != terminos.Id)
            {
                return BadRequest();
            }

            db.Entry(terminos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminosExists(id))
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

        // POST: api/Terminos
        [ResponseType(typeof(Terminos))]
        public IHttpActionResult PostTerminos(Terminos terminos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Terminos.Add(terminos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TerminosExists(terminos.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = terminos.Id }, terminos);
        }

        // DELETE: api/Terminos/5
        [ResponseType(typeof(Terminos))]
        public IHttpActionResult DeleteTerminos(int id)
        {
            Terminos terminos = db.Terminos.Find(id);
            if (terminos == null)
            {
                return NotFound();
            }

            db.Terminos.Remove(terminos);
            db.SaveChanges();

            return Ok(terminos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerminosExists(int id)
        {
            return db.Terminos.Count(e => e.Id == id) > 0;
        }
    }
}