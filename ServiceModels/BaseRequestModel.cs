using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ServiceModels
{
    [DataContract]
    public class BaseRequestModel
    {
        [FromQuery]
        public bool TakeAll { get; set; }
        [FromQuery]
        public int Limit { get; set; } = 20;
        [FromQuery]
        [NotMapped]
        public int Page { get; set; } = 1;
        [BindNever]
        public int OffSet
        {
            get
            {
                return Limit * (Page - 1);
            }
            set => OffSet = value;
        }
    }
}
