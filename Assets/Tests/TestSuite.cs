using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Tests
{
    public class TestSuite
    {
        [Test]
        public void FastListTest()
        {
            //List<int> ints = new List<int>();
            //FastList<string> valueTypes = new FastList<string>();
            //Debug.Log("ints initial count: " + ints.Count);
            //ints.Capacity = 4000;
            //Debug.Log("Capacity: " + ints.Capacity);
            //Debug.Log("Inner Length: " + ints.InnerArray.Length);
            //List<int> intList = new List<int>();
            //for (int i = 0; i < 200; i++)
            //{
            //    valueTypes.Add($"{i}.a..");
            //    intList.Add(0);
            //}
            //ints.AddRange(intList);
            //valueTypes.ForEach(e => Debug.Log(e));
            //for (int i = 0; i < valueTypes.Count; i++)
            //{
            //    valueTypes.InnerArray[i] = "aaa";
            //}
            //valueTypes.ForEach(e => Debug.Log(e));
            //for (int i = 0; i < ints.Count; i++)
            //{
                //ints.InnerArray[i] = 1;
            //}
            //ints.ForEach((e) => Debug.Log(e));
            //ints.ForEach((e) => e = 3);
            //ints.ForEach((e) => Debug.Log(e));
            //Debug.Log("index " + ints.IndexOf(1));
            //for (int i = 0; i < 4500; i++)
            //{
                //ints.Add(3);
            //}
            //Debug.Log("8500 Count: " + ints.Count);
            //Debug.Log("3: " + ints[2000]);
            
            //ints.ForEach(e => e = 9);
            //ints.ForEach(e => Debug.Log("are they 9? " + e));

            //FastList<TestClass> fastClassList = new FastList<TestClass>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    fastClassList.Add(new TestClass()
            //    {
            //        ID = i,
            //        Name = $"Smith{i + 5}"
            //    });
            //}
            //fastClassList.ForEach(e => Debug.Log(e.ID + " : " + e.Name));
            //fastClassList.ForEach(e => { e.ID = default; e.Name = "John";});
            //fastClassList.ForEach(e => Debug.Log(e.ID + " " + e.Name));
            //Assert.That(fastClassList.Count, Is.EqualTo(10000));
            //
            //for (int i = 0; i < 10000; i++)
            //{
            //    fastClassList.InnerArray[i].Name = "N/A";
            //    Assert.That(fastClassList[i].Name == "N/A", "inner array not updated!");
            //}
            //fastClassList.ForEach(e => Debug.Log(e.ID + " " + e.Name));
            //Debug.Log("---------------------------------------------");
            //List<TestClass> normalStructs = new List<TestClass>();
            //for (int i = 0; i < 100; i++)
            //{
            //    normalStructs.Add(new TestClass()
            //    {
            //        ID = i,
            //        Name = $"Smith{i + 5}"
            //    });
            //}
            //normalStructs.ForEach(e => Debug.Log(e.ID + " : " + e.Name));
            //normalStructs.ForEach(e => { e.ID = default; e.Name = "John"; });
            //normalStructs.ForEach(e => Debug.Log(e.ID + " " + e.Name));
            //var art = des as int[];
            //or (int i = 0; i < ints.Count; i++)
            //{
            //ints.Array[i] = i;
        }

        public class TestClass
        {
            public int ID;
            public string Name;
        }

        public class TestStruct
        {
            public int ID;
            public string Name;
        }

    }
}