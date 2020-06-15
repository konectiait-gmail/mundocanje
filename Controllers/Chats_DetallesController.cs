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
    public class Chats_DetallesController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Chats_Detalles
        public IQueryable<Chats_Detalles> GetChats_Detalles()
        {
            return db.Chats_Detalles;
        }
        [HttpGet]
        [Route("api/Chats_Detalles/ChatsDetailsByIdUsuario/{IdEmisor}/{IdReceptor}")]
        public List<ChatsDetallesViewModel> ChatsDetailsByIdUsuario(int IdEmisor, int IdReceptor)
        {
            List<Chats_Detalles> listChatsDet = db.Chats_Detalles.Where(x => x.Usuario_Emisor == IdEmisor || x.Usuario_Receptor== IdEmisor).ToList();
            listChatsDet = listChatsDet.Where(x => x.Usuario_Emisor == IdReceptor || x.Usuario_Receptor == IdReceptor).ToList();
            if (listChatsDet == null)
            {
                return null;
            }
            List<ChatsDetallesViewModel> listVM = new List<ChatsDetallesViewModel>();
            foreach (var item in listChatsDet)
            {

                listVM.Add(new ChatsDetallesViewModel
                {
                    Id = item.Id,
                    Usuario_Emisor = item.Usuario_Emisor.Value,
                    NombreUsuarioEmisor = item.Usuarios.Nombre,
                    Usuario_Receptor = item.Usuario_Receptor.Value,
                    NombreUsuarioReceptor = item.Usuarios1.Nombre,
                    Mensaje = item.Mensaje,
                    Fecha_Mensaje = item.Fecha_Mensaje.Value,
                    IdChat = item.IdChat.Value
                });
            }


            return listVM;
        }

        // GET: api/Chats_Detalles/5
        [ResponseType(typeof(Chats_Detalles))]
        public IHttpActionResult GetChats_Detalles(int id)
        {
            Chats_Detalles chats_Detalles = db.Chats_Detalles.Find(id);
            if (chats_Detalles == null)
            {
                return NotFound();
            }

            return Ok(chats_Detalles);
        }

        // PUT: api/Chats_Detalles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChats_Detalles(int id, Chats_Detalles chats_Detalles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chats_Detalles.Id)
            {
                return BadRequest();
            }

            db.Entry(chats_Detalles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Chats_DetallesExists(id))
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

        // POST: api/Chats_Detalles
        [ResponseType(typeof(Chats_Detalles))]
        public IHttpActionResult PostChats_Detalles(Chats_Detalles chats_Detalles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            chats_Detalles.Fecha_Mensaje = DateTime.Now;
            db.Chats_Detalles.Add(chats_Detalles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Chats_DetallesExists(chats_Detalles.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = chats_Detalles.Id }, chats_Detalles);
        }

        // DELETE: api/Chats_Detalles/5
        [ResponseType(typeof(Chats_Detalles))]
        public IHttpActionResult DeleteChats_Detalles(int id)
        {
            Chats_Detalles chats_Detalles = db.Chats_Detalles.Find(id);
            if (chats_Detalles == null)
            {
                return NotFound();
            }

            db.Chats_Detalles.Remove(chats_Detalles);
            db.SaveChanges();

            return Ok(chats_Detalles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Chats_DetallesExists(int id)
        {
            return db.Chats_Detalles.Count(e => e.Id == id) > 0;
        }
    }
}