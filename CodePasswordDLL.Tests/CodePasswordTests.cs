using CodePasswordDLL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CodePasswordDLL.Tests
{
    [TestClass]
    public class CodePasswordTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Debug.WriteLine("Test Cleanup");
        }
        [TestMethod]
        public void getCodPassword_abc_bcd()
        {
            //arrange
            string strIn = "abc";
            string strExpected = "bcd";

            //act
            string strActual = CodePassword.getCodPassword(strIn);
            Debug.WriteLine(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual, "Переменные отличаются: выходит " + strActual + " а должно " + strExpected);
            StringAssert.Equals(strExpected, strActual);
        }

        [TestMethod()]
        public void getCodPassword_empty_empty()
        {
            // arrange 
            string strIn = "";
            string strExpected = "";
            // act 
            string strActual = CodePassword.getCodPassword(strIn);
            //assert
            Assert.AreEqual(strExpected, strActual);
        }

        [TestMethod]
        public void getPassword_abc_bcd()
        {
            //arrange
            string strExpected = "abc";
            string strIn = "bcd";

            //act
            string strActual = CodePassword.getPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }
        [TestMethod]
        public void getPassword_empty_empty()
        {
            //arrange
            string strIn = "";
            string strExpected = "";

            //act
            string strActual = CodePassword.getPassword(strIn);

            //assert
            Assert.AreEqual(strExpected, strActual);
        }
    }
}
