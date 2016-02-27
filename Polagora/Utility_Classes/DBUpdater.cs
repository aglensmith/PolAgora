using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using Polagora.Models;
using ApiCaller;
using System.Threading.Tasks;


namespace Polagora.UtilityClasses
{
    public class DBUpdater
    {
        public async Task Update(PolagoraContext db)
        {
            List<Candidate> Candidates = db.Candidates.ToList();
            List<string> TwitterIDs = new List<string>();
            List<string> FacebookIDs = new List<string>();
            string TwitterToken = WebConfigurationManager.AppSettings["TwitterBearer"];
            string FacebookToken = WebConfigurationManager.AppSettings["FacebookToken"];

            foreach (Candidate Candidate in Candidates)
            {
                TwitterIDs.Add(Candidate.TwitterID);

                if (Candidate.FacebookID != null)
                {
                    FacebookIDs.Add(Candidate.FacebookID);
                }
            }

            List<TwitterCaller.TwitterResponse> TwitterResponses =
                await TwitterCaller.CallTwitterAsync(TwitterIDs, TwitterToken);

            Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses =
                await FacebookCaller.CallFacebookAsync(FacebookIDs, FacebookToken);


            //Split this into another class/method to seperate the calls and DB work
            foreach (TwitterCaller.TwitterResponse Response in TwitterResponses)
            {
                Candidate Candidate = (from c in db.Candidates where c.TwitterID == Response.id_str select c).FirstOrDefault();
                Candidate.TwitterFollowers = Response.followers_count;
            }
            
            foreach (KeyValuePair<string, FacebookCaller.FacebookResponse> Response in FacebookResponses)
            {
                string id = Response.Value.id;
                Candidate Candidate = (from c in db.Candidates where c.FacebookID == id select c).FirstOrDefault();
                Candidate.FacebookLikes = Response.Value.likes;                 
            }

            db.SaveChanges();

        }
    }
}