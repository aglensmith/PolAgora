using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Configuration;
using Polagora.UtilityClasses;
using System.Collections.Generic;
using System.Data;
using Polagora.Models;

namespace Polagora.Tests.Utility_Classes
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void PropertiesToListNotEmpty()
        {
            //Mocks a list of candidates
            Candidate MockCandidate1 = new Candidate();
            Candidate MockCandidate2 = new Candidate();
            MockCandidate1.TwitterID = "123";
            MockCandidate2.TwitterID = "123";
            MockCandidate1.FacebookID = "123";
            MockCandidate2.FacebookID = "123";
            List<Candidate> MockCandidateList = new List<Candidate>();
            MockCandidateList.Add(MockCandidate1);
            MockCandidateList.Add(MockCandidate2);

            List<string> FacebookIDs = PropertiesToList.FacebookIDsToList(MockCandidateList);
            List<string> TwitterIDs = PropertiesToList.FacebookIDsToList(MockCandidateList);

            Assert.IsTrue(FacebookIDs.Count != 0 && TwitterIDs.Count != 0);
        }
    }
}
