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

        [Test]
        public void Add_New_Offer()
        {
            Offer temp = new Offer(8, new DateTime(2008, 3, 1, 7, 0, 0), new DateTime(2009, 3, 1, 7, 0, 0),
                "Ubezpieczenie testowe", "Test content", 500, "California", "Travel");
            Assert.That(temp.Id, Is.EqualTo(8));
            Assert.That(temp.Date.Day, Is.EqualTo(1));
            Assert.That(temp.EndDate.Value.Year, Is.EqualTo(2009));
            Assert.That(temp.Title, Is.EqualTo("Ubezpieczenie testowe"));
            Assert.That(temp.Content, Is.EqualTo("Test content"));
            Assert.That(temp.BasePrice, Is.EqualTo(500));
            Assert.That(temp.Place, Is.EqualTo("California"));
            Assert.That(temp.Purpose, Is.EqualTo("Travel"));
        }

        [Test]
        public void Add_New_Offer_To_Catalog()
        {
            Catalog temp_catalog = new Catalog();
            temp_catalog.Offers.Add(new Offer(8, new DateTime(2008, 3, 1, 7, 0, 0), new DateTime(2009, 3, 1, 7, 0, 0),
                "Ubezpieczenie testowe", "Test content", 500, "California", "Travel"));

            Assert.That(temp_catalog.Offers.Count, Is.EqualTo(3));
        }

        [Test]
        public void Delete_Offer_From_Catalog()
        {
            Catalog temp_catalog = new Catalog();

            Assert.That(temp_catalog.Offers.Count, Is.EqualTo(2));

            Offer new_offer = new Offer(8, new DateTime(2008, 3, 1, 7, 0, 0), new DateTime(2009, 3, 1, 7, 0, 0),
                "Ubezpieczenie testowe", "Test content", 500, "California", "Travel");

            temp_catalog.Offers.Add(new_offer);

            Assert.That(temp_catalog.Offers.Count, Is.EqualTo(3));

            temp_catalog.DeleteOfferFromList(new_offer);

            Assert.That(temp_catalog.Offers.Count, Is.EqualTo(2));
        }

        [Test]
        public void Get_Offer_By_Id_From_Catalog()
        {
            Catalog temp_catalog = new Catalog();

            Offer new_offer = new Offer(8, new DateTime(2008, 3, 1, 7, 0, 0), new DateTime(2009, 3, 1, 7, 0, 0),
                "Ubezpieczenie testowe", "Test content", 500, "California", "Travel");

            temp_catalog.Offers.Add(new_offer);

            Assert.That(new_offer, Is.EqualTo(temp_catalog.GetOfferById(8)));
        }

        [Test]
        public void Update_Offer_by_Id_From_Catalog()
        {
            Catalog temp_catalog = new Catalog();

            Offer new_offer = new Offer(8, new DateTime(2008, 3, 1, 7, 0, 0), new DateTime(2009, 3, 1, 7, 0, 0),
                "Ubezpieczenie testowe", "Test content", 500, "California", "Travel");

            temp_catalog.Offers.Add(new_offer);

            temp_catalog.updateOfferInDb(8); // update base price in new_offer to 600

            Assert.That(temp_catalog.GetOfferById(8).BasePrice, Is.EqualTo(600));
        }

    }
}