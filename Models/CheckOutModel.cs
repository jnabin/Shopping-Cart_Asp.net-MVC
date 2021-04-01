using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Models
{
    public class CheckOutModel
    {
        
        public CheckOutViewModel GetCheckList(List<ProductViewModel> ProductList)
        {
            CheckOutViewModel model = new CheckOutViewModel();
            model.checkoutlist = ProductList;
           
            return model;
        }
        
    }
}