using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Configuration;

namespace Polagora.Tests.Utility_Classes
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            string TwitterToken = WebConfigurationManager.AppSettings["TwitterBearer"];
            string FacebookToken = WebConfigurationManager.AppSettings["FacebookToken"];
            Assert.IsNotNull(TwitterToken);
            Assert.IsNotNull(FacebookToken);
            Assert.IsInstanceOfType(TwitterToken, typeof(string));
            Assert.IsInstanceOfType(FacebookToken, typeof(string));
        }
    }
}
