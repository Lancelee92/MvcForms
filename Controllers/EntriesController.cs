using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {
            var entry = new Entry()
            {
                Date =  DateTime.Today,
            };
            
            ViewBag.ActivitiesSelectListItems = new SelectList(Data.Data.Activities, "Id", "Name");
            return View(entry);
        }
        // [HttpPost]
        // public ActionResult Add(DateTime? date, int? activityId, double? duration, Entry.IntensityLevel? intensity, bool? exclude, string notes)
        // {
            
        //     //if(Request.Method == "POST"){}
        //     //string date = Request.Form["Date"];
        //     // ViewBag.Date = ModelState["Date"].AttemptedValue;
        //     // ViewBag.ActivityId = ModelState["ActivityId"].AttemptedValue;
        //     // ViewBag.Duration = ModelState["Duration"].AttemptedValue;
        //     // ViewBag.Intensity = ModelState["Intensity"].AttemptedValue;
        //     // ViewBag.Exclude = ModelState["Exclude"].AttemptedValue;
        //     // ViewBag.Notes = ModelState["Notes"].AttemptedValue;
            
        //     return View();
        // }
        [HttpPost]
        public ActionResult Add(Entry entry)
        {
            if(ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);

                return RedirectToAction("Index");
                //ToDo Display the entries list page
            }
            ViewBag.ActivitiesSelectListItems = new SelectList(Data.Data.Activities, "Id", "Name");
            
            //if(Request.Method == "POST"){}
            //string date = Request.Form["Date"];
            // ViewBag.Date = ModelState["Date"].AttemptedValue;
            // ViewBag.ActivityId = ModelState["ActivityId"].AttemptedValue;
            // ViewBag.Duration = ModelState["Duration"].AttemptedValue;
            // ViewBag.Intensity = ModelState["Intensity"].AttemptedValue;
            // ViewBag.Exclude = ModelState["Exclude"].AttemptedValue;
            // ViewBag.Notes = ModelState["Notes"].AttemptedValue;
            
            return View(entry);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}