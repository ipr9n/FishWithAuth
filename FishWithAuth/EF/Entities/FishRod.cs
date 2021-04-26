﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishWithAuth.EF.Entities
{
    public class FishRod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual List<Fish> Fishes { get; set; }
        public virtual List<Hooks> Hookses { get; set; }

        public FishRod()
        {
            Hookses = new List<Hooks>();
            Fishes = new List<Fish>();
        }
    }
}