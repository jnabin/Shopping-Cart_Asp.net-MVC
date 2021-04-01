using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Controllers {
    public class CartController : Controller
    {
        private CartModel cart = new CartModel();

        [HttpGet]
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("List/");
        }
        [HttpGet]
        public ViewResult List()
        {
            CartViewModel model = (CartViewModel)TempData["cart"];
            //if the model is null, then call the method GetCart
           if(model == null)
            {
                if (Session["paymentsucces"]!=null && Session["paymentsucces"].ToString() == "true") {
                    model = cart.GetEmptyCart(Session["paymentsucces"].ToString());
                    Session["paymentsucces"] = null;
                }
                else
                {
                    model = cart.GetCart();
                }
                
            }
            //Passing model to View
            return View(model);
        }
        [HttpPost]
        public ActionResult List(OrderViewModel order, int onhand)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "**invalid quantity!!";
                return RedirectToAction("Index", "Order", new { id = order.SelectedProduct.ProductID });
            }
            if(order.SelectedProduct.Quantity > onhand)
            {
                TempData["error"] = "*quantity not available!";
                return RedirectToAction("Index", "Order", new { id = order.SelectedProduct.ProductID });
            }
            CartViewModel carta = new CartViewModel();
            if (Session["paymentsucces"] != null && Session["paymentsucces"].ToString() == "true")
            {
                carta = cart.GetEmptyCart(Session["paymentsucces"].ToString());
                Session["paymentsucces"] = null;
            }
            CartViewModel model = cart.GetCart(order.SelectedProduct.ProductID);
            //Assign the quantity of the selected product to the quantity of the added product
            model.AddedProduct.Quantity = order.SelectedProduct.Quantity;
            //Call the method AddtoCart
            cart.AddToCart(model);
            //Assign model to the TempData
            TempData["cart"] = model;
            return RedirectToAction("List", "Cart");
        }
        [HttpGet]
        public RedirectToRouteResult Delete(String id)
        {
            CartViewModel model = new CartViewModel();
            model.Cart = cart.Delete(id);
            TempData["cart"] = model;
            return RedirectToAction("List", "Cart");
        }
         [HttpGet]
         public RedirectToRouteResult UpdateQuantity()
        {
            cart.UpdateSoldProductQuantity();
            return RedirectToAction("Index", "Home");
        }

    }
}
