using FishWithAuth.EF.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishWithAuth.Models
{
    public class FullInfoViewModel
    {
        public List<Lake> Lakes { get; set; }
        public List<Boat> Boats { get; set; }
        public List<Fish> Fishes { get; set; }
        public List<Bait> Baits { get; set; }
        public List<FishRod> Rods { get; set; }
        public List<Hooks> Hooks { get; set; }

    }
}