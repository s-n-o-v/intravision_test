using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;

namespace Vending.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Core.Coin> ac = Core.Coin.availableList();
            List<Vending.Models.Coin> coins = new List<Models.Coin>();
            foreach (var c in ac)
            {
                Vending.Models.Coin c0 = new Models.Coin(c);
                coins.Add(c0);
            }
            Vending.Models.ViewModel vm = new Models.ViewModel(Settings.AppSettings.Cash, coins);
            //ViewBag.Cash = Settings.AppSettings.Cash;
            //ViewBag.Coins = coins;
            return View(vm);
        }

        [HttpPost]
        public JsonResult Purchase(int? drink_id)
        {
            if (drink_id == null || drink_id < 1)
            {
                return Json(new { success = false, message = "Неправильный код напитка", cash = Settings.AppSettings.Cash }, JsonRequestBehavior.AllowGet);
            }

            Core.Drink drink = Core.Drink.GetEntityById(drink_id.Value);
            if (drink.price > Settings.AppSettings.Cash)
            {
                return Json(new { success = false, message = "Недостаточно средств", cash = Settings.AppSettings.Cash }, JsonRequestBehavior.AllowGet);
            }
            // Спишем средства со счета
            Settings.AppSettings.Cash -= drink.price;
            // Запишем продажу в БД
            Core.Sold sold = new Sold();
            sold.drink_id = drink_id.Value;
            sold.dt = DateTime.Now;
            sold.Save();
            // Уменьшим количество товара в хранилище
            Core.Store store = Core.Store.GetEntityById(drink_id.Value);
            store.qty--;
            store.Save();

            return Json(new { success = true, message = "success", cash = Settings.AppSettings.Cash }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Список доступных напитков в зависимости от остатка денежных средств
        /// </summary>
        /// <returns>json-список напитков</returns>
        public JsonResult Available()
        {
            int cash = Settings.AppSettings.Cash;
            IEnumerable<Drink> drinks = Drink.getListStored();
            List<Vending.Models.DrinkList> items = new List<Vending.Models.DrinkList>();
            foreach(var d in drinks)
            {
                Vending.Models.DrinkList drink_model = new Models.DrinkList(d);
                drink_model.avail = d.price <= cash;
                items.Add(drink_model);
            }
            return Json(items, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}