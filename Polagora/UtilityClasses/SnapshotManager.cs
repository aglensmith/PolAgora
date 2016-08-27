using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using Polagora.Models;
using ApiCaller;
using System.Threading.Tasks;
using System;
using System.Diagnostics;


namespace Polagora.UtilityClasses
{
    public class SnapshotManager
    {
        public List<Candidate> Candidates { get; set; }

        public List<string> FacebookIDs { get; set; }

        public List<string> TwitterIDs { get; set; }

        private PolagoraContext _dbcontext;
        public PolagoraContext DBContext
        {
            get
            {
                return _dbcontext;
            }

            set
            {
                _dbcontext = value;
                Candidates = DBContext.Candidates.ToList();
                FacebookIDs = Candidates.Select(c => c.FacebookID).ToList();
                TwitterIDs = Candidates.Select(c => c.TwitterID).ToList();
            }
        }

        //Adds new Snapshots to Snapshot Table, updates Candidate Table 
        public void InsertSnapshots(Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses, 
                                    Dictionary<string, TwitterCaller.TwitterResponse> TwitterResponses)
        {

            DateTime CallTime = DateTime.UtcNow;
            foreach (Candidate candidate in Candidates)
            {
                DBContext.Snapshots.Add(new Snapshot
                {
                    CandidateID = candidate.ID,
                    Time = CallTime,
                    FacebookLikes = FacebookResponses[candidate.FacebookID.ToString()].likes,
                    TwitterFollowers = TwitterResponses[candidate.TwitterID.ToString()].followers_count
                });

                candidate.FacebookLikes = FacebookResponses[candidate.FacebookID.ToString()].likes;
                candidate.TwitterFollowers = TwitterResponses[candidate.TwitterID.ToString()].followers_count;
            }

            DBContext.SaveChanges();
        }

    }


}