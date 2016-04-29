using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cis237Assignment6.Models;

namespace cis237Assignment6.Controllers
{
    [Authorize]
    public class BeverageController : Controller
    {
        // Crea a new instance of the Beverage
        private BeveragePBathEntities db = new BeveragePBathEntities();
        // GET: Beverage
        public ActionResult Index()
        {
            DbSet<Beverage> BevToFilter = db.Beverages;
            String filterName = "";
            string filterPack = "";
            string filterMinPrice = "";
            string filterMaxPrice = "";

            decimal min = 0m;
            decimal max = 1000m;

            //Check to see if there is a value in the session, if there is assign it to the variable
                        // set up to hole value.
                        if (Session["name"] != null && !String.IsNullOrWhiteSpace((string)Session["name"]))
                            {
                filterName = (string)Session["name"];
                            }
                        if (Session["pack"] != null && !String.IsNullOrWhiteSpace((string)Session["pack"]))
                            {
                filterPack = (string)Session["pack"];
                            }
                        if (Session["min"] != null && !String.IsNullOrWhiteSpace((string)Session["min"]))
                            {
                filterMinPrice = (string)Session["min"];
                min = Convert.ToDecimal(filterMinPrice);
                            }
                        if (Session["max"] != null && !String.IsNullOrWhiteSpace((string)Session["max"]))
                            {
                filterMaxPrice = (string)Session["max"];
                max = Convert.ToDecimal(filterMaxPrice);
                            }
            
            ViewBag.filterName = filterName;
            ViewBag.filterPack = filterPack;
            ViewBag.filterMinPrice = filterMinPrice;
            ViewBag.filterMaxprice = filterMaxPrice;
            
            IEnumerable < Beverage > filtered = BevToFilter.Where(bev => bev.name.Contains(filterName) &&
   bev.pack.Contains(filterPack) &&
   bev.price >= min &&
   bev.price <= max);
            
            IEnumerable < Beverage > finalFiltered = filtered.ToList();
            
                        return View(finalFiltered);
                    }

        // GET: Beverages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: Beverages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Pack,Price,Active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Beverages.Add(beverage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Pack,Price,Active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beverage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
       }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
            db.SaveChanges();
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



        // FILTERS - ADDED
        [HttpPost, ActionName("Filter")]
        public ActionResult Filter()
        {
            string name = Request.Form.Get("name");
            string pack = Request.Form.Get("pack");
            string min = Request.Form.Get("min");
            string max = Request.Form.Get("max");

            Session["name"] = name;
            Session["pack"] = pack;
            Session["min"] = min;
            Session["max"] = max;

            return RedirectToAction("Index");
    	    }
       }
   }

