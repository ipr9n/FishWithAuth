using FishWithAuth.EF.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishWithAuth.Models
{
    public class CreateBaitViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public List<Fish> Fishes { get; set; }
    }
}