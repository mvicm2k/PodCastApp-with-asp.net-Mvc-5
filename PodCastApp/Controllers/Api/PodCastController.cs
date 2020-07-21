using PodCastApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PodCastApp.Controllers.Api
{
    public class PodCastController : ApiController
    {
        private readonly PodCastDbContext _db = new PodCastDbContext();
        // GET: api/PodCast
        public IHttpActionResult GetCustomers()
        {
            var podCasts = _db.PodCasts.OrderByDescending(p => p.Id).ToList();
            return Ok(podCasts);

        }

        // GET: api/PodCast
        public IHttpActionResult GetPodCastById(int id)
        {
            var podCast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
            if (podCast == null)
                return NotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            return Ok(podCast);

        }
        
        
        // POST: api/PodCast
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreatePodCast(PodCast podcast)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            podcast.dateUploaded = DateTime.Now;
            _db.PodCasts.Add(podcast);
            _db.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + podcast.Id), podcast);

        }

        

        // PUT: api/Podcast/5
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdatePodCast(int id, PodCast podcast)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var PodCastInDb = _db.PodCasts.FirstOrDefault(c => c.Id == id);
            if (PodCastInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            PodCastInDb.title= podcast.title;
            PodCastInDb.tag = podcast.tag;
            PodCastInDb.description = podcast.description;
            PodCastInDb.dateUploaded = podcast.dateUploaded;
            
            _db.SaveChanges();
            return Ok(podcast);
        }

        // DELETE: api/PodCast/5
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeletePodCast(int id, PodCast podcast)
        {
            var PodCastInDb = _db.PodCasts.FirstOrDefault(c => c.Id == id);
            if (PodCastInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _db.PodCasts.Remove(PodCastInDb);
            _db.SaveChanges();
            return Ok(podcast);
        }

 
    }
}
