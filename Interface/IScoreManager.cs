using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IScoreManager
    {
        Operation<ScoreModel> CreateScore(ScoreModel model);
        Operation<ScoreModel[]> GetScores();
        Operation<ScoreModel> UpdateScore(ScoreModel model);
        Operation<ScoreModel> GetScoreById(int scoreId);
        Operation<List<SummaryReportModel>> GetRestaurantSummaryReport();
        //Operation DeleteSummaryReport(int id);
        //Operation<SummaryReportModel> GetSummaryReportById(int RestId);
        Operation GetDetails(int id);
        Operation DeleteScore(int id);
    }
}