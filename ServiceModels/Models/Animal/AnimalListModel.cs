using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.Models.Animal
{
    public class AnimalResponseModel
    {
        public IEnumerable<AnimalItemModel>  Animals { get; set; }
        public int TotalCount { get; set; }
    }
    public class AnimalItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AnimalRequestModel
    {
        public string Name { get; set; }
    }
}
