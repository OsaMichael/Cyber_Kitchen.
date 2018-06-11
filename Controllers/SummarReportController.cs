using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    [Authorize]
    public class SummaryReportController : Controller
    {
        private IScoreManager _scoreMgr;

        public SummaryReportController(IScoreManager scoreMgr)
        {
            _scoreMgr = scoreMgr;
        }
        // GET: SummaryReport
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }

            var results = _scoreMgr.GetRestaurantSummaryReport();

            if (results.Succeeded == true)
            {
                TempData["message"] = "was successfully added!";

                return View(results.Unwrap());
            }

            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var results = _scoreMgr.GetScoreById(id);
            return View(results);
        }
        //public ActionResult Details (int id)
        //{
        //    var result = _scoreMgr
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult DeleteSummaryReport(int id)
        //{
        //    var result = _scoreMgr.GetSummaryReportById(id);
        //    if (result.Succeeded)
        //    {
        //        return View(result.Result);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, result.Message);
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult DeleteSummaryReport(int id, SummaryReport model)
        //{
        //    var result = _scoreMgr.GetSummaryReportById(id);
        //    if (result == null)
        //    {
        //        return View("not found");
        //    }
        //    _scoreMgr.DeleteSummaryReport(id);
        //    return RedirectToAction("Index");

        //    return View();
        //}

    }
}