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

        //Gets follower and like count from facbook and twitter, then updates DB
        public async Task Update(PolagoraContext db)
        {

            //Get candidates from DB
            List<Candidate> Candidates = db.Candidates.ToList();

            List<string> TwitterIDs = PropertiesToList.TwitterIDsTolist(Candidates);
            List<string> FacebookIDs = PropertiesToList.FacebookIDsToList(Candidates);

            //Get tokens from app settings
            string TwitterToken = WebConfigurationManager.AppSettings["TwitterBearer"];
            string FacebookToken = WebConfigurationManager.AppSettings["FacebookToken"];

            //Call twitter and get newest follower count -- split into diff method
			List<TwitterCaller.TwitterResponse> TwitterResponses =
                await TwitterCaller.CallTwitterAsync(TwitterIDs, TwitterToken);
			
			//Call facebook and get newest like count -- split into diff method
            Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses =
                await FacebookCaller.CallFacebookAsync(FacebookIDs, FacebookToken);


            //Update the DB with the new counts
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

        public async Task AddSnapshot(PolagoraContext db)
        {
            //Get candidates from DB
            List<Candidate> Candidates = db.Candidates.ToList();

            List<string> TwitterIDs = PropertiesToList.TwitterIDsTolist(Candidates);
            List<string> FacebookIDs = PropertiesToList.FacebookIDsToList(Candidates);

            //Get tokens from app settings
            string TwitterToken = WebConfigurationManager.AppSettings["TwitterBearer"];
            string FacebookToken = WebConfigurationManager.AppSettings["FacebookToken"];

            //Call twitter and get newest follower count -- split into diff method
			List<TwitterCaller.TwitterResponse> TwitterResponses =
                await TwitterCaller.CallTwitterAsync(TwitterIDs, TwitterToken);
			
			//Call facebook and get newest like count -- split into diff method
            Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses =
                await FacebookCaller.CallFacebookAsync(FacebookIDs, FacebookToken);


            //Update the DB with the new counts
            foreach (TwitterCaller.TwitterResponse Response in TwitterResponses)
            {
            }

            foreach (KeyValuePair<string, FacebookCaller.FacebookResponse> Response in FacebookResponses)
            {
            }

            db.SaveChanges();
        }
    }


}