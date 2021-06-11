using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ToDoList
{
    [TestClass]
    public class Test_Item
    {
        /**
         * IsValid method :
         * TRUE if name is set AND content length is < 1000 characters AND creation date is set
         **/
        [TestMethod]
        public void IsValid()
        {
            Assert.IsTrue(new Item()
            {
                name = "item",
                content = "content",
                creationDate = DateTime.Now,
            }.IsValid());
        }

        [TestMethod]
        public void IsValid_NoNameSet()
        {
            Assert.IsFalse(new Item()
            {
                content = "content",
                creationDate = DateTime.Now,
            }.IsValid());
        }

        [TestMethod]
        public void IsValid_NoCreationDateSet()
        {
            Assert.IsFalse(new Item() // No creation date
            {
                name = "item",
                content = "content",
            }.IsValid());
        }
    }
}