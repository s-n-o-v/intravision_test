using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vending.Controllers
{
    public class CashController : Controller
    {
        // GET: Cash
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Increase(int size)
        {
            Settings.AppSettings.Cash += size;
            // Сохраним значение в банк
            Core.Coin coin = Core.Coin.GetEntityByValue(size);
            Core.Bank bank = new Core.Bank();
            bank.coin_id = coin.id;
            bank.qty = 1;
            bank.Save();

            return Json(new { success = true, cash = Settings.AppSettings.Cash }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Cashout()
        {
            bool result = false;
            int cash = Settings.AppSettings.Cash;
            string info = "Недостаточно средств";

            if (cash > 0)
            {
                Settings.AppSettings.Cash = 0;
                info = Core.Bank.Cashout(cash);
                //info = "Выдано монет: 1руб - 2шт, 5руб - 3шт.";
                result = true;
            }
            return Json(new { success = result, cash = 0, message = info}, JsonRequestBehavior.AllowGet);
        }

        // GET: Cash/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cash/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
