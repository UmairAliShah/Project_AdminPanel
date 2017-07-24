using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cuffs_And_Cufflinks.Models;
namespace Cuffs_And_Cufflinks.Controllers
{
    public class Item
    {

        private Table_products products = new Table_products();

        public Table_products Products
        {
            get { return products; }
            set { products = value; }
        }


        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private string size;

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public Item()
        { }


        public Item(Table_products p, int q, string s, string c)
        {
            products = p;
            quantity = q;
            size = s;
            color = c;
        }

    }
}