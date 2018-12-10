using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace WindowsFormsApp1
{
    [TestFixture]
    class Class1
    {
        private Catalog sampleCatalog;

        public List<string> expectedReadOffersResult = new List<string>() { "sfgsdfsd", "dfgdf" };

        /*[SetUp]
        public void Init()
        {
            sampleCatalog = new Catalog();
        }

        [Test(Description = "sdfsd sdfsdfs")]
        public void SampleMethod()
        {
            var result = sampleCatalog.readOffers();

            Assert.AreEqual(expectedReadOffersResult, result);
        }*/
    }
}
