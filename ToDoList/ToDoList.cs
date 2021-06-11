using System;
using System.Collections.Generic;

namespace ToDoList
{
    public interface IToDoList
    {
        public List<IItem> items { get; set; }
        public bool mailSent { get; set; }

        public bool AddItem(IItem item);
        public bool IsItemCreationPossible();
        public bool IsValid();
        public void SendMail();
    }

    public class ToDoList : IToDoList
    {
        public List<IItem> items { get; set; } = new List<IItem>();
        public bool mailSent { get; set; } = false;

        public bool AddItem(IItem item)
        {
            if (this.IsItemCreationPossible() && item.IsValid())
            {
                this.items.Add(item);
                
                if (this.items.Count == 8)
                {
                    this.SendMail();
                }

                return true;
            }

            return false;
        }

        public bool IsItemCreationPossible()
        {
            return this.items.Count == 0
                || (this.items.Count < 10
                && (DateTime.Now - this.items[this.items.Count - 1].creationDate).TotalMinutes > 30); // [this.items.Count - 1] -> last item added to the list
        }

        public bool IsValid()
        {
            if (this.items.Count > 10)
            {
                return false;
            }

            // Useless verification since it is impossible to add an invalid item in the list
            // Unless force pushing in the list with items.Add()
            foreach (IItem item in this.items)
            {
                if (!item.IsValid())
                {
                    return false;
                }
            }

            return true;
        }

        public void SendMail()
        {
            /**
             * A piece of code using a nice API to send a mail to the customer
             **/
            this.mailSent = true;
        }
    }
}