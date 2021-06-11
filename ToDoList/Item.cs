using System;

namespace ToDoList
{
    public interface IItem
    {
        string name { get; set; }
        string content { get; set; }
        DateTime creationDate { get; set; }

        public bool IsValid();
    }

    public class Item : IItem
    {
        public string name { get; set; }
        public string content { get; set; }
        public DateTime creationDate { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.name)
                && this.content.Length < 1000
                && this.creationDate != default(DateTime); // DateTime is non nullable, instead it is initialized at its minimal value
        }
    }
}
