using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI_LAB1;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
             Node n = new Node(new List<int>() { 1, 2, 3, 4, 6 });

             Stack<Node> nodes = new Stack<Node>();
             nodes.Push(new Node(new List<int>() { 1, 2, 3 }));
             nodes.Push(new Node(new List<int>() { 1, 2, 3, 7, 8 }));
             nodes.Push(new Node(new List<int>() { 1, 2, 3, 4, 6}));
             nodes.Push(new Node(new List<int>() { 1, 2, 3,4 }));

             Assert.AreEqual(true, nodes.Contains(n));
            
        }
    }
}
