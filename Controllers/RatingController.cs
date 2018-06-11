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
    public class RatingController : Controller
    {
        private IRatingManager _ratMgr;
        private IVoterManager _votMgr;
        private IRestaurantManager _restMgr;

        public RatingController(IRatingManager ratMgr, IVoterManager votMgr, IRestaurantManager restMgr)
        {
            _ratMgr = ratMgr;
            _votMgr = votMgr;
            _restMgr = restMgr;
        }
        

        //GET: Rating
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _ratMgr.GetRatings();

            if (results.Succeeded == true)
            {
                List<Rating> mylist = new List<Rating>();

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

        public ViewResult New()
        {
            var restuarants = _restMgr.GetRestaurants();

            var result = new Restaurant 
            {
               RestName = restuara
            };


        }




        //public ActionResult Details(int id)
        //{
        //    var results = _scoreMgr.GetScoreById(id);
        //    return View(results);
        //}

        [HttpGet]
        public ActionResult CreateRating()
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");

            //          ViewBag.voters = _votMgr.GetVoters()
            //.Select(c => new SelectListItem { Value = c.VoterId, Text = c.VotName })
            //.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateRating(RatingModel[] model)
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");

            model.CreatedBy = User.Identity.GetUserId();
            var result = _ratMgr.CreateRating(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"Rating{model.RatId} was successfully added!";

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditRating(int id = 0)
        {
            var result = _ratMgr.GetRatingById(id);
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
        public ActionResult EditRating(RatingModel model)
        {

            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserId();
                var result = _ratMgr.UpdateRating(model);
                if (result.Succeeded == true)
                {
                    TempData["message"] = $"Rating{model.RatId} was successfully added!";
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
        public ActionResult DeleteRating(int id)
        {
            var result = _ratMgr.GetRatingById(id);
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
        public ActionResult DeleteRating(int id, Score model)
        {
            var result = _ratMgr.GetRatingById(id);
            if (result == null)
            {
                return View("not found");
            }
            _ratMgr.DeleteRating(id);
            return RedirectToAction("Index");
        }
    }
}