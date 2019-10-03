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
    public class ItemManager
    {
        ItemRepository _itemRepository = new ItemRepository();
        public bool ExistItem(Item item)
        {
            return _itemRepository.ExistItem(item);
        }
        public string InsertItem(Item item)
        {
            string message;
            message = _itemRepository.InsertItem(item) > 0 ? "Item is Saved" : "Item is not saved";
            return message;
        }
        public DataTable ShowItem()
        {
            return _itemRepository.ShowItem();
        }
        public int DeleteItem(int id)
        {
            return _itemRepository.DeleteItem(id);
        }
        public int UpdateItem(Item item)
        {
            return _itemRepository.UpdateItem(item);

        }
        public DataTable SearchItem(Item item)
        {
            return _itemRepository.SearchItem( item);
        }
    }
}
