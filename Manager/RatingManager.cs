using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class RatingManager : IRatingManager
    {

        private ApplicationDbContext _context;

        public RatingManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public Operation<RatingModel> CreateRating(RatingModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExists = _context.Ratings.Where(c => c.RatId == model.RatId).FirstOrDefault();
                if (isExists != null) throw new Exception("rating already exist");

                var entity = model.Create(model);
                _context.Ratings.Add(entity);
                _context.SaveChanges();

                return model;
            });
        }

        public Operation<RatingModel[]> GetRatings()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Ratings.ToList();

                var models = entities.Select(s => new RatingModel(s)
                {
                    Voters = new VoterModel(s.Voter),
                    Restaurant = new RestaurantModel(s.Restaurant)
                }

                ).ToArray();
                return models;
            });
        }

        public Operation<RatingModel> UpdateRating(RatingModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                //var isExist = _context.Scores.Where(c => c.ScoreId == model.ScoreId).AsNoTracking().FirstOrDefault();
                var isExist = _context.Ratings.Find(model.RatId);
                if (isExist == null) throw new Exception("rating does not exist");
                var entity = model.Edit(isExist, model);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();


                return model;
            });
        }
        public Operation<RatingModel> GetRatingById(int ratId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Ratings.Find(ratId);
                if (entity != null) throw new Exception("rating does not exist");
                return new RatingModel(entity);

            });
        }

        public Operation<List<SummaryReportModel>> GetRestaurantSummaryReport()
        {
            return Operation.Create(() =>
            {
                var query = (from r in _context.Restaurants.Include("Scores")
                             group r by r.RestId into g
                             select new SummaryReportModel
                             {
                                 RestId = g.Key,
                                 RestName = g.Select(c => c.RestName).FirstOrDefault(),
                                 EntryDate = g.Select(c => c.CreatedDate).FirstOrDefault(),
                                 RestSum = g.Select(c => c.Scores).Sum(v => v.Sum(s =>
                                 s.Quality + s.Quantity + s.Taste + s.TimeLiness + s.CustomerServices))
                             }).OrderByDescending(c => c.RestSum.Value).ToList();
                //
                // sum (c=> c.sum(x =>x.total))
                // b = 5
                // a = a + b 
                return query;
            });
        }

        //public Operation<SummaryReportModel> GetSummaryReportById(int RestId)
        //{
        //    return Operation.Create(() =>
        //    {
        //        var entity = _context.SummaryReports.Find(RestId);
        //        if (entity == null) throw new Exception("Summary does not exist");
        //        return new SummaryReportModel();

        //    });
        //}

        //public Operation DeleteSummaryReport(int id)
        //{
        //    return Operation.Create(() =>
        //    {
        //        var entity = _context.SummaryReports.Find(id);
        //        if (entity == null) throw new Exception("SummaryReport does not exist");

        //        _context.SummaryReports.Remove(entity);
        //        _context.SaveChanges();
        //    });
        //}
        //public Operation GetDetails(int id)
        //{
        //    return Operation.Create(() =>
        //    {
        //        var entity = _context.Scores.Include(s => s.RestId == id).FirstOrDefault();
        //        if (entity == null) throw new Exception("Scores does not  exist");
        //        return new ScoreModel(entity);
        //    });
        //}
        public Operation DeleteRating(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Ratings.Find(id);
                if (entity == null) throw new Exception("rating does not exist");

                _context.Ratings.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}