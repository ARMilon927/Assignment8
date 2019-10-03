using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopApp.Model;
using CoffeeShopApp.Repository;

namespace CoffeeShopApp.Manager
{
    public class OrderManager
    {
        OrderRepository _orderRepository = new OrderRepository();
        public DataTable GetCustomer()
        {
            return _orderRepository.GetCustomer();
        }

        public DataTable GetItem()
        {
            return _orderRepository.GetItem();
        }

        public int GetItemPrice(int id)
        {
            return _orderRepository.GetItemPrice(id);
        }

        public bool InsertOrder(Order order)
        {
            return _orderRepository.InsertOrder(order);
        }

        public DataTable ShowOrder()
        {
            return _orderRepository.ShowOrder();
        }

        public bool DeleteItem(int id)
        {
            return _orderRepository.DeleteItem(id);
        }

        public bool UpdateOrder(Order order)
        {
            return _orderRepository.UpdateOrder(order);
        }

        public DataTable SearchOrder(string name, string item, int quantity)
        {
            return _orderRepository.SearchOrder(name, item, quantity);
        }
    }
}
