using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;
using Ch24ShoppingCartMVC.Models.DataAccess;

namespace Ch24ShoppingCartMVC.Models{
    public class CartModel
    {
        public static List<ProductViewModel> cart = new List<ProductViewModel>();
        
        //Data Access methods 
        public List<ProductViewModel> Delete(string id)
        {

            cart.Remove(cart.FirstOrDefault(c => c.ProductID == id));
            return cart;
        }
        private List<ProductViewModel> GetCartFromDataStore()
        {
            return cart;
        }
        private ProductViewModel GetSelectedProduct(string id)
        {   //Create an OrderModel object called order
            OrderModel order = new OrderModel();
            //Call the method GetSelectedProduct of the class OrderModel. Return the object returned by the call.
            return order.GetSelectedProduct(id);
        }
        public CartViewModel GetCart(string id = "", string clear = "")
        {
            CartViewModel model = new CartViewModel();
            //Call the method GetCartFromDataStore
            model.Cart = GetCartFromDataStore();
            if (!string.IsNullOrEmpty(id))
                //Called the method GetSelectedProduct with parameter id and assign the return object to the AddedProduct
                model.AddedProduct = GetSelectedProduct(id);
            return model;
        }
        public CartViewModel GetEmptyCart(string clear)
        {
            CartViewModel model = new CartViewModel();
            cart.Clear();
            model.Cart = GetCartFromDataStore();
            return model;
        }
        public void UpdateSoldProductQuantity()
        {
            HalloweenEntities data = new HalloweenEntities();
            
            foreach (var item in cart)
            {
                data.Products.Find(item.ProductID).OnHand -= item.Quantity;
                data.SaveChanges();
            }
            data.SaveChanges();
        }
        private void AddItemToDataStore(CartViewModel model)
        {   
            if(model.Cart != null)
            {
                model.Cart.Add(model.AddedProduct);
            }
            else
            {
                List<ProductViewModel> bb = new List<ProductViewModel>() { model.AddedProduct };
                model.Cart = bb;
            }
            
        }
        public void AddToCart(CartViewModel model)
        {
            if (model.AddedProduct.ProductID != null)
            {
                //Get the product id of the added product
                string id = model.AddedProduct.ProductID;
                //Find the product in the cart that matches the id using lambda expression.
                
                if (model.Cart.Count == 0)
                {
                    //Call the method AddItemToDataStore
                    AddItemToDataStore(model);
                }else if(model.Cart.Count != 0)
                {
                    if(model.Cart.FirstOrDefault(c => c.ProductID == id) == null)
                    {
                        //Call the method AddItemToDataStore
                        AddItemToDataStore(model);
                    }
                    else
                    {
                        //Increase the Quantity by the quantity of the added product
                        model.Cart.FirstOrDefault(c => c.ProductID == id).Quantity++;
                    }
                }

                else
                {
                    //Increase the Quantity by the quantity of the added product
                    model.Cart.FirstOrDefault(c => c.ProductID == id).Quantity++;
                }
                    
            }
        }
                
       
    }    
}