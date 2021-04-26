using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishWithAuth.EF.Entities
{
    public class Hooks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual List<Fish> Fishes { get; set; }
        public virtual List<FishRod> FishRods { get; set; }

        public Hooks()
        {
            Fishes = new List<Fish>();
            FishRods = new List<FishRod>();
        }
    }
}