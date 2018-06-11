using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class RestaurantModel
    {

        [Key]

        public int RestId { get; set; }
        [Required]
        public string RestName { get; set; }
        public bool IsCanceled { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<VoterModel> Voters { get; set; }
        public virtual ICollection<ScoreModel> Scores { get; set; }

        public RestaurantModel()
        {
            new HashSet<VoterModel>();
            new HashSet<ScoreModel>();
        }
        public RestaurantModel(Restaurant restaurant)
        {
            this.Assign(restaurant);
            Voters = new HashSet<VoterModel>();
            Scores = new HashSet<ScoreModel>();
        }

        public Restaurant Create(RestaurantModel model)
        {
            return new Restaurant
            {
                RestName = model.RestName,
                IsCanceled = model.IsCanceled,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
        public Restaurant Edit(Restaurant entity, RestaurantModel model)
            {
            entity.RestId = model.RestId;
            entity.RestName = model.RestName;
           entity.IsCanceled = model.IsCanceled;
            entity.ModifiedBy = model.ModifiedBy;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedDate = DateTime.Now;
            //entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}