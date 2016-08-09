using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using Polagora.Models;
using ApiCaller;
using System.Threading.Tasks;
using System;


namespace Polagora.UtilityClasses
{
    public class DBUpdater
    {
        //Gets followers & likes, updates candidate fields, creates new snapshots
        public async Task Update(PolagoraContext db)
        {
            //Get candidates from DB
            List<Candidate> Candidates = db.Candidates.ToList();

            List<string> TwitterIDs = Candidates.Select(c => c.TwitterID).ToList();
            List<string> FacebookIDs = Candidates.Select(c => c.FacebookID).ToList();

            //Call facebook and twitter
            Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses =
                await FacebookCaller.CallFacebookAsync(FacebookIDs);

            Dictionary<string, TwitterCaller.TwitterResponse> TwitterResponses =
                await TwitterCaller.CallTwitterAsync(TwitterIDs);

            //Update the DB with the new counts
            DateTime CallTime = DateTime.UtcNow;
            foreach (Candidate candidate in Candidates)
            {
                db.Snapshots.Add(new Snapshot
                {
                    CandidateID = candidate.ID,
                    Time = CallTime,
                    FacebookLikes = FacebookResponses[candidate.FacebookID.ToString()].likes,
                    TwitterFollowers = TwitterResponses[candidate.TwitterID.ToString()].followers_count
                });

                candidate.FacebookLikes = FacebookResponses[candidate.FacebookID.ToString()].likes;
                candidate.TwitterFollowers = TwitterResponses[candidate.TwitterID.ToString()].followers_count;
            }
            
            db.SaveChanges();
        }
    }


}