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
        static List<BoutModel> bouts = new List<BoutModel>();

        static int[,] boutOrder = { {0, 3}, {1, 2}, {0, 2}, {1, 3}, {2, 3}, { 0, 1} };

        // GET: /ScoreSheet/Index
        public ActionResult Index()
        {
            return View("Index", fencers);
        }

        // GET: /ScoreSheet/Create
        public ActionResult Create()
        {
            return View("Create", new FencerModel { ID= fencers.Count(), Name = "" });
        }

        // POST: /ScoreSheet/Create
        [HttpPost]
        public ActionResult Create(FencerModel newFencer)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Create", newFencer);
                }
                fencers.Add(newFencer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create", newFencer);
            }
        }

        [HttpGet]
        public ActionResult Bout()
        {
            if (bouts.Count() == boutOrder.GetLength(0)) //Check if all bouts have been finished
            {
                return RedirectToAction("ScoreSummary");
            }

            BoutModel bout = new BoutModel();
            bout.FirstFencerId = boutOrder[bouts.Count(), 0];
            bout.SecondFencerId = boutOrder[bouts.Count(), 1];

            ViewBag.First = fencers.First(f => f.ID == bout.FirstFencerId).Name;
            ViewBag.Second = fencers.First(f => f.ID == bout.SecondFencerId).Name;

            return View(bout);
        }

        [HttpPost]
        public ActionResult Bout(BoutModel bout)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.First = fencers.First(f => f.ID == bout.FirstFencerId).Name;
                    ViewBag.Second = fencers.First(f => f.ID == bout.SecondFencerId).Name;

                    return View("Bout", bout);
                }

                bouts.Add(bout);

                if(bouts.Count() == boutOrder.GetLength(0)) //all bouts have been finished
                {
                    return RedirectToAction("ScoreSummary");
                }

                return RedirectToAction("Bout"); //return view with next bout
            }
            catch
            {
                return View("Bout", bout); //return view with original bout
            }
        }

        [HttpGet]
        public ActionResult ScoreSummary()
        {
            if (bouts.Count() != boutOrder.GetLength(0)) //Check if all bouts have been finished
            {
                return RedirectToAction("Bout");
            }

            foreach (BoutModel bout in bouts)
            {
                FencerModel firstFencer = fencers.First(f => f.ID == bout.FirstFencerId);
                FencerModel secondFencer = fencers.First(f => f.ID == bout.SecondFencerId);

                firstFencer.TouchesScored += bout.FirstFencerScore;
                secondFencer.TouchesScored += bout.SecondFencerScore;

                firstFencer.TouchesReceived += bout.SecondFencerScore;
                secondFencer.TouchesReceived += bout.FirstFencerScore;

                if (bout.FirstFencerScore > bout.SecondFencerScore)
                {
                    firstFencer.Victories += 1;
                }
                else if (bout.FirstFencerScore > bout.SecondFencerScore)
                {
                    secondFencer.Victories += 1;
                }
            }

            foreach (FencerModel fencer in fencers)
            {
                fencer.Indicator = fencer.TouchesScored - fencer.TouchesReceived;
            }

            var test = fencers.Max(x => x.Victories);


            //TODO: The seeding places first fencers with highest number of victories 
            //(not absolute number as in the pool, but relative number, which indicates a percentage of victories in the pools against number of bouts fenced in the pool by that fencer), 
            //followed by higher Indicator (in case two or more fencers have same amount of victories), 
            //then by touches scored (if indicator is the same). In case all these parameters are the  same, then fencers are all tied and placed in the same place in the random order 
            //(notes with letter T near the final placement number, like 21T).

            return View("ScoreSummary", fencers.First());
        }
    }
}