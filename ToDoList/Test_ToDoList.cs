using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ToDoList
{
    [TestClass]
    public class Test_ToDoList
    {
        private IItem GetMockIItem(bool? isValid = null, string name = null, string content = null, DateTime creationDate = default(DateTime))
        {
            IItem item = Mock.Of<IItem>();
            
            if (isValid.HasValue)
            {
                Mock.Get(item).Setup(x => x.IsValid()).Returns(isValid.Value);
            }

            if (name != null)
            {
                Mock.Get(item).Setup(x => x.name).Returns(name);
            }

            if (content != null)
            {
                Mock.Get(item).Setup(x => x.content).Returns(content);
            }

            if (creationDate != default(DateTime))
            {
                Mock.Get(item).Setup(x => x.creationDate).Returns(creationDate);
            }

            //Mock.Get(item).Setup(x => x.name).Callback<string>(x => changed = true);

            return item;
        }

        /**
         * IsValid method :
         * TRUE if list size <= 10 AND each item in the list is valid
         **/
        [TestMethod]
        public void IsValid()
        {
            ToDoList toDoList = new ToDoList();

            IItem firstItem = this.GetMockIItem(isValid: true);
            toDoList.AddItem(firstItem);
            IItem secondItem = this.GetMockIItem(isValid: true, creationDate: DateTime.Now.Subtract(new TimeSpan(1, 0, 0)));
            toDoList.AddItem(secondItem);
            IItem thirdItem = this.GetMockIItem(isValid: true, name: "thirdItem", creationDate: DateTime.Now);
            toDoList.AddItem(thirdItem);

            Assert.IsTrue(toDoList.items.Contains(firstItem));
            Assert.IsTrue(toDoList.items.Contains(secondItem));
            Assert.IsTrue(toDoList.items.Contains(thirdItem));
            Assert.IsTrue(toDoList.IsValid());
        }

        [TestMethod]
        public void IsValid_ContainsAnInvalideItem()
        {
            ToDoList toDoList = new ToDoList();

            IItem firstItem = this.GetMockIItem(isValid: true);
            toDoList.AddItem(firstItem);
            IItem secondItem = this.GetMockIItem(isValid: false, creationDate: DateTime.Now.Subtract(new TimeSpan(1, 0, 0)));
            toDoList.items.Add(secondItem); // Force pushing with List.Add, otherwise Additem would not add an invalid item to the list
            IItem thirdItem = this.GetMockIItem(isValid: true, name: "thirdItem", creationDate: DateTime.Now);
            toDoList.AddItem(thirdItem);

            Assert.IsTrue(toDoList.items.Contains(firstItem));
            Assert.IsTrue(toDoList.items.Contains(secondItem));
            Assert.IsTrue(toDoList.items.Contains(thirdItem));
            Assert.IsFalse(toDoList.IsValid());
        }

        [TestMethod]
        public void IsValid_ContainsMoreThanTenItems()
        {
            ToDoList toDoList = new ToDoList();

            for (int i = 0; i < 10; i += 1)
            {
                IItem newItem = this.GetMockIItem(isValid: true);
                toDoList.AddItem(newItem);

                Assert.IsTrue(toDoList.items.Contains(newItem));
            }

            IItem eleventhItem = this.GetMockIItem(isValid: true);
            toDoList.items.Add(eleventhItem); // Force pushing with List.Add, otherwise Additem would not add an eleventh item to the list

            Assert.IsTrue(toDoList.items.Contains(eleventhItem));
            Assert.IsFalse(toDoList.IsValid());
        }

        /**
         * AddItem method :
         * TRUE if item is valid AND item creation is possible
         **/
        [TestMethod]
        public void AddItem_AddingValidItem()
        {
            ToDoList toDoList = new ToDoList();

            IItem validItem = this.GetMockIItem(isValid: true);

            Assert.IsTrue(toDoList.AddItem(validItem));
            Assert.IsTrue(toDoList.items.Contains(validItem));
        }

        [TestMethod]
        public void AddItem_AddingInvalidItem()
        {
            ToDoList toDoList = new ToDoList();

            IItem invalidItem = this.GetMockIItem(isValid: false);

            Assert.IsFalse(toDoList.AddItem(invalidItem));
            Assert.IsFalse(toDoList.items.Contains(invalidItem));
        }

        /**
         * IsItemCreationPossible method :
         * TRUE if list size < 10 AND last item in the list was created more than thirty minutes ago
         **/
        [TestMethod]
        public void IsItemCreationPossible()
        {
            ToDoList toDoList = new ToDoList();

            IItem itemCreatedTwoHoursAgo = this.GetMockIItem(isValid: true, creationDate: DateTime.Now.Subtract(new TimeSpan(2, 0, 0)));
            toDoList.AddItem(itemCreatedTwoHoursAgo);
            IItem itemCreatedOneHoursAgo = this.GetMockIItem(isValid: true, creationDate: DateTime.Now.Subtract(new TimeSpan(1, 0, 0)));
            toDoList.AddItem(itemCreatedOneHoursAgo);

            Assert.IsTrue(toDoList.IsItemCreationPossible());
        }

        [TestMethod]
        public void IsItemCreationPossible_TenItemsOrMoreInTheList()
        {
            ToDoList toDoList = new ToDoList();

            for (int i = 0; i < 10; i += 1)
            {
                IItem item = this.GetMockIItem(isValid: true);
                toDoList.AddItem(item);

                Assert.IsTrue(toDoList.items.Contains(item));
            }

            Assert.IsFalse(toDoList.IsItemCreationPossible());
        }

        [TestMethod]
        public void IsItemCreationPossible_LastItemCreatedTenMinutesAgo()
        {
            ToDoList toDoList = new ToDoList();

            IItem itemCreatedTenMinutesAgo = this.GetMockIItem(isValid: true, creationDate: DateTime.Now.Subtract(new TimeSpan(0, 10, 0)));
            toDoList.AddItem(itemCreatedTenMinutesAgo);

            Assert.IsFalse(toDoList.IsItemCreationPossible());
        }


        /**
         * SendMail method :
         * mailSent should be TRUE after the 8th item was added
         **/
        [TestMethod]
        public void MailSender_MailHasBeenSent()
        {
            ToDoList toDoList = new ToDoList();

            for (int i = 0; i < 8; i += 1)
            {
                IItem newItem = this.GetMockIItem(isValid: true);
                toDoList.AddItem(newItem);

                Assert.IsTrue(toDoList.items.Contains(newItem));
            }

            Assert.AreEqual(toDoList.items.Count, 8);
            Assert.IsTrue(toDoList.mailSent);
        }

        [TestMethod]
        public void MailSender_MailHasNotBeenSent()
        {
            ToDoList toDoList = new ToDoList();

            for (int i = 0; i < 7; i += 1)
            {
                IItem newItem = this.GetMockIItem(isValid: true);
                toDoList.AddItem(newItem);

                Assert.IsTrue(toDoList.items.Contains(newItem));
            }

            Assert.AreEqual(toDoList.items.Count, 7);
            Assert.IsFalse(toDoList.mailSent);
        }
    }
}