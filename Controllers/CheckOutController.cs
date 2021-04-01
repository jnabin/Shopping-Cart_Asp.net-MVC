using Ch24ShoppingCartMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ch24ShoppingCartMVC.Controllers
{
    public class CheckOutController : Controller
    {
        private CheckOutModel CheckOut = new CheckOutModel();
        
        [HttpGet]
        public ViewResult List()
        {
            CheckOutViewModel model = new CheckOutViewModel();
            model = (CheckOutViewModel)TempData["checkout"];
            //Passing model to View
            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult List(List<ProductViewModel> cart)
        {
            TempData["checkout"] = CheckOut.GetCheckList(cart);
            return RedirectToAction("List", "CheckOut");
        }

    }
}
