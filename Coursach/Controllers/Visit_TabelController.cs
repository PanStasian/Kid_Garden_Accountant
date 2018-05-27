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
    public class Visit_TabelController : Controller
    {
        private Kid_GardenEntities db = new Kid_GardenEntities();

        // GET: Visit_Tabel
        public ActionResult Index(int id)
        {
            var kid = db.Visit_Tabel.Where(x => x.Personal_Account_ID == id).Include(x => x.Month).Include(x => x.Kids__Personal_Account).ToList();
            double count = 0;
            
            Dictionary<int, string> sum = new Dictionary<int, string>();
            int i = 0;
            foreach (var month in kid)
            {
                count = month.Kids__Personal_Account.Kid_Garden.Pay_Sum / (month.Month.Work_Days_Amount) * month.Visit_Days_Amount;
                string summa = count.ToString("#.##");
                sum.Add(i, summa);
                i++;
            }

            ViewData["Sum"] = sum;
            return View(kid);
        }
        
        public async Task<ActionResult> Months(int id)
        {
            var months = await db.Visit_Tabel.Where(x => x.Kids__Personal_Account.Kid_Garden_Number == id).Include(x => x.Month).Take(12).ToListAsync();
            return View(months);
        }

        public ActionResult MonthSummary(int idGarden, int idMonth)
        {
            var summary = db.Visit_Tabel.Where(x => x.Kids__Personal_Account.Kid_Garden_Number == idGarden)
                                        .Where(x => x.Month_Code == idMonth).Include(x=>x.Kids__Personal_Account.Parent).ToList();
            double count = 0;

            Dictionary<int, string> sum = new Dictionary<int, string>();
            int i = 0;
            foreach (var kid in summary)
            {
                count = kid.Kids__Personal_Account.Kid_Garden.Pay_Sum / (kid.Month.Work_Days_Amount) * kid.Visit_Days_Amount;
                string summa = count.ToString("#.##");
                sum.Add(i, summa);
                i++;
            }

            ViewData["Sum"] = sum;
            
            return View(summary);
        }

        public ActionResult EditSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kids__Personal_Account = db.Visit_Tabel.Where(x => x.Kids__Personal_Account.ID_Personal_Account == id).FirstOrDefault();
            if (kids__Personal_Account == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name", kids__Personal_Account.Kid_Garden_Number);
            //ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", kids__Personal_Account.Parent_Tabel_Number);
            return View(kids__Personal_Account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSummary([Bind(Include = "ID_Personal_Account,Visit_Days_Amount,Paid")] Visit_Tabel kids__Personal_Account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kids__Personal_Account).State = EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Summary/" + kids__Personal_Account.Kids__Personal_Account.ID_Personal_Account);
            }
            //ViewBag.Kid_Garden_Number = new SelectList(db.Kid_Gardens, "Kid_Garden_Number", "Kid_Garden_Name", kids__Personal_Account.Kid_Garden_Number);
            //ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", kids__Personal_Account.Parent_Tabel_Number);
            return View(kids__Personal_Account);
        }

        // GET: Visit_Tabel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit_Tabel visit_Tabel = await db.Visit_Tabel.Where(x=>x.ID_Visit_Tabel == id).Include(x=>x.Kids__Personal_Account).FirstOrDefaultAsync();
            if (visit_Tabel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", visit_Tabel.Kids__Personal_Account.Parent_Tabel_Number);
            ViewBag.Personal_Account_ID = new SelectList(db.Kids__Personal_Account, "ID_Personal_Account", "Kid_FIO", visit_Tabel.Kids__Personal_Account);
            ViewBag.Paid = new SelectList(db.Kids__Personal_Account, "ID_Personal_Account", "Paid", visit_Tabel.Kids__Personal_Account);
            ViewBag.Month_Code = new SelectList(db.Months, "Month_Code", "Month_Name", visit_Tabel.Month.Month_Code);
            return View(visit_Tabel);
        }

        // POST: Visit_Tabel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_Visit_Tabel,Personal_Account_ID,ID_Personal_Account,ID_Tabel_number,Visit_Days_Amount,Month_Code,Paid")] Visit_Tabel visit_Tabel)
        {
            //visit_Tabel.Kids__Personal_Account = db.Kids__Personal_Account.Where(x => x.ID_Personal_Account == visit_Tabel.Personal_Account_ID).First(); 
            if (ModelState.IsValid)
            {
                db.Entry(visit_Tabel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index/" + visit_Tabel.Personal_Account_ID);
            }
            ViewBag.Parent_Tabel_Number = new SelectList(db.Parents, "ID_Tabel_number", "Parent_FIO", visit_Tabel.Personal_Account_ID);
            ViewBag.Personal_Account_ID = new SelectList(db.Kids__Personal_Account, "ID_Personal_Account", "Kid_FIO", visit_Tabel.Kids__Personal_Account);
            ViewBag.Month_Code = new SelectList(db.Months, "Month_Code", "Month_Name", visit_Tabel.Month);
            return View(visit_Tabel);
        }

        // GET: Visit_Tabel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit_Tabel visit_Tabel = await db.Visit_Tabel.FindAsync(id);
            if (visit_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(visit_Tabel);
        }

        // POST: Visit_Tabel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Visit_Tabel visit_Tabel = await db.Visit_Tabel.FindAsync(id);
            db.Visit_Tabel.Remove(visit_Tabel);
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
