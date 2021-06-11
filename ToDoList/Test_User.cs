using EmailValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ToDoList
{
    [TestClass]
    public class Test_User
    {
        /**
         * IsValid method :
         * TRUE if mail address is valid AND names field are set AND password length is >= 8 and <= 40 AND user is thirteen years old minimum
         **/
        [TestMethod]
        public void IsValid()
        {
            Assert.IsTrue(new User()
            {
                mailAddress = "jb1985@yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "leNomDeMonChat",
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList(),
            }.IsValid());
        }

        [TestMethod]
        public void IsValid_MailAddressIsNotValid()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb1985yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "leNomDeMonChat",
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList(),
            }.IsValid());
        }

        [TestMethod]
        public void IsValid_PasswordIsTooShort()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb1985@yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "fifi12",
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList(),
            }.IsValid());
        }

        [TestMethod]
        public void IsValid_UserIsTooYoung()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb2015@yahoo.fr",
                firstName = "Jean",
                lastName = "Baptiste",
                password = "leNomDeMonChat",
                birthDate = new DateTime(2015, 4, 30, 0, 0, 0), // Too young
                toDoList = new ToDoList(),
            }.IsValid());
        }

        /**
         * EmailValidation validate method :
         * from : http://www.mimekit.net/
         **/
        [TestMethod]
        public void EmailValidator_Validate()
        {
            Assert.IsTrue(EmailValidator.Validate("john.smith.53@mailservice.uk"));
            Assert.IsTrue(EmailValidator.Validate("john.smith.53@mailservice.uk.fr"));
            Assert.IsFalse(EmailValidator.Validate("john.smith.53@mailservice")); // No .something
            Assert.IsFalse(EmailValidator.Validate("john.smith.53mailservice.uk")); // No '@'
        }

        /**
         * IsPasswordValid method :
         * TRUE if password length is >= 8 and <= 40
         **/
        [TestMethod]
        public void IsPasswordValid()
        {
            Assert.IsTrue(new User()
            {
                password = "monPseudoEnLigneSuivitDe`Du91`",
            }.IsPasswordValid());
        }

        [TestMethod]
        public void IsPasswordValid_TooShort()
        {
            Assert.IsFalse(new User()
            {
                password = "1234",
            }.IsPasswordValid());
        }

        [TestMethod]
        public void IsPasswordValid_TooLong()
        {
            Assert.IsFalse(new User()
            {
                password = "unMotDePasseBeauuuuuucoupTropLongDePlusDe40Caractères",
            }.IsPasswordValid());
        }

        /**
         * IsBirthDateValid method :
         * TRUE if the user is at least thirteen years old
         **/
        [TestMethod]
        public void IsBirthDateValid()
        {
            Assert.IsTrue(new User()
            {
                birthDate = new DateTime(1992, 4, 30, 0, 0, 0),
            }.IsBirthDateValid());
        }

        [TestMethod]
        public void IsBirthDateValid_TooYoung()
        {
            Assert.IsFalse(new User()
            {
                birthDate = new DateTime(2010, 4, 30, 0, 0, 0),
            }.IsBirthDateValid());
        }
    }
}