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
using ApiNoticias.Models;

namespace ApiNoticias.Controllers
{
    public class NoticiasController : ApiController
    {
        private NoticiasEntities db = new NoticiasEntities();

        // GET: api/Noticias
        public IHttpActionResult GetNoticia()
        {
            return Ok(db.Noticia.ToList());
        }





        //[Route("api/Noticias/sapo")]
        //public IHttpActionResult GetNoticia2()
        //{
        //    return Ok(db.Noticia.ToList().Select(a => new
        //    {
        //        a.ID,
        //        FechaInicio = a.FechaInicio.Value.Date,
        //        FechaFin = a.FechaFin.Value.Date,
        //        a.Titulo,
        //        a.Contenido
        //    }));
        //}

        // GET: api/Noticias/5
        [ResponseType(typeof(Noticia))]
        public IHttpActionResult GetNoticia(int id)
        {
            Noticia noticia = db.Noticia.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }

            return Ok(noticia);
        }

        // PUT: api/Noticias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNoticia(int id, Noticia noticia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticia.ID)
            {
                return BadRequest();
            }

            db.Entry(noticia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticias
        [ResponseType(typeof(Noticia))]
        public IHttpActionResult PostNoticia(Noticia noticia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Noticia.Add(noticia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = noticia.ID }, noticia);
        }

        // DELETE: api/Noticias/5
        [ResponseType(typeof(Noticia))]
        public IHttpActionResult DeleteNoticia(int id)
        {
            Noticia noticia = db.Noticia.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }

            db.Noticia.Remove(noticia);
            db.SaveChanges();

            return Ok(noticia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoticiaExists(int id)
        {
            return db.Noticia.Count(e => e.ID == id) > 0;
        }
    }
}