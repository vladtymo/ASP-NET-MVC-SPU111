﻿namespace AspNet_MVC_SPU111.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ---------- navigation properties
        public ICollection<Product> Products { get; set; }
    }
}