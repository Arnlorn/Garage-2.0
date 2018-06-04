﻿using Garage_2._0.DataAccessLayer;
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
        private RegisterContext db = new RegisterContext(); // TOMAS HÄR ÄR DEN NYA

        // GET: ParkedVehicles
        public ActionResult Index()
        {
            return View(db.parkedVehicles.ToList());
        }

        public ActionResult Filter(string filterString)
        {
            if (filterString== null)
            {
                filterString = "Default";
            }
            //var mytype = (Types) Enum.Parse(typeof(Types), filterString);
            Enum.TryParse<Types>(filterString, true, out Types mytype);
            
            var regNr = db.parkedVehicles
                .Where(e => e.RegNr.Contains(filterString) || e.Color.Contains(filterString)
                            || e.Make.Contains(filterString) || e.Model.Contains(filterString)
                            || e.Type == mytype)
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

           // var regNr = db.parkedVehicles.FirstOrDefault(i => i.RegNr == id);

            return View(regNr);
        }

        // GET: ParkedVehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.parkedVehicles.Find(id);
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
                db.parkedVehicles.Add(parkedVehicle);
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
            ParkedVehicle parkedVehicle = db.parkedVehicles.Find(id);
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
            ParkedVehicle parkedVehicle = db.parkedVehicles.Find(id);
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
            ParkedVehicle parkedVehicle = db.parkedVehicles.Find(id);
            db.parkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public ActionResult Statistic()
        {
            var statisticDictionary = new Dictionary<Types, int>();
            int numberOfWheels;

            foreach (var vehicle in db.parkedVehicles)
            {
                if (!statisticDictionary.ContainsKey(vehicle.Type))
                {
                    statisticDictionary.Add(vehicle.Type, 1);
                }
                else
                {
                    statisticDictionary[vehicle.Type] += 1;
                }


            }

           

            return 


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
