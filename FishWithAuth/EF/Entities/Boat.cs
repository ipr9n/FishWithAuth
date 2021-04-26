using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FishWithAuth.EF.Entities
{
    public class Boat
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Озера в которых можно плавать")]
        public virtual List<Lake> Lakes { get; set; }

        public Boat()
        {
            Lakes = new List<Lake>();
        }
    }
}