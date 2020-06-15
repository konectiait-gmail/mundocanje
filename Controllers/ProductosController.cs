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
    public class ProductosController : ApiController
    {
        private MundoCanjeDBEntities db = new MundoCanjeDBEntities();

        // GET: api/Productos
        public IQueryable<Productos> GetProductos()
        {
            return db.Productos;
        }

        // GET: api/Productos/5
        //[ResponseType(typeof(Productos))]
        //public IHttpActionResult GetProductos(int id)
        //{
        //    Productos productos = db.Productos.Find(id);
        //    if (productos == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(productos);
        //}
        public ItemVM GetProductos(int id)
        {
            Productos objProd = db.Productos.Find(id);
            int nroSolicitudes = objProd.Pedidos.Count;
            if (objProd == null)
            {
                return null;
            }

            ItemVM listVM = new ItemVM()
            {
                Id = objProd.Id,
                Nombre = objProd.Nombre,
                Descripcion = string.IsNullOrEmpty(objProd.Descripcion) ? "Palermo. Buenos Aires" : objProd.Descripcion,
                IdTipo = objProd.IdTipo,
                Fecha_Publicacion = objProd.Fecha_Publicacion,
                Ult_Dias = (int)DateTime.Now.Subtract(objProd.Fecha_Publicacion.Value).TotalDays,
                Imagen = objProd.Imagen,
                Categoria = objProd.Categorias.Nombre,
                IdCategoria = objProd.IdCategoria.ToString(),
                Importe = objProd.Importe.ToString(),
                lat = string.IsNullOrEmpty(objProd.lat) ? "0" : objProd.lat,
                lng = string.IsNullOrEmpty(objProd.lng) ? "0" : objProd.lng,
                NroSolicitudes = nroSolicitudes.ToString(),
                Usuario = objProd.Usuarios.Nombre,
                UsuarioImagen = objProd.Usuarios.Imagen,
                IdProductoUsuario = objProd.Usuarios.Id.ToString()
            };
            

            return listVM;
        }

        [HttpGet]
        [Route("api/productos/ProductsByUser/{idUsuario}")]
        public List<ItemVM> ProductsByUser(string idUsuario)
        {
            List<Productos> listaProductos = db.Productos.Where(x => x.IdUsuario.ToString().Contains(idUsuario)).ToList();

            if (listaProductos == null)
            {
                return null;
            }

            List<ItemVM> listVM = new List<ItemVM>();
            foreach (var item in listaProductos)
            {
                /*
               listVM.Add(new ProductoViewModel
               {
                   Id = item.Id,
                   Nombre = item.Nombre,
                   Descripcion = item.Descripcion,
                   IdTipo = item.IdTipo,
                   IdEstado = item.IdEstado,
                   Importe = item.Importe,
                   Fecha_Publicacion = item.Fecha_Publicacion,
                   TipoDespublicacion = item.TipoDespublicacion,
                   IdCategoria = item.IdCategoria,
                   IdUsuario = item.IdUsuario,
                   Cantidad = item.Cantidad
               });
               */
                listVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = string.IsNullOrEmpty(item.Descripcion) ? "Palermo. Buenos Aires" : item.Descripcion,
                    IdTipo = item.IdTipo,
                    Fecha_Publicacion = item.Fecha_Publicacion,
                    Ult_Dias = (int)DateTime.Now.Subtract(item.Fecha_Publicacion.Value).TotalDays,
                    Imagen = item.Imagen,
                    Categoria = item.Categorias.Nombre,
                    IdCategoria = item.IdCategoria.ToString(),
                    Importe = item.Importe.ToString(),
                    lat = string.IsNullOrEmpty(item.lat) ? "0" : item.lat,
                    lng = string.IsNullOrEmpty(item.lng) ? "0" : item.lng
                });

            }

            return listVM;
        }

        [HttpGet]
        [Route("api/productos/ProductsByName/{Nombre}")]
        public List<ItemVM> ProductsByName(string Nombre)
        {
            List<Productos> listaProductos = db.Productos.Where(x => x.Nombre.ToUpper().ToString().Contains(Nombre.ToUpper())).ToList();

            if (listaProductos == null)
            {
                return null;
            }

            List<ItemVM> listVM = new List<ItemVM>();
            foreach (var item in listaProductos)
            {
               
                listVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = string.IsNullOrEmpty(item.Descripcion) ? "Palermo. Buenos Aires" : item.Descripcion,
                    IdTipo = item.IdTipo,
                    Fecha_Publicacion = item.Fecha_Publicacion,
                    Ult_Dias = (int)DateTime.Now.Subtract(item.Fecha_Publicacion.Value).TotalDays,
                    Imagen = item.Imagen,
                    Categoria = item.Categorias.Nombre,
                    IdCategoria = item.IdCategoria.ToString(),
                    Importe = item.Importe.ToString(),
                    lat = string.IsNullOrEmpty(item.lat) ? "0" : item.lat,
                    lng = string.IsNullOrEmpty(item.lng) ? "0" : item.lng
                });

            }

            return listVM;
        }



        [HttpGet]
        [Route("api/productos/HomeApp/")]
        public HomeViewModel HomeApp()
        {
            List<Productos> listaProductos = db.Productos.ToList();

            if (listaProductos == null)
            {
                return null;
            }

            List<Parametros> listaBanner = db.Parametros.Where(z=>z.Key== "home_banner").ToList();
            List<Productos> listaCanjes = listaProductos.Where(x => x.IdTipo == 1).OrderByDescending(x=> x.Id).Take(4).ToList();
            List<Productos> listaDescuentos = listaProductos.Where(x => x.IdTipo == 2).OrderByDescending(x => x.Id).Take(3).ToList();

            List<ItemVM> listItemBannerVM = new List<ItemVM>();
            List<ItemVM> listItemCanjeVM = new List<ItemVM>();
            List<ItemVM> listItemDescVM = new List<ItemVM>();
            HomeViewModel homeVM = new HomeViewModel();

            foreach (var item in listaBanner)
            {
                listItemBannerVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Key,
                    Descripcion = "",
                    IdTipo = 0,
                    Fecha_Publicacion = DateTime.Now,
                    Ult_Dias = 1,
                    Imagen = item.Value
                });

            }
            foreach (var item in listaCanjes)
            {
                listItemCanjeVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    //Descripcion = item.Descripcion,
                    Descripcion = "Palermo. Buenos Aires",
                    IdTipo = item.IdTipo,
                    Fecha_Publicacion = item.Fecha_Publicacion,
                    Ult_Dias = (int)DateTime.Now.Subtract(item.Fecha_Publicacion.Value).TotalDays,
                    Imagen = item.Imagen,
                    Categoria = item.Categorias.Nombre,
                    IdCategoria = item.IdCategoria.ToString(),
                    Importe = item.Importe.ToString(),
                    lat = string.IsNullOrEmpty(item.lat) ? "0" : item.lat,
                    lng = string.IsNullOrEmpty(item.lng) ? "0" : item.lng
                });

            }
            foreach (var item in listaDescuentos)
            {
                listItemDescVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    IdTipo = item.IdTipo,
                    Fecha_Publicacion = item.Fecha_Publicacion,
                    Ult_Dias = (int)DateTime.Now.Subtract(item.Fecha_Publicacion.Value).TotalDays,
                    Imagen = item.Imagen
                });

            }
            homeVM.Banners = listItemBannerVM;
            homeVM.Canjes = listItemCanjeVM;
            homeVM.Descuentos = listItemDescVM;


            return homeVM;
        }

        [HttpGet]
        [Route("api/productos/GetProductosByIdTipo/{idTipo}")]
        public List<ItemVM> GetProductosByIdTipo(int idTipo)
        {
            List<Productos> listaProductos = db.Productos.Where(x=> x.IdTipo== idTipo).ToList();

            if (listaProductos == null)
            {
                return null;
            }

            List<ItemVM> listItemProducVM = new List<ItemVM>();
           
            foreach (var item in listaProductos)
            {
                listItemProducVM.Add(new ItemVM
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = string.IsNullOrEmpty(item.Descripcion) ? "Palermo. Buenos Aires": item.Descripcion,
                    IdTipo = item.IdTipo,
                    Fecha_Publicacion = item.Fecha_Publicacion,
                    Ult_Dias = (int)DateTime.Now.Subtract(item.Fecha_Publicacion.Value).TotalDays,
                    Imagen = item.Imagen,
                    Categoria = item.Categorias.Nombre,
                    IdCategoria = item.IdCategoria.ToString(),
                    Importe = item.Importe.ToString(),
                    lat = string.IsNullOrEmpty(item.lat) ? "0" : item.lat,
                    lng = string.IsNullOrEmpty(item.lng) ? "0" : item.lng
                });

            }
            

            return listItemProducVM;
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductos(int id, Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productos.Id)
            {
                return BadRequest();
            }

            db.Entry(productos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
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

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(productos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductosExists(productos.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productos.Id }, productos);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult DeleteProductos(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }

            db.Productos.Remove(productos);
            db.SaveChanges();

            return Ok(productos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductosExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }
    }
}