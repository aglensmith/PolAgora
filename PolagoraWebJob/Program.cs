using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polagora.Models;
using System.Data.Entity;
using Polagora.UtilityClasses;
using ApiCaller;
using System.Data.Sql;
using System.Diagnostics;
using System.Configuration;

//IDEA: Make tokens a class property so that it can be instatiated in web app or console app with same code
//Checkout API Console App template
//Seperate updates into seperate methods: InsertSnapshots

namespace PolagoraWebJob
{
    public class DBUpdater
    {
        //Gets followers & likes, updates candidate fields, creates new snapshots
        public async Task Update(PolagoraContext db)
        {
            //Tokens
            string FacebookToken = ConfigurationManager.AppSettings["FacebookToken"];
            string TwitterToken = ConfigurationManager.AppSettings["TwitterBearer"];

            //Get candidates from DB
            Debug.WriteLine("Starting Update...");
            List<Candidate> Candidates = db.Candidates.ToList();

            List<string> TwitterIDs = Candidates.Select(c => c.TwitterID).ToList();
            List<string> FacebookIDs = Candidates.Select(c => c.FacebookID).ToList();

            //Call facebook and twitter
            Dictionary<string, FacebookCaller.FacebookResponse> FacebookResponses =
                await FacebookCaller.CallFacebookAsync(FacebookIDs, FacebookToken);

            Dictionary<string, TwitterCaller.TwitterResponse> TwitterResponses =
                await TwitterCaller.CallTwitterAsync(TwitterIDs, TwitterToken);

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
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {

            //add try catch
            PolagoraContext dbctx = new PolagoraContext();
            await stuff(dbctx);
        }

        static async Task stuff(PolagoraContext db)
        {
            Console.WriteLine("updating...");
            Debug.WriteLine("Updating...");

            var x = db.Candidates.ToList();

            foreach (Candidate candidate in x)
            {
                Debug.WriteLine("Candidate: {0}", candidate.FirstName);
            }

            PolagoraWebJob.DBUpdater Updater = new PolagoraWebJob.DBUpdater();
            await Updater.Update(db);
            Console.WriteLine("DB updated");
            Debug.WriteLine("Updated DB");
        }
}
