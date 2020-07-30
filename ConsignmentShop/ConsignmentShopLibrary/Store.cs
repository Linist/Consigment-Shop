﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsignmentShopLibrary
{
    public class Store
    {
        public string Name { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<Item> Items { get; set; }


        /*Here we instaniated both list in the constructor before they are being used*/
        public Store()
        {
            Vendors = new List<Vendor>();
            Items = new List<Item>();
        }
    }
}
