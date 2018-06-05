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
        private const int numberOfParkingSlots = 20;
        private RegisterContext db = new RegisterContext(); 

        // GET: ParkedVehicles
        public ActionResult Index()
        {
            return View(db.ParkedVehicles.ToList());
        }

        public ActionResult Filter(string FilterString)
        {
            var regNr = db.ParkedVehicles
                .Where(e => e.RegNr.Contains(FilterString) || e.Color.Contains(FilterString)
                            || e.Make.Contains(FilterString) || e.Model.Contains(FilterString))
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
                var parkedVehicles = db.ParkedVehicles.ToList();
                int[] Parkingslots = GetParkingSlots(parkedVehicles);
              
                for (int i = 0; i < numberOfParkingSlots; i++)
                {
                    if (parkedVehicle.Type == Types.Lastbil || parkedVehicle.Type == Types.Buss)
                    {
                        if (i == numberOfParkingSlots)
                        {
                            GarageLimit();
                            break;
                        } else
                        if (Parkingslots[i] == 0 && Parkingslots[i+1] == 0)
                        {
                            parkedVehicle.ParkingSlot = i;
                            Park_It(parkedVehicle);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    if (parkedVehicle.Type == Types.Flygplan)
                    {
                        if (i == numberOfParkingSlots || i == numberOfParkingSlots - 1)
                        {
                            GarageLimit();
                            break;
                        }
                        else
                        if (Parkingslots[i] == 0 && Parkingslots[i + 1] == 0 && Parkingslots[i + 2] == 0)
                        {
                            parkedVehicle.ParkingSlot = i;
                            Park_It(parkedVehicle);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    if (parkedVehicle.Type == Types.Personbil)
                    {
                        if (Parkingslots[i] == 0)
                        {
                            parkedVehicle.ParkingSlot = i;
                            Park_It(parkedVehicle);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    if (parkedVehicle.Type == Types.Motorcyckel)
                    {
                        if (Parkingslots[i] < 3)
                        {
                            parkedVehicle.ParkingSlot = i;
                            Park_It(parkedVehicle);
                            return RedirectToAction("Index");
                        }
                    }
                }
                GarageLimit();
            }
            return View(parkedVehicle);
        }

        private int[] GetParkingSlots(List<ParkedVehicle> parkedVehicles)
        {
            int[] Parkingslots = new int[numberOfParkingSlots];
            // foreach (var i in Parkingslots) { Parkingslots[i] = 0; } // Defaultvalue is already 0

            for (int i = 0; i < parkedVehicles.Count; i++)
            {

                if (parkedVehicles[i].Type == Types.Lastbil || parkedVehicles[i].Type == Types.Buss)
                {
                    Parkingslots[parkedVehicles[i].ParkingSlot] = 3;
                    Parkingslots[parkedVehicles[i].ParkingSlot + 1] = 3;
                }
                else
                if (parkedVehicles[i].Type == Types.Flygplan)
                {
                    Parkingslots[parkedVehicles[i].ParkingSlot] = 3;
                    Parkingslots[parkedVehicles[i].ParkingSlot + 1] = 3;
                    Parkingslots[parkedVehicles[i].ParkingSlot + 2] = 3;
                }
                else
                if (parkedVehicles[i].Type == Types.Personbil)
                {
                    Parkingslots[parkedVehicles[i].ParkingSlot] = 3;
                }
                else
                if (parkedVehicles[i].Type == Types.Motorcyckel)
                {
                    Parkingslots[parkedVehicles[i].ParkingSlot]++;
                }
            }
            return Parkingslots;
        }

        private void Park_It(ParkedVehicle parkedVehicle)
        {
            parkedVehicle.TimeStamp = DateTime.Now;
            db.ParkedVehicles.Add(parkedVehicle);
            db.SaveChanges();
        }

        private void GarageLimit()
        {
            Response.Write("<script type=\"text/javascript\">alert('No space for the choosen vehicle type');</script>");
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
                   TimeStamp = parkedVehicle.TimeStamp,
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
