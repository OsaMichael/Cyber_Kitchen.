using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IRatingManager
    {
        Operation<RatingModel> CreateRating(RatingModel model);
        Operation<RatingModel[]> GetRatings();
        Operation<RatingModel> UpdateRating(RatingModel model);
        Operation<RatingModel> GetRatingById(int ratId);
        Operation<List<SummaryReportModel>> GetRestaurantSummaryReport();
        Operation DeleteRating(int id);
    }
}