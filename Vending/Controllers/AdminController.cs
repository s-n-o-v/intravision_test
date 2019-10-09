using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vending.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index(string key)
        {
            #region Авторизация
            string superKey = "LetMeIn";
            if (string.IsNullOrEmpty(key))
            {
                string tmp_key = null;
                tmp_key = Settings.AppSettings.SecretKey;
                if(string.IsNullOrEmpty(tmp_key) || tmp_key != superKey)
                    return RedirectToAction("Index", "Home");
            }
            if (key == superKey)
                Settings.AppSettings.SecretKey = key;
            #endregion

            var drinks = Core.Drink.getAllList();
            List<Vending.Models.Admin.DrinkViewModel> vmDrinks = new List<Models.Admin.DrinkViewModel>();
            foreach(var dr in drinks)
            {
                Vending.Models.Admin.DrinkViewModel v = new Models.Admin.DrinkViewModel(dr);
                vmDrinks.Add(v);
            }

            var coins = Core.Coin.getList();
            List<Vending.Models.Admin.CoinViewModel> vmCoins = new List<Models.Admin.CoinViewModel>();
            foreach(var coin in coins)
            {
                Vending.Models.Admin.CoinViewModel c = new Models.Admin.CoinViewModel(coin);
                vmCoins.Add(c);
            }

            ViewBag.Cash = Core.Bank.getBank();
            ViewBag.Coins = vmCoins;
            return View(vmDrinks);
        }

        public ActionResult Logout()
        {
            Settings.AppSettings.SecretKey = "";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DrinkCreate()
        {
            Core.Drink drink = new Core.Drink();
            Vending.Models.Admin.DrinkViewModel model = new Models.Admin.DrinkViewModel(drink);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult DrinkCreate(Vending.Models.Admin.DrinkViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var originalFilename = "";
                    var extFile = "";
                    string fileId = "";
                    if (Request.Files["pic_url"] != null && Request.Files["pic_url"].ContentLength != 0)
                    {
                        var fff = Request.Files["pic_url"];
                        originalFilename = Path.GetFileName(fff.FileName);
                        extFile = Path.GetExtension(fff.FileName);
                        fileId = Guid.NewGuid().ToString().Replace("-", "") + extFile;

                        var path = Path.Combine(Server.MapPath("~/Content/drinks/"), fileId);
                        fff.SaveAs(path);
                    }

                    Core.Drink drink = new Core.Drink();
                    model.CopyTo(drink);
                    drink.picture = fileId;
                    drink.Save();
                    // Сохраняем количество
                    Core.Store store = new Core.Store();
                    store.drink_id = drink.id;
                    store.qty = model.qty;
                    store.Save();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Стоит сделать редирект на страницу с ошибкой или записать ее куда-нибудь
                return RedirectToAction("Index");
            }
        }

        public ActionResult DrinkEdit(int id)
        {
            Core.Drink drink = Core.Drink.GetEntityById(id);
            Vending.Models.Admin.DrinkViewModel model = new Models.Admin.DrinkViewModel(drink);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult DrinkEdit(Vending.Models.Admin.DrinkViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Core.Drink drink = Core.Drink.GetEntityById(model.id);
                    model.CopyTo(drink);
                    var originalFilename = "";
                    var extFile = "";
                    string fileId = "";
                    if (Request.Files["pic_url"] != null && Request.Files["pic_url"].ContentLength != 0)
                    {
                        var fff = Request.Files["pic_url"];
                        originalFilename = Path.GetFileName(fff.FileName);
                        extFile = Path.GetExtension(fff.FileName);
                        fileId = Guid.NewGuid().ToString().Replace("-", "") + extFile;

                        var path = Path.Combine(Server.MapPath("~/Content/drinks/"), fileId);
                        drink.picture = fileId;
                        fff.SaveAs(path);
                    }

                    drink.Save();
                    // Сохраняем количество
                    Core.Store store = Core.Store.GetEntityById(drink.id);
                    store.drink_id = drink.id;
                    store.qty = model.qty;
                    store.Save();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                // Стоит сделать редирект на страницу с ошибкой или записать ее куда-нибудь
                return RedirectToAction("Index");
            }
        }
        
        [HttpGet]
        public ActionResult DrinkDelete(int id)
        {
            Core.Drink drink = Core.Drink.GetEntityById(id);
            return PartialView(drink);
        }

        [HttpPost]
        public ActionResult DrinkDelete(int id, FormCollection collection)
        {
            try
            {
                Core.Drink drink = Core.Drink.GetEntityById(id);
                drink.Delete();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // Стоит сделать редирект на страницу с ошибкой или записать ее куда-нибудь
                return RedirectToAction("Index");
            }
        }

        public ActionResult CoinManagment(int id)
        {
            Core.Coin coin = Core.Coin.GetEntityById(id);
            Vending.Models.Admin.CoinViewModel model = new Models.Admin.CoinViewModel(coin);
            return PartialView(model);
        }

        public JsonResult IncreaseCash(int coin_id, int size)
        {
            Core.Coin coin = Core.Coin.GetEntityById(coin_id);
            coin.Increase(size);

            return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BlockCoin(int coin_id, int blk)
        {
            Core.Coin coin = Core.Coin.GetEntityById(coin_id);
            coin.blocked = blk == 1;
            coin.Save();
            return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
