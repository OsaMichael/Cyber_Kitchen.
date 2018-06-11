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
    public class RestaurantController : Controller
    {
        private IRestaurantManager _restMgr;
        public RestaurantController(IRestaurantManager restMgr)
        {
            _restMgr = restMgr;
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _restMgr.GetRestaurants();
            if (results.Succeeded == true)
            {
                return View(results.Unwrap());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();
            }
        
        }
        [HttpGet]
        public ActionResult CreateRestaurant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRestaurant(RestaurantModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _restMgr.CreateRestaurant(model);
            if(result.Succeeded == true)
            {
                TempData["message"] = $"Restaurant{model.RestName} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditRestaurant(int id = 0)
        {
            var result = _restMgr.GetRestaurantById(id);
            if(result.Succeeded)
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
        public ActionResult EditRestaurant(RestaurantModel model)
        {
            if(ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserName();

                model.CreatedBy = User.Identity.GetUserName();
                var result = _restMgr.UpdateRestaurant(model);
                if(result.Succeeded)
                {
                    TempData["message"] = $"Restaurant{model.RestName} was successfully added!";
                    ModelState.AddModelError(string.Empty, "Update was successfully ");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(model);

                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteRestaurant(int id)
        {
            var result = _restMgr.GetRestaurantById(id);
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
        public ActionResult DeleteRestaurant(int id, Restaurant model)
        {
            var result = _restMgr.GetRestaurantById(id);
            if (result == null)
            {
                return View("not found");
            }
            _restMgr.DeleteRestaurant(id);
            return RedirectToAction("Index");

        }

    }
}