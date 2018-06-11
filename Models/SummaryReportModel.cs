using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class SummaryReportModel
    {

        [Key]
        public int RestId { get; set; }
        [Required]
        public string RestName { get; set; }
        public DateTime EntryDate { get; set; }
        [Required]
        public int? RestSum { get; set; }



        //public SummaryReportModel(SummaryReport summaryReport)
        //{
        //    this.Assign(summaryReport);
        //}

        //public SummaryReport Create(SummaryReportModel model)
        //{
        //    return new SummaryReport
        //    {
        //        //RestId = model.RestId,
        //        RestName = model.RestName,
        //        EntryDate = model.EntryDate,
        //        RestSum = (int)model.RestSum

        //    };
        //}
        //public SummaryReport Edit(SummaryReport entity, SummaryReportModel model)
        //{
        //    entity.RestId = model.RestId;
        //    entity.RestName = model.RestName;
        //    entity.RestSum = (int)model.RestSum;
        //    return entity;


        //}
        }
}