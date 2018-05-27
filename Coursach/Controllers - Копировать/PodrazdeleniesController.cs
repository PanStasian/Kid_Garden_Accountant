using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coursach;

namespace Coursach.Controllers
{
    public class PodrazdeleniesController : Controller
    {
        private Kid_GardenEntities db = new Kid_GardenEntities();

        // GET: Podrazdelenies
        public async Task<ActionResult> Index()
        {
            return View(await db.Podrazdelenies.ToListAsync());
        }

        // GET: Podrazdelenies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podrazdelenie podrazdelenie = await db.Podrazdelenies.FindAsync(id);
            if (podrazdelenie == null)
            {
                return HttpNotFound();
            }
            return View(podrazdelenie);
        }

        // GET: Podrazdelenies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Podrazdelenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Podrazdelenie_Code,Podrazdelenie_Name")] Podrazdelenie podrazdelenie)
        {
            if (ModelState.IsValid)
            {
                db.Podrazdelenies.Add(podrazdelenie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(podrazdelenie);
        }

        // GET: Podrazdelenies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podrazdelenie podrazdelenie = await db.Podrazdelenies.FindAsync(id);
            if (podrazdelenie == null)
            {
                return HttpNotFound();
            }
            return View(podrazdelenie);
        }

        // POST: Podrazdelenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Podrazdelenie_Code,Podrazdelenie_Name")] Podrazdelenie podrazdelenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(podrazdelenie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(podrazdelenie);
        }

        // GET: Podrazdelenies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podrazdelenie podrazdelenie = await db.Podrazdelenies.FindAsync(id);
            if (podrazdelenie == null)
            {
                return HttpNotFound();
            }
            return View(podrazdelenie);
        }

        // POST: Podrazdelenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Podrazdelenie podrazdelenie = await db.Podrazdelenies.FindAsync(id);
            db.Podrazdelenies.Remove(podrazdelenie);
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
