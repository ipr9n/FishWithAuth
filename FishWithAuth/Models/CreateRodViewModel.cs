using FishWithAuth.EF.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FishWithAuth.Models
{
    public class CreateRodViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Рыба которую можно поймать")]
        public List<Fish> Fishes { get; set; }

        public CreateRodViewModel()
        {
            Image = "https://offers-api.agregatoreat.ru/api/file/8a9d80ed-1e36-4386-ba47-90307f47587d";
            Fishes = new List<Fish>();
        }
    }
}