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
    public class ChatsController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Chats
        public IQueryable<Chats> GetChats()
        {
            return db.Chats;
        }

        // GET: api/Chats/5
        [ResponseType(typeof(Chats))]
        public IHttpActionResult GetChats(int id)
        {
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return NotFound();
            }

            return Ok(chats);
        }

        [HttpGet]
        [Route("api/Chats/ChatsByIdUsuario/{IdUsuario}")]
        public List<ChatsViewModel> ChatsByIdUsuario(int IdUsuario)
        {
            List<Chats> listChat = db.Chats.Where(x => x.Usuario_Emisor == IdUsuario || x.Usuario_Receptor == IdUsuario).ToList();
            if (listChat == null)
            {
                return null;
            }
            List<ChatsViewModel> listVM = new List<ChatsViewModel>();
            foreach (var item in listChat)
            {

                listVM.Add(new ChatsViewModel
                {
                    Id = item.Id,
                    Usuario_Emisor = item.Usuario_Emisor.Value,
                    NombreUsuarioEmisor = item.Usuarios1.Nombre,
                    ImgUsuarioEmisor = item.Usuarios1.Imagen,
                    Usuario_Receptor = item.Usuario_Receptor.Value,
                    NombreUsuarioReceptor = item.Usuarios.Nombre,
                    ImgUsuarioReceptor = item.Usuarios.Imagen,
                    Ultimo_Mensaje = item.Ultimo_Mensaje,
                    Fecha_Ultimo_Mensaje = item.Fecha_Ultimo_Mensaje.Value
                });
            }


            return listVM;
        }

        // PUT: api/Chats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChats(int id, Chats chats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chats.Id)
            {
                return BadRequest();
            }

            db.Entry(chats).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatsExists(id))
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

        // POST: api/Chats
        [ResponseType(typeof(Chats))]
        public IHttpActionResult PostChats(Chats chats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            chats.Fecha_Ultimo_Mensaje = DateTime.Now;
            db.Chats.Add(chats);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ChatsExists(chats.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = chats.Id }, chats);
        }

        // DELETE: api/Chats/5
        [ResponseType(typeof(Chats))]
        public IHttpActionResult DeleteChats(int id)
        {
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return NotFound();
            }

            db.Chats.Remove(chats);
            db.SaveChanges();

            return Ok(chats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatsExists(int id)
        {
            return db.Chats.Count(e => e.Id == id) > 0;
        }
    }
}