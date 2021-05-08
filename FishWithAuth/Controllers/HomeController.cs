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
    public class HomeController : Controller
    {
        public string userid
        {
            get { return User.Identity.GetUserId(); }
        }

        public bool IsAdmin
        {
            get { return User.Identity.IsAuthenticated ? db.Users.First(x => x.Id == userid).IsAdmin : false;  }
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

        [HttpGet]
        [Authorize]
        public ActionResult CreateFish()
        {
            var createfish = new CreateFishViewModel() { Lakes = db.Lakes.ToList(), AllBaits = db.Baits.ToList() };

            return PartialView(createfish);
        }

        [HttpGet]
        public ActionResult Fish(int Id)
        {
            return View(db.Fishes.First(x => x.Id == Id));
        }

        [HttpGet]
        public ActionResult Lake(int Id)
        {
            return View(db.Lakes.First(x => x.Id == Id));
        }

        [HttpGet]
        public ActionResult Boat(int Id)
        {
            return View(db.Boats.First(x => x.Id == Id));
        }

        [HttpGet]
        public ActionResult Bait(int Id)
        {
            return View(db.Baits.First(x => x.Id == Id));
        }

        [HttpGet]
        public ActionResult Rod(int Id)
        {
            return View(db.FishRods.First(x => x.Id == Id));
        }

        [HttpGet]
        public ActionResult Hook(int Id)
        {
            return View(db.Hookses.First(x => x.Id == Id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateFish(Fish model, List<String> Lakes, List<String> Baits, List<string> Rods, List<string> Hookses)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var fish = db.Fishes.First(x => x.Id == model.Id);
                fish.Name = model.Name;
                fish.Description = model.Description;
                fish.Image = model.Image;

                if (Rods != null)
                {
                    fish.FishRods.RemoveAll(x => x.Id != -1);
                    Rods?.ForEach(x => fish.FishRods.Add(db.FishRods.First(y => y.Name == x)));
                }
                if (Lakes != null)
                {
                    fish.Lakes.RemoveAll(x => x.Id != -1);
                    Lakes?.ForEach(x => fish.Lakes.Add(db.Lakes.First(y => y.Name == x)));
                }
                if (Baits != null)
                {
                    fish.Baits.RemoveAll(x => x.Id != -1);
                    Baits?.ForEach(x => fish.Baits.Add(db.Baits.First(y => y.Name == x)));
                }
                if (Hookses != null)
                {
                    fish.Hookses.RemoveAll(x => x.Id != -1);
                    Hookses?.ForEach(x => fish.Hookses.Add(db.Hookses.First(y => y.Name == x)));
                }

                db.SaveChanges();
            }
            else if (db.Fishes.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                    Lakes?.ForEach(x => model.Lakes.Add(db.Lakes.First(y => y.Name == x)));
                    Baits?.ForEach(x => model.Baits.Add(db.Baits.First(y => y.Name == x)));
                    Rods?.ForEach(x => model.FishRods.Add(db.FishRods.First(y => y.Name == x)));
                    Hookses?.ForEach(x => model.Hookses.Add(db.Hookses.First(y => y.Name == x)));
                db.Fishes.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [Authorize]
        public ActionResult UpdateFish(int Id)
        {
            if (IsAdmin)
            {
                var fish = db.Fishes.First(x => x.Id == Id);
                var fishView = new CreateFishViewModel() { Id = Id, Description = fish.Description, Rods = db.FishRods.ToList(), Hookses = db.Hookses.ToList(), AllBaits = db.Baits.ToList(), Lakes = db.Lakes.ToList(), Name = fish.Name, Image = fish.Image };
                return View("CreateFish", fishView);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeleteFish(int fishId)
        {
            db.Fishes.Remove(db.Fishes.First(x => x.Id == fishId));
            db.SaveChanges();
            return RedirectToAction("Fishes");
        }


        [HttpPost]
        [Authorize]
        public ActionResult CreateLake(Lake model)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var lake = db.Lakes.First(x => x.Id == model.Id);
                lake.Name = model.Name;
                lake.Description = model.Description;
                lake.Image = model.Image;
                db.SaveChanges();
            }
            else if (db.Lakes.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                db.Lakes.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [Authorize]
        public ActionResult UpdateLake(int Id)
        {
            var lake = db.Lakes.First(x => x.Id == Id);
            var lakeView = new CreateLakeViewModel() { Id = Id, Description = lake.Description, Name = lake.Name, Image = lake.Image };
            return View("CreateLake", lakeView);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateLake()
        {
            var createLake = new CreateLakeViewModel();

            return PartialView(createLake);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateRod(FishRod model, List<string> Fishes)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var rod = db.FishRods.First(x => x.Id == model.Id);
                rod.Name = model.Name;
                rod.Description = model.Description;
                rod.Image = model.Image;
                db.SaveChanges();
            }
            else if (db.FishRods.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                Fishes?.ForEach(x => model.Fishes.Add(db.Fishes.First(y => y.Name == x)));
                db.FishRods.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateRod()
        {
            var createRod = new CreateRodViewModel() { Fishes = db.Fishes.ToList() };

            return PartialView(createRod);
        }

        [Authorize]
        public ActionResult UpdateRod(int Id)
        {
            var rod = db.FishRods.First(x => x.Id == Id);
            var rodView = new CreateRodViewModel() { Id = Id, Description = rod.Description, Name = rod.Name, Image = rod.Image };
            return View("CreateRod", rodView);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateBoat(Boat model, List<string> Lakes)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var boat = db.Boats.First(x => x.Id == model.Id);
                boat.Name = model.Name;
                boat.Description = model.Description;
                boat.Image = model.Image;
                db.SaveChanges();
            }
            else if (db.Boats.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                Lakes?.ForEach(x => model.Lakes.Add(db.Lakes.First(y => y.Name == x)));
                db.Boats.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateBoat()
        {
            var createBoat = new CreateBoatViewModel() { Lakes = db.Lakes.ToList() };

            return PartialView(createBoat);
        }

        [Authorize]
        public ActionResult UpdateBoat(int Id)
        {
            var boat = db.Boats.First(x => x.Id == Id);
            var boatView = new CreateBoatViewModel() { Id = Id, Description = boat.Description, Name = boat.Name, Image = boat.Image };
            return View("CreateBoat", boatView);
        }

        ///
        [HttpPost]
        [Authorize]
        public ActionResult CreateHook(Hooks model, List<string> Fishes)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var hook = db.Hookses.First(x => x.Id == model.Id);
                hook.Name = model.Name;
                hook.Description = model.Description;
                hook.Image = model.Image;
                db.SaveChanges();
            }
            else if (db.Boats.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                Fishes?.ForEach(x => model.Fishes.Add(db.Fishes.First(y => y.Name == x)));
                db.Hookses.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateHook()
        {
            var createHook = new CreateHookViewModel() { Fishes = db.Fishes.ToList() };

            return PartialView(createHook);
        }

        [Authorize]
        public ActionResult UpdateHook(int Id)
        {
            var hook = db.Hookses.First(x => x.Id == Id);
            var hookView = new CreateHookViewModel() { Id = Id, Description = hook.Description, Name = hook.Name, Image = hook.Image };
            return View("CreateHook", hookView);
        }
        //
        [HttpPost]
        [Authorize]
        public ActionResult CreateBait(Bait model, List<string> Fishes)
        {
            if (model.Id != 0 && IsAdmin)
            {
                var bait = db.Baits.First(x => x.Id == model.Id);
                bait.Name = model.Name;
                bait.Description = model.Description;
                bait.Image = model.Image;
                db.SaveChanges();
            }
            else if (db.Baits.FirstOrDefault(x => x.Name == model.Name) == null)
            {
                Fishes?.ForEach(x => model.Fishes.Add(db.Fishes.First(y => y.Name == x)));
                db.Baits.Add(model);
                db.SaveChanges();
            }
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateBait()
        {
            var createBait = new CreateBaitViewModel() { Fishes = db.Fishes.ToList() };

            return PartialView(createBait);
        }

        [Authorize]
        public ActionResult UpdateBait(int Id)
        {
            var hook = db.Baits.First(x => x.Id == Id);
            var baitView = new CreateBaitViewModel() { Id = Id, Description = hook.Description, Name = hook.Name, Image = hook.Image };
            return View("CreateBait", baitView);
        }

        public ActionResult Lakes(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.Lakes.Where(x => x.Name.Contains(text)).ToList());
            else
                return View(db.Lakes.ToList());
            
        }

        public ActionResult Boats(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.Boats.Where(x => x.Name.Contains(text)).ToList());
            else
                return View(db.Boats.ToList());
        }

        public ActionResult Rods(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.FishRods.Where(x => x.Name.Contains(text)).ToList());
            else
                return View(db.FishRods.ToList());
        }

        public ActionResult Hookses(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.Hookses.Where(x => x.Name.Contains(text)).ToList());
            else
                return View(db.Hookses.ToList());
        }

        public ActionResult Baits(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.Baits.Where(x => x.Name.Contains(text)).ToList());
            else
                return View(db.Baits.ToList());
        }

        public ActionResult Fishes(string text)
        {
            ViewBag.IsAdmin = IsAdmin;

            if (!String.IsNullOrEmpty(text))
                return View(db.Fishes.Where(x => x.Name.Contains(text)).ToList());
            else
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
    }
}