using FishWithAuth.EF.Entities;
using FishWithAuth.Models;

using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishWithAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public string userid
        {
            get { return User.Identity.GetUserId(); }
        }

        public bool IsAdmin
        {
            get { return db.Users.First(x => x.Id == userid).IsAdmin; }
            set
            {
                db.Users.First(x => x.Id == userid).IsAdmin = value;
                db.SaveChanges();
            }
        }

        public ApplicationDbContext db = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult CreateFish()
        {
            var createfish = new CreateFishViewModel() { Lakes = db.Lakes.ToList(), AllBaits = db.Baits.ToList() };

            return View(createfish);
        }

        [HttpPost]
        public ActionResult CreateFish(Fish model, List<String> Lakes, List<String> Baits)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var fish = db.Fishes.First(x => x.Id == model.Id);
                fish.Name = model.Name;
                fish.Description = model.Description;
                db.SaveChanges();
            }
            else if (db.Fishes.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                if(Lakes != null)
                Lakes.ForEach(x => model.Lakes.Add(db.Lakes.First(y => y.Name == x)));
                if(Baits != null)
                Baits.ForEach(x => model.Baits.Add(db.Baits.First(y => y.Name == x)));
                db.Fishes.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateFish(int fishId)
        {
            var fish = db.Fishes.First(x => x.Id == fishId);
            var fishView = new CreateFishViewModel() { Id = fishId, Description = fish.Description, Name = fish.Name };
            return View("CreateFish", fishView);
        }

        [HttpPost]
        public ActionResult CreateLake(Lake model)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var lake = db.Lakes.First(x => x.Id == model.Id);
                lake.Name = model.Name;
                lake.Description = model.Description;
                db.SaveChanges();
            }
            else if (db.Lakes.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                db.Lakes.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateLake(int lakeId)
        {
            var lake = db.Lakes.First(x => x.Id == lakeId);
            var lakeView = new CreateLakeViewModel() { Id = lakeId, Description = lake.Description, Name = lake.Name };
            return View("CreateLake", lakeView);
        }

        [HttpGet]
        public ActionResult CreateLake()
        {
            var createLake = new CreateLakeViewModel();

            return View(createLake);
        }

        [HttpPost]
        public ActionResult CreateRod(FishRod model)
        {
            if (db.FishRods.FirstOrDefault(x => x.Name == model.Name) != null)
            {
                db.FishRods.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult CreateRod()
        {
            var createRod = new CreateRodViewModel();

            return View(createRod);
        }

        [HttpPost]
        public ActionResult UpdateRod(int rodId)
        {
            var rod = db.FishRods.First(x => x.Id == rodId);
            var rodView = new CreateRodViewModel() { Id = rodId, Description = rod.Description, Name = rod.Name };
            return View("CreateRod", rodView);
        }

        [HttpPost]
        public ActionResult CreateBoat(Boat model, List<string> Lakes)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var boat = db.Boats.First(x => x.Id == model.Id);
                boat.Name = model.Name;
                boat.Description = model.Description;
                db.SaveChanges();
            }
            else if (db.Boats.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                Lakes.ForEach(x => model.Lakes.Add(db.Lakes.First(y => y.Name == x)));
                db.Boats.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult CreateBoat()
        {
            var createBoat = new CreateBoatViewModel() { Lakes = db.Lakes.ToList() };

            return View(createBoat);
        }

        [HttpPost]
        public ActionResult UpdateBoat(int boatId)
        {
            var boat = db.Boats.First(x => x.Id == boatId);
            var boatView = new CreateBoatViewModel() { Id = boatId, Description = boat.Description, Name = boat.Name };
            return View("CreateBoat", boatView);
        }

        /*
         * Добавить страницу HTML:
         * Views -> Home -> RMB ( Add View ))
         * Чтобы зайти на эту страницу:
         * Заходим в Controllers ->  HomeController
         * Создаем метод который называется так же как   View!!! Обязательно
         * Код метода:
         * 
                  public ActionResult Name()
             {
                return View();
             }
         *
         *
         * Где Name - название View
         * 
         *  Путь к странице будет "https://localhost:44314/Home/Name"
         *  
         *  Где Name -  название View и метода
         *  
         *  Страница с  навбаром, футером и хедером = Views/Shared/_Layout
         *  
         *  На этой странице можешь увидеть строчки:
            @Scripts.Render("~/bundles/modernizr")
            @Styles.Render("~/Content/css")
        *
        *  Этими строчками подключаются  скрипты и стили для страниц
        *  Эти стили находятся в файле  App_Start/BundleConfig.cs
        *  Для  примера стиль из этого файла, который используется
        *   
          bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        *
        *
        *   Первая строчка - псевдоним ( ~/Content/css ), который мы используем при подключении скрипта ( который используется в Views/Shared/_Layout )
        *   На  второй и третей строчке  -   путь к файлам стилей ( можешь добавить свои по примеру )
        *   
        *   
        *   После запуска проекта (  на F5 ). Если изменяешь файлы с Html ( в папке Views ) не обязательно заново собирать проект, можно просто сохранить файл (  CTRL+S )
        *   и перезагрузить страницу в браузере. Со стилями не проверял, возможно так же работает
         */

        public ActionResult Lakes()
        {
            ViewBag.IsAdmin = IsAdmin;
            return View(db.Lakes.ToList());
        }

        public ActionResult Boats()
        {
            ViewBag.IsAdmin = IsAdmin;
            return View(db.Boats.ToList());
        }

        public ActionResult Rods()
        {
            ViewBag.IsAdmin = IsAdmin;
            return View(db.FishRods.ToList());
        }

        public ActionResult Fishes()
        {
            ViewBag.IsAdmin = IsAdmin;
            return View(db.Fishes.ToList());
        }

        public ActionResult AdminPanel()
        {
            if (IsAdmin)
            {
                var model = new FullInfoViewModel() { Lakes = db.Lakes.ToList(), Boats = db.Boats.ToList(), Fishes = db.Fishes.ToList() };
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult GetAdmin()
        {
            IsAdmin = true;
            return RedirectToAction("AdminPanel");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}