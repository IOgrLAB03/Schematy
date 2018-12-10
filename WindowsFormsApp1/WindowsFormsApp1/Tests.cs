using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

/*
    To run your unit tests in Visual Studio, you first need to install the NUnit 3 Test Adapter. To do so,

    In the main menu, click Tools | Extensions and Updates…
    Click on Online | Visual Studio Gallery,
    Search for NUnit and install NUnit 3 Test Adapter
    After you install, click the button at the bottom to Restart Visual Studio.

    Back in Visual Studio with your solution open, you need to open the Test Explorer Window. To do this, in the main menu, click Test | Windows | Test Explorer.
 */

namespace WindowsFormsApp1
{
    [TestFixture]
    class Tests
    {
        
        [Test]
        public void Test_Success()
        {
            Assert.That(5, Is.EqualTo(5));
        }

        [Test]
        public void Test_Fail()
        {
            Assert.That(2, Is.EqualTo(5));
        }
    }
}
