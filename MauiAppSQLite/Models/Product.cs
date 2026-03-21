using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppSQLite.Models
{
    public class Product
    {
        string _name;
        decimal _price;
        int _quantity;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get => _name; set { if (value == null) throw new Exception("Por favor preencha o nome!"); _name = value; }  }
        public decimal Price { get => _price; set { if (value <= 0) throw new Exception("Por favor preencha o preço!"); _price = value; } }
        public int Quantity { get => _quantity; set { if (value <= 0 ) throw new Exception("Por favor preencha a quantidade!"); _quantity = value; } }
        public string Category { get; set; }
        public decimal TotalPrice { get => Price * Quantity; }
    }
}