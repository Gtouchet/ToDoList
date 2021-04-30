using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ToDoList
{
    [TestClass]
    public class Test_User
    {
        [TestMethod]
        public void UserIsValid()
        {
            Assert.IsTrue(new User()
            {
                mailAddress = "jb1985@yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "leNomDeMonChat",
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList()
            }.IsValid());
        }

        [TestMethod]
        public void UserMailAddressIsNotValid()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb1985yahoo.fr", // Missing the @ sign
                firstName = "Jean",
                lastName = "Bernard",
                password = "leNomDeMonChat",
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList()
            }.IsValid());
        }

        [TestMethod]
        public void UserPasswordIsNotValid()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb1985@yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "fifi12", // Too short
                birthDate = new DateTime(1985, 4, 30, 0, 0, 0),
                toDoList = new ToDoList()
            }.IsValid());
        }

        [TestMethod]
        public void UserAgeIsNotValid()
        {
            Assert.IsFalse(new User()
            {
                mailAddress = "jb1985@yahoo.fr",
                firstName = "Jean",
                lastName = "Bernard",
                password = "leNomDeMonChat",
                birthDate = new DateTime(2015, 4, 30, 0, 0, 0), // Too young
                toDoList = new ToDoList()
            }.IsValid());
        }

        [TestMethod]
        public void PasswordIsValid()
        {
            Assert.IsTrue(new User().IsPasswordValid("monPseudoEnLigneSuivitDe`Du91`"));
        }

        [TestMethod]
        public void PasswordIsNotValid()
        {
            Assert.IsFalse(new User().IsPasswordValid("1234"));
        }

        [TestMethod]
        public void BirthDateIsValid()
        {
            // todo
        }

        [TestMethod]
        public void BirthDateIsNotValid()
        {
            // todo
        }
    }
}