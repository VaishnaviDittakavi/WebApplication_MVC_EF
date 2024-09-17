using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_MVC_EF;

namespace WebApplication_MVC_EF.Controllers
{
    public class locationsController : Controller
    {
        private HRDBEntities db = new HRDBEntities();

        // GET: locations
        public async Task<ActionResult> Index()
        {
            var locations = db.locations.Include(l => l.country);
            return View(await locations.ToListAsync());
        }

        // GET: locations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = await db.locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: locations/Create
        public ActionResult Create()
        {
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name");
            return View();
        }

        // POST: locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "location_id,street_address,postal_code,city,state_province,country_id")] location location)
        {
            if (ModelState.IsValid)
            {
                db.locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", location.country_id);
            return View(location);
        }

        // GET: locations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = await db.locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", location.country_id);
            return View(location);
        }

        // POST: locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "location_id,street_address,postal_code,city,state_province,country_id")] location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", location.country_id);
            return View(location);
        }

        // GET: locations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = await db.locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            location location = await db.locations.FindAsync(id);
            db.locations.Remove(location);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
