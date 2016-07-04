using System.Collections.Generic;
using System.Linq;
using Polagora.Models;
using System;

namespace Polagora.UtilityClasses
{
    public class PropertiesToList
    {
        //returns as list of facebook IDs from a list of candidates
        public static List<string> FacebookIDsToList(List<Candidate> Candidates)
        {
            List<string> FacebookIDs = new List<string>();

            foreach (Candidate Candidate in Candidates)
            {
                if (Candidate.FacebookID != null)
                {
                    FacebookIDs.Add(Candidate.FacebookID);
                }       
            }

            return FacebookIDs;
        }

        //returns a list of twitter ids from a list of candidates
        public static List<string> TwitterIDsTolist(List<Candidate> Candidates)
        {
            List<string> TwitterIDs = new List<string>();

            foreach (Candidate Candidate in Candidates)
            {
                if (Candidate.TwitterID != null)
                {
                    TwitterIDs.Add(Candidate.TwitterID);
                }
            }

            return TwitterIDs;
        }
    }
}