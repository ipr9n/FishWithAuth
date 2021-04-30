using FishWithAuth.EF.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FishWithAuth.Models
{
    public class CreateFishViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        public string Image { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Озера в которых обитает данный вид:")]
        public List<Lake> Lakes { get; set; }
        [DisplayName("Наживка на которую можно поймать:")]
        public List<Bait> AllBaits { get; set; }

        public CreateFishViewModel()
        {
            Image = "https://offers-api.agregatoreat.ru/api/file/8a9d80ed-1e36-4386-ba47-90307f47587d";

            Lakes = new List<Lake>();
            AllBaits = new List<Bait>();
        }
    }
}