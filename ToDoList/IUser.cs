using System;

namespace ToDoList
{
    public interface IUser
    {
        public string mailAddress { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime birthDate { get; set; }
        public ToDoList toDoList { get; set; }

        public bool IsValid();
    }
}
