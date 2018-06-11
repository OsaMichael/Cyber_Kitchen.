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
    public class VoterController : Controller
    {
        private IVoterManager _votMgr;
        public VoterController(IVoterManager votMgr)
        {
            _votMgr = votMgr;
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _votMgr.GetVoters();
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
        public ActionResult CreateVoter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVoter(VoterModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _votMgr.CreateVoter(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"Voter{model.VotName} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditVoter(int id = 0)
        {
            var result = _votMgr.GetVoterById(id);
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
        public ActionResult EditVoter(VoterModel model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserName();
                var result = _votMgr.UpdateVoter(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Voter{model.VotName} was successfully added!";
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
        public ActionResult DeleteVoter(int id)
        {
            var result = _votMgr.GetVoterById(id);
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
        public ActionResult DeleteVoter(int id, Voter model)
        {
            var result = _votMgr.GetVoterById(id);
            if (result == null)
            {
                return View("not found");
            }
            
            _votMgr.DeleteVoter(id);
            return RedirectToAction("Index");

            return View();
        }

    }
}