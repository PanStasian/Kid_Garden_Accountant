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
    public class Kid_GardenController : Controller
    {
        private Kid_GardenEntities db = new Kid_GardenEntities();

        // GET: Kid_Garden
        public async Task<ActionResult> Index()
        {
            return View(await db.Kid_Gardens.ToListAsync());
        }

      


        // GET: Kid_Garden/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kid_Garden kid_Garden = await db.Kid_Gardens.FindAsync(id);
            if (kid_Garden == null)
            {
                return HttpNotFound();
            }
            return View(kid_Garden);
        }

        // GET: Kid_Garden/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kid_Garden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Kid_Garden_Number,Kid_Garden_Name,Pay_Sum,Address")] Kid_Garden kid_Garden)
        {
            if (ModelState.IsValid)
            {
                db.Kid_Gardens.Add(kid_Garden);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kid_Garden);
        }

        // GET: Kid_Garden/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kid_Garden kid_Garden = await db.Kid_Gardens.FindAsync(id);
            if (kid_Garden == null)
            {
                return HttpNotFound();
            }
            return View(kid_Garden);
        }

        // POST: Kid_Garden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Kid_Garden_Number,Kid_Garden_Name,Pay_Sum,Address")] Kid_Garden kid_Garden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kid_Garden).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kid_Garden);
        }

        // GET: Kid_Garden/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kid_Garden kid_Garden = await db.Kid_Gardens.FindAsync(id);
            if (kid_Garden == null)
            {
                return HttpNotFound();
            }
            return View(kid_Garden);
        }

        // POST: Kid_Garden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Kid_Garden kid_Garden = await db.Kid_Gardens.FindAsync(id);
            db.Kid_Gardens.Remove(kid_Garden);
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
