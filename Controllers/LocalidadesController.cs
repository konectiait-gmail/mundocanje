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
    public class LocalidadesController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Localidades
        //public IQueryable<Localidades> GetLocalidades()
        //{
        //    return db.Localidades;
        //}
        public List<Models.CiudadesViewModel> GetLocalidades()
        {
            List<Localidades> listaLocal = db.Localidades.ToList();

            if (listaLocal == null)
            {
                return null;
            }

            List<CiudadesViewModel> listVM = new List<CiudadesViewModel>();
            foreach (var item in listaLocal)
            {
                listVM.Add(new CiudadesViewModel
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    IdPais = 1,
                    Pais = "Argentina"
                });

            }

            return listVM;
        }

        // GET: api/Localidades/5
        [ResponseType(typeof(Localidades))]
        public IHttpActionResult GetLocalidades(int id)
        {
            Localidades localidades = db.Localidades.Find(id);
            if (localidades == null)
            {
                return NotFound();
            }

            return Ok(localidades);
        }

        // PUT: api/Localidades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocalidades(int id, Localidades localidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != localidades.Id)
            {
                return BadRequest();
            }

            db.Entry(localidades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalidadesExists(id))
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

        // POST: api/Localidades
        [ResponseType(typeof(Localidades))]
        public IHttpActionResult PostLocalidades(Localidades localidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Localidades.Add(localidades);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = localidades.Id }, localidades);
        }

        // DELETE: api/Localidades/5
        [ResponseType(typeof(Localidades))]
        public IHttpActionResult DeleteLocalidades(int id)
        {
            Localidades localidades = db.Localidades.Find(id);
            if (localidades == null)
            {
                return NotFound();
            }

            db.Localidades.Remove(localidades);
            db.SaveChanges();

            return Ok(localidades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocalidadesExists(int id)
        {
            return db.Localidades.Count(e => e.Id == id) > 0;
        }
    }
}