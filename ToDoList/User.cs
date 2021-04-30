using System;
using EmailValidation;

namespace ToDoList
{
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
                && IsPasswordValid(this.password)
                && IsBirthDateValid(this.birthDate);
        }

        public bool IsPasswordValid(string password)
        {
            return password.Length >= 8 && password.Length <= 40;
        }

        public bool IsBirthDateValid(DateTime birthDate)
        {
            return new DateTime(DateTime.Now.Subtract(birthDate).Ticks).Year - 1 > 13;
        }
    }
}
