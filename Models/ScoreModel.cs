using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class ScoreModel
    {
        public int ScoreId { get; set; }
        [DisplayName("Voters Name")]
        [Required]
        public int? VoterId { get; set; }
        [DisplayName("Restaurants Name")]
        [Required]
        public int? RestId { get; set; } 
        [Required]
        public int Taste { get; set; }
        [Required]
        public int Quality { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int TimeLiness { get; set; }
        [Required]
        public int CustomerServices { get; set; }
        [Required]
        public decimal TotalScore { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual RestaurantModel Restaurant { get; set; }
        public virtual VoterModel Voters { get; set; }

        public ScoreModel()
        {
            new RestaurantModel();
            new VoterModel();
        }

        public ScoreModel(Score scores)
        {
            this.Assign(scores);
            Restaurant = new RestaurantModel();
            Voters = new VoterModel();
        }

        public Score Create(ScoreModel model)
        {
            return new Score
            {
                RestId = model.RestId,
                VoterId = model.VoterId,
                Taste = model.Taste,
                Quality = model.Quality,
                Quantity = model.Quantity,
                TimeLiness = model.TimeLiness,
                CustomerServices = model.CustomerServices,
                TotalScore = model.TotalScore,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public Score Edit(Score entity, ScoreModel model)
        {
            entity.ScoreId = model.ScoreId;
            entity.RestId = model.RestId;
            entity.VoterId = model.VoterId;
            entity.Taste = model.Taste;
            entity.Quality = model.Quality;
            entity.Quantity = model.Quantity;
            entity.TimeLiness = model.TimeLiness;
            entity.CustomerServices = model.CustomerServices;
            entity.TotalScore = model.TotalScore;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            return entity;


        }
    }
}