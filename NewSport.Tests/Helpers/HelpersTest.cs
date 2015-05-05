using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewSport.WebApi.HtmlHelpers;

namespace NewSport.Tests.Helpers
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TimeHelperTest()
        {
            HtmlHelper helper = null;
            string result = helper.HowMuchTimeHasPassedSinceSbAddedComment(DateTime.Now);
            Assert.AreEqual("0 sekund temu",result);
        }
    }
}
