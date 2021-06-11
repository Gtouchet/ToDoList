using System;
using EmailValidation;

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

    public class User : IUser
    {
        public string mailAddress { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime birthDate { get; set; }
        public ToDoList toDoList { get; set; }

        public User() { }
        public User(IUser properties)
        {
            this.mailAddress = properties.mailAddress;
            this.firstName = properties.firstName;
            this.lastName = properties.lastName;
            this.password = properties.password;
            this.birthDate = properties.birthDate;
            this.toDoList = properties.toDoList;
        }

        public bool IsValid()
        {
            return EmailValidator.Validate(this.mailAddress)
                && !string.IsNullOrEmpty(this.firstName)
                && !string.IsNullOrEmpty(this.lastName)
                && this.IsPasswordValid()
                && this.IsBirthDateValid();
        }

        public bool IsPasswordValid()
        {
            return this.password.Length >= 8 && this.password.Length <= 40;
        }

        public bool IsBirthDateValid()
        {
            return new DateTime(DateTime.Now.Subtract(this.birthDate).Ticks).Year - 1 > 13;
        }
    }
}
