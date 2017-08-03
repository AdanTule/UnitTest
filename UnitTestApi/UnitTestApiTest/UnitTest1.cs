using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestApi.Service;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("1234", "1234");
        }

        public class MockStorage: IDataStorage
        {
            List<string> names = new List<string>();

            public List<string> NameList { get
                {
                    return names;
                }
            }
            public int Insert(string name) //Function to insert a name to names 
            {
                names.Add(name);
                return names.Count;
            }

            public string Select() // Simple Select function return.
            {
                return "Adan";
            }

            public int Update(string oldName, string newName) //function to update item in names
            {
                string exists = names.Where(n => n == oldName).FirstOrDefault(); //search through names and sets exist to oldName

                if (!string.IsNullOrEmpty(exists)) //check if string is not null or empty
                {
                    names = names.Select(n => n.Replace(exists, newName)).ToList(); // Adds newName to names 
                }

                return names.Count;
            }

            public void Delete(string id) //function to remove item from names
            {
                names.Remove(id);
            }
        }


        [TestMethod]
        public void TestCrudService()
        {
            int expectedId = 2;
            string readId = "Adan";
            string updateId = "Sonny";

            MockStorage ms = new MockStorage();
            CrudService cs = new CrudService(ms);
            cs.Create("Adan"); //Adding to NameList
            cs.Create("Jae");
            cs.Create("Tiana");
            cs.Delete("test"); //Deleting from NameList


            Assert.AreEqual(expectedId, ms.NameList.Count); // compare 2 to the Namelist count
            Assert.AreEqual(readId, ms.Select()); //comparing Adan to value in Select function
            Assert.AreEqual(expectedId, ms.Update(readId, updateId)); // updating Adan to Sonny
            Assert.AreEqual(updateId, ms.NameList.Where(n => n == "Sonny").FirstOrDefault()); //Checking to if Sonny exist in NameList
        }
    }
}
