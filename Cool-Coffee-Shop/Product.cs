﻿using System;
using System.Collections.Generic;

namespace Cool_Coffee_Shop
{
    public class Product
    {
        public string Name { get; set; }
        public string Cateogory { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Product(string name, string cateogory, string desc, double price)
        {
            Name = name;
            Cateogory = cateogory;
            Description = desc;
            Price = price;
        }
    }
}
