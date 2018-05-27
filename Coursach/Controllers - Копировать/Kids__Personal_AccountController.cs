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
    public class Kids__Personal_AccountController : Controller
    {
        private Kid_GardenEntities db = new Kid_GardenEntities();

        // GET: Kids__Personal_Account
        public async Task<ActionResult> Index(int id)
        {
            var kids__Personal_Account = db.Kids__Personal_Account.Where(x=>x.Kid_Garden_Number==id).Include(k => k.Kid_Garden).Include(k => k.Parent);
            return View(await kids__Personal_Account.ToListAsync());
        }

        // GET: Kids__Personal_Account/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kids__Personal_Account kids__Personal_Account = await db.Kids__Personal_Account.FindAsync(id);
            if (kids__Personal_Account == null)
            {
                return HttpNotFound();
            }
            return View(kids__Personal_Account);
        }

        // GET: Kids__Personal_Account/Create
        public ActionResult Create()
        {
            ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name");
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO");
            return View();
        }

        // POST: Kids__Personal_Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_Personal_Account,Kid_Garden_Number,Parent_Tabel_Number,Kid_FIO,Paid")] Kids__Personal_Account kids__Personal_Account)
        {
            if (ModelState.IsValid)
            {
                db.Kids__Personal_Account.Add(kids__Personal_Account);
                await db.SaveChangesAsync();
                return RedirectToAction("Index/"+kids__Personal_Account.Kid_Garden_Number);
            }

            ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name", kids__Personal_Account.Kid_Garden_Number);
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", kids__Personal_Account.Parent_Tabel_Number);
            return View(kids__Personal_Account);
        }

        

        // GET: Kids__Personal_Account/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kids__Personal_Account kids__Personal_Account = await db.Kids__Personal_Account.Where(x=>x.ID_Personal_Account == id).Include(x=>x.Parent).Include(x=>x.Visit_Tabel).FirstAsync();
            if (kids__Personal_Account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name", kids__Personal_Account.Kid_Garden_Number);
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", kids__Personal_Account.Parent_Tabel_Number);
            ViewBag.Visit_Tabel = new SelectList(db.Visit_Tabel, "ID_Visit_Tabel", "Visit_Days_Amount", kids__Personal_Account.Visit_Tabel);
            ViewBag.Month = new SelectList(db.Visit_Tabel, "ID_Visit_Tabel", "Month_Code", kids__Personal_Account.Visit_Tabel);
            return View(kids__Personal_Account);
        }

        // POST: Kids__Personal_Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_Personal_Account,ID_Tabel_number,ID_Visit_Tabel,Kid_Garden_Number,Parent_Tabel_Number,Kid_FIO,Paid")] Kids__Personal_Account kids__Personal_Account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kids__Personal_Account).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index/"+kids__Personal_Account.Kid_Garden_Number);
            }
            ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name", kids__Personal_Account.Kid_Garden_Number);
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", kids__Personal_Account.Parent_Tabel_Number);
            ViewBag.Visit_Tabel = new SelectList(db.Visit_Tabel, "ID_Visit_Tabel", "Visit_Days_Amount", kids__Personal_Account.Visit_Tabel);
            ViewBag.Month = new SelectList(db.Visit_Tabel, "ID_Visit_Tabel", "Month", kids__Personal_Account.Visit_Tabel);
            return View(kids__Personal_Account);
        }

        // GET: Kids__Personal_Account/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kids__Personal_Account kids__Personal_Account = await db.Kids__Personal_Account.FindAsync(id);
            if (kids__Personal_Account == null)
            {
                return HttpNotFound();
            }
            return View(kids__Personal_Account);
        }

        // POST: Kids__Personal_Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Kids__Personal_Account kids__Personal_Account = await db.Kids__Personal_Account.FindAsync(id);
            db.Kids__Personal_Account.Remove(kids__Personal_Account);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Summary(int id)
        {
            var kid = db.Visit_Tabel.Where(x => x.Personal_Account_ID == id).Include(x => x.Month).Include(x=>x.Kids__Personal_Account).ToList();
            double count = 0;
            //
            Dictionary<int,string> sum = new Dictionary<int,string>();
            int i = 0;
            foreach(var month in kid)
            {
                count = month.Kids__Personal_Account.Kid_Garden.Pay_Sum / (month.Month.Work_Days_Amount) * month.Visit_Days_Amount;
                string summa = count.ToString("#.##");
                sum.Add(i,summa);
                i++;
            }
            
            ViewData["Sum"] = sum;
            return View(kid);
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
