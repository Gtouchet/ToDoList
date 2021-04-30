using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class ToDoList
    {
        public List<Item> items = new List<Item>();

        public ToDoList() { }

        public void AddItem(List<Item> items, Item item)
        {
            items.Add(item);
            if (items.Count == 8)
            {
                this.SendMail();
            }
        }

        public bool isItemsValid(List<Item> items)
        {
            if (items.Count > 10)
            {
                return false;
            }

            foreach (Item item in items)
            {
                if (!string.IsNullOrEmpty(item.name)
                    || item.content.Length > 1000
                    || item.creationDate == null)
                {
                    return false;
                }
            }

            return true;
        }

        public bool isCreationPossible(Item lastCreatedItem)
        {
            return new DateTime(DateTime.Now.Subtract(lastCreatedItem.creationDate).Ticks).Minute - 1 > 30;
        }

        public bool SendMail()
        {
            Console.WriteLine("you've got mail");
            return true;
        }
    }

    public class Item
    {
        public string name { get; set; }
        public string content { get; set; }
        public DateTime creationDate { get; set; }
    }
}
