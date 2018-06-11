//using Cyber_Kitchen.Interface;
//using Cyber_Kitchen.Models;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace Cyber_Kitchen.Controllers.Api
//{
//    [Authorize]
//    public class RestaurantController : ApiController
//    {
//        private ApplicationDbContext _context;
//        public RestaurantController()
//        {
//            _context = new ApplicationDbContext();
//        }
//        [HttpDelete]
//        public IHttpActionResult Cancel(int id)
//        {
//            var userId = User.Identity.GetUserId();
//            var restaurant = _context.Restaurants.Single(r => r.RestId == id);
//            restaurant.IsCanceled = true;
//            _context.SaveChanges();

//            return Ok();
//        }
//    }
//}
