using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cis237Assignment6;
using cis237Assignment6.Controllers;

namespace cis237Assignment6.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;
            Assert.AreEqual("This is a database where you can see Wines.", result.ViewBag.Message);
        }

        [TestMethod]
        public void count()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Contact() as ViewResult;
            Assert.AreEqual("Reach out to us anytime!", result.ViewBag.Message);
        }

    }
}
