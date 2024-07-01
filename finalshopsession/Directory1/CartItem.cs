using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalshopsession
{
    internal class CartItem 
    {
        private int _id;
        private int _pId; 
        private string _name; 
        private double _price; 
        private int _quantity; 
        
        public CartItem(int id, int pId, string name, double price, int quantity)
        {
            _id = id;
            _pId = pId;
            _name = name;
            _price = price;
            _quantity = quantity;
        }

        public int cId
        {
            get { return _id; }
            set { _id = value; }
        }

        public int prId
        {
            get { return _pId; }
            set { _pId = value; }
        }

        public string cName
        {
            get { return _name; }
            set { _name = value; }
        }

        public double cPrice
        {
            get { return _price; }
            set { _price = Math.Round(value,2); }
        }

        public int cQuantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
}
