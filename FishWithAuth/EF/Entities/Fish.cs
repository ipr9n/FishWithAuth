using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FishWithAuth.EF.Entities
{
    public class Fish
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual List<Bait> Baits { get; set; }
        public virtual List<Lake> Lakes { get; set; }
        public virtual List<FishRod> FishRods { get; set; }
        public virtual List<Hooks> Hookses { get; set; }

        public Fish()
        {
            FishRods = new List<FishRod>();
            Baits = new List<Bait>();
            Lakes = new List<Lake>();
            Hookses = new List<Hooks>();
        }
    }
}