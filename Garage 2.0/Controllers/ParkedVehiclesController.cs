using Garage_2._0.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Garage_2._0.Models
{
    public class ParkedVehiclesController : Controller
    {
        private RegisterContext db = new RegisterContext(); 

        // GET: ParkedVehicles
        public ActionResult Index()
        {
            return View(db.ParkedVehicles.ToList());
        }

        public ActionResult Filter(string FilterString)
        {
            Enum.TryParse(FilterString, true, out Types type);
            var regNr = db.ParkedVehicles
                .Where(e => e.RegNr.Contains(FilterString) || e.Color.Contains(FilterString)
                            || e.Make.Contains(FilterString) || e.Model.Contains(FilterString)
                            || e.Type == type)
                .Select(e => new ParkedVehiclesViewModel()
                {
                    Id = e.Id,
                    Type = e.Type,
                    RegNr = e.RegNr,
                    Color = e.Color,
                    Make = e.Make,
                    Model = e.Model,
                    NrOfWheels = e.NrOfWheels,
                    TimeStamp = e.TimeStamp
                });

           // var regNr = db.ParkedVehicles.FirstOrDefault(i => i.RegNr == id);

            return View(regNr);
        }

        // GET: ParkedVehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,RegNr,Color,Make,Model,NrOfWheels,TimeStamp")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.TimeStamp = DateTime.Now;
                db.ParkedVehicles.Add(parkedVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,RegNr,Color,Make,Model,NrOfWheels,TimeStamp")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            RecieptViewModel CheckOutVehicle = new RecieptViewModel
            {
                   Id = parkedVehicle.Id,
                   Type = parkedVehicle.Type,
                   RegNr = parkedVehicle.RegNr,
                   TimeStamp = parkedVehicle.TimeStamp
            };
            return RedirectToAction("Reciept",  CheckOutVehicle );
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Reciept(RecieptViewModel checkOutVehicle)
        {
            if (ModelState.IsValid)
            {
                return View(checkOutVehicle);
            }
            return RedirectToAction("Index");
        }

        //GET
        public ActionResult Statistic()
        {
            var model = new StatisticViewModel();
            model.Dictionary = new Dictionary<string, double>();
            var numberOfWheels = 0;
            double TotalMony = 0;



            foreach (var vehicle in db.ParkedVehicles)
            {
                if (!model.Dictionary.ContainsKey(vehicle.Type.ToString()))
                {
                    model.Dictionary.Add(vehicle.Type.ToString(), 1);
                }
                else
                {
                    model.Dictionary[vehicle.Type.ToString()] += 1;
                }

                numberOfWheels += vehicle.NrOfWheels;

                TotalMony = Math.Round((DateTime.Now - vehicle.TimeStamp).TotalMinutes * 0.1,2);
            }

            model.Dictionary.Add("Number of all Wheels", numberOfWheels);
            model.Dictionary.Add("Total Mony", TotalMony);



            return View(model);
        }


        //}

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
