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
    public class ParentsController : Controller
    {
        private Kid_GardenEntities db = new Kid_GardenEntities();

        // GET: Parents
        public async Task<ActionResult> Index(int id)
        {
            var parents = db.Parents.Where(x=>x.Podrazdelenie_Number==id).Include(p => p.Podrazdelenie);
            return View(await parents.ToListAsync());
        }

        // GET: Parents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            ViewBag.Podrazdelenie_Number = new SelectList(db.Podrazdelenies, "Podrazdelenie_Code", "Podrazdelenie_Name");
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_Tabel_number,Parent_FIO,Podrazdelenie_Number")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Parents.Add(parent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index/"+parent.Podrazdelenie_Number);
            }

            ViewBag.Podrazdelenie_Number = new SelectList(db.Podrazdelenies, "Podrazdelenie_Code", "Podrazdelenie_Name", parent.Podrazdelenie_Number);
            return View(parent);
        }

        // GET: Parents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Podrazdelenie_Number = new SelectList(db.Podrazdelenies, "Podrazdelenie_Code", "Podrazdelenie_Name", parent.Podrazdelenie_Number);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_Tabel_number,Parent_FIO,Podrazdelenie_Number")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Podrazdelenie_Number = new SelectList(db.Podrazdelenies, "Podrazdelenie_Code", "Podrazdelenie_Name", parent.Podrazdelenie_Number);
            return View(parent);
        }

        // GET: Parents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parent parent = await db.Parents.FindAsync(id);
            db.Parents.Remove(parent);
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
