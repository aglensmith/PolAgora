using System.Collections.Generic;
using System.Threading.Tasks;
using Polagora.Models;
using Polagora.UtilityClasses;
using ApiCaller;
using System.Configuration;
using System.Threading;

namespace PolagoraWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            //add try catch
            SnapshotManager SnapshotManager = new SnapshotManager() { DBContext = new PolagoraContext() };

            //Call Twitter and Facebook
            string FacebookToken = ConfigurationManager.AppSettings["FacebookToken"];
            string TwitterToken = ConfigurationManager.AppSettings["TwitterBearer"];
            Dictionary<string, FacebookCaller.FacebookResponse> 
                FacebookResponses = await FacebookCaller.CallFacebookAsync(SnapshotManager.FacebookIDs, FacebookToken);
            Dictionary<string, TwitterCaller.TwitterResponse> 
                TwitterResponses = await TwitterCaller.CallTwitterAsync(SnapshotManager.TwitterIDs, TwitterToken);

            //Insert Snapshots into DB
            SnapshotManager.InsertSnapshots(FacebookResponses, TwitterResponses);
        }

    }
}
