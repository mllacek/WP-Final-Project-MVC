using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Project.Controllers
{
    using Final_Project.Models;

    public class ScoreSheetController : Controller
    {
        static List<FencerModel> fencers = new List<FencerModel>();

        // GET: /ScoreSheet/Index
        public ActionResult Index()
        {
            return View(fencers);
        }

        //// GET: /ScoreSheet/Fencers
        //public ActionResult Fencers()
        //{
        //    List<FencerModel> tempFencers = new List<FencerModel>();

        //    for (int i = 0; i <4; i++)
        //    {
        //        tempFencers.Add(new FencerModel { ID = i, Name = "" });
        //    }

        //    return View("Fencers", tempFencers);
        //}

        //// POST: /ScoreSheet/CreateSheet
        //[HttpPost]
        //public ActionResult CreateSheet(List<FencerModel> newFencers)
        //{
        //    try
        //    {
        //        foreach(FencerModel fencer in newFencers)
        //        {
        //            // TODO: Add insert logic here
        //            if (!ModelState.IsValid)
        //            {
        //                return View("Fencers", newFencers);
        //            }
        //            fencers.Add(fencer);
        //        }

        //        return RedirectToAction("Index");

        //    }
        //    catch
        //    {
        //        return View("Index");
        //    }
        //}

        // GET: /ScoreSheet/Create
        public ActionResult Create()
        {
            //fencers.Add();
            return View(new FencerModel { Name = "" });
        }

        // POST: /ScoreSheet/Create
        [HttpPost]
        public ActionResult Create(FencerModel newFencer)
        {
            try
            {
                newFencer.ID = fencers.Count();

                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View("Create", newFencer);
                }
                fencers.Add(newFencer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}