﻿using System;
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
