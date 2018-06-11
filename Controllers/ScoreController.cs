using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    [Authorize]
    public class ScoreController : Controller
    {
        private IScoreManager _scoreMgr;
        private IVoterManager _votMgr;
        private IRestaurantManager _restMgr;

        public ScoreController(IScoreManager scoreMgr, IVoterManager votMgr, IRestaurantManager restMgr)
        {
            _scoreMgr = scoreMgr;
            _votMgr = votMgr;
            _restMgr = restMgr;
        }
        //public ActionResult New()
        //{
        //    var caterer = _scoreMgr.GetScores.ToList();
        //    //var viewModel = new CustomerFormViewModel
        //    //{
        //    //    Customer = new Customer(),
        //    //    MembershipTypes = membershipTypes
        //    //};
        //    return View("CustomerForm", viewModel);
        //}

        //GET: Score
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _scoreMgr.GetScores();
        
            if (results.Succeeded == true)
            {
                List<Score> mylist = new List<Score>();

                foreach (var item in results.Unwrap())
                {
                    item.TotalScore =
                        item.Quality + item.Quantity + item.Taste + item.CustomerServices + item.TimeLiness;
                }
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

        [HttpGet]
        public ActionResult CreateScore()
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");

  //          ViewBag.voters = _votMgr.GetVoters()
  //.Select(c => new SelectListItem { Value = c.VoterId, Text = c.VotName })
  //.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateScore(ScoreModel model)
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");

            model.CreatedBy = User.Identity.GetUserId();
            var result = _scoreMgr.CreateScore(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"Score{model.ScoreId} was successfully added!";

                if(User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditScore(int id = 0)
        {
            var result = _scoreMgr.GetScoreById(id);
            if (result.Succeeded == true)
            {
                return View(result.Result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditScore(ScoreModel model)
        {

            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserId();
                var result = _scoreMgr.UpdateScore(model);
                if (result.Succeeded == true)
                {
                    TempData["message"] = $"Score{model.ScoreId} was successfully added!";
                    ModelState.AddModelError(string.Empty, "Update was successfully ");
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.Message);
                ViewBag.Error = $"Error occured : {result.Message}";
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteScore(int id)
        {
            var result = _scoreMgr.GetScoreById(id);
            if (result.Succeeded)
            {
                return View(result.Result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteScore(int id, Score model)
        {
            var result = _scoreMgr.GetScoreById(id);
            if (result == null)
            {
                return View("not found");
            }
            _scoreMgr.DeleteScore(id);
            return RedirectToAction("Index");

           
        }

    }
}