using FishWithAuth.EF.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FishWithAuth.Models
{
    public class CreateBoatViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Озера в которых можно плавать")]
        public List<Lake> Lakes { get; set; }

        public CreateBoatViewModel()
        {
            Image = "https://offers-api.agregatoreat.ru/api/file/8a9d80ed-1e36-4386-ba47-90307f47587d";
            Lakes = new List<Lake>();
        }
    }
}