using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class RestaurantManager : IRestaurantManager
    {
        private ApplicationDbContext _context;

        public RestaurantManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Operation<RestaurantModel> CreateRestaurant(RestaurantModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExists = _context.Restaurants.Where(c => c.RestName == model.RestName).FirstOrDefault();
                if (isExists != null) throw new Exception("restaurant already exist");

                var entity = model.Create(model);
                _context.Restaurants.Add(entity);
                _context.SaveChanges();
               
                return model;
            });
        }

        public Operation<RestaurantModel[]> GetRestaurants()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Restaurants.ToList();

                var models = entities.Select(c => new RestaurantModel(c)).ToArray();
                return models;
            });
        }
        public Operation<RestaurantModel> UpdateRestaurant(RestaurantModel model)
        {

            return Operation.Create(() =>
            {
                //model.Validate();
                var isExist = _context.Restaurants.Find(model.RestId);
                if (isExist == null) throw new Exception("Restaurant does not exist");

                var entity = model.Edit(isExist, model);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                

                return model;
            });
        }
        public Operation<RestaurantModel> GetRestaurantById(int restaurantId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Restaurants.Where(c => c.RestId == restaurantId).FirstOrDefault();
                if (entity == null) throw new Exception("Restaurant does not exist");
                return new RestaurantModel(entity);

            });
        }

        public Operation Details(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Restaurants.Include(s => s.RestId == id).FirstOrDefault();
                if (entity == null) throw new Exception("Scores does not  exist");
                return new RestaurantModel(entity);


            });
        }
        public Operation DeleteRestaurant(int id)
        {
            return Operation.Create(() =>
            {
                //var entity = _context.Restaurants.Include(c => c.RestId == id).FirstOrDefault();
                var entity = _context.Restaurants.Find(id);
                if (entity == null) throw new Exception("Restaurant does  exist");

                _context.Restaurants.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}