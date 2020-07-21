using Microsoft.AspNet.Identity.EntityFramework;
using PodCastApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PodCastApp.Controllers
{
    [Authorize]
    public class PortalController : Controller
    {
        private readonly PodCastDbContext _db = new PodCastDbContext();
        private readonly ApplicationDbContext _conn = new ApplicationDbContext();
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole identity)
        {

            _conn.Roles.Add(identity);
            _conn.SaveChanges();
            RedirectToAction("Create");

            return View();
        }

// GET: Portal

            public ViewResult Index()
        {
            var podcasts = _db.PodCasts.OrderByDescending(p => p.Id).ToList();
            if (User.IsInRole("Admin"))
                return View("Admin", podcasts);

            return View("Users", podcasts);
        }
        /*
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            var podcasts = _db.PodCasts.OrderByDescending(p => p.Id).ToList();
            return View(podcasts);
        }
        [Authorize(Roles = "User")]
        public ActionResult Users()
        {
            var podcasts = _db.PodCasts.OrderByDescending(p => p.Id).ToList();
            return View(podcasts);
        }
        */
        [Authorize(Roles = "Admin")]
        // GET: Portal/Details/5
        public ActionResult PodCastDetails(int id)
        {
            PodCast podcast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
            if (podcast == null)
                return HttpNotFound();
            return View(podcast);
            
        }

        [Authorize(Roles = "Admin")]
        // GET: Portal/Create
        public ActionResult CreatePodCast()
        {
            return View();
        }

        public string UploadFile(HttpPostedFileBase file)
        {
            var files = Request.Files;

            if (files != null)
            {

                if (file != null && file.ContentLength < 104857600)
                {
                    // get a stream
                    var path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), file.FileName);
                    var dbPath = "Content/UploadedFiles/" + file.FileName;
                    file.SaveAs(path);
                    return dbPath;
                }
            }
            return null;
        }

        // POST: Portal/Create
        [HttpPost]
        public ActionResult CreatePodCast(PodCast podcast)
        {
            try
            {
                // TODO: Add insert logic here
                podcast.file = UploadFile(Request.Files["model.file"]);
                podcast.dateUploaded = DateTime.Now;
                _db.PodCasts.Add(podcast);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Portal/Edit/5
        public ActionResult EditPodCast(int id)
        {
            PodCast podcast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
            if (podcast == null)
            {
                return HttpNotFound();
            }
            return View(podcast);
        }

        // POST: Portal/Edit/5
        [HttpPost]
        public ActionResult EditPodCast(int id, PodCast model)
        {
           
            try
            {
                PodCast podcast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
                var newFile = UploadFile(Request.Files["model.file"]);
                if (newFile != null)
                {
                    podcast.file = newFile;
                }
                else
                {
                    model.file = podcast.file;

                }
                // TODO: Add update logic here
                podcast.dateUploaded = podcast.dateUploaded;
                podcast.tag = model.tag;
                podcast.title = model.title;
                podcast.description = model.description;
                if (TryUpdateModel(model))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Portal/Delete/5
        public ActionResult DeletePodCast(int id)
        {
            PodCast podcast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
            if (podcast == null)
            {
                return HttpNotFound();
            }
            return View(podcast);
        }

        // POST: Portal/Delete/5
        [HttpPost]
        public ActionResult DeletePodCast(int id, PodCast model)
        {
            PodCast podcast = _db.PodCasts.FirstOrDefault(p => p.Id == id);
            try
            {
                // TODO: Add delete logic here
                _db.PodCasts.Remove(podcast);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
