using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class RatingModel
    {
        [Key]
        public int RatId { get; set; }
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

        public RatingModel()
        {
            new RestaurantModel();
            new VoterModel();
        }

        public RatingModel(Rating ratings)
        {
            this.Assign(ratings);
            Restaurant = new RestaurantModel();
            Voters = new VoterModel();
        }

        public Rating Create(RatingModel model)
        {
            return new Rating
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
        public Rating Edit(Rating entity, RatingModel model)
        {
            entity.RatId = model.RatId;
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