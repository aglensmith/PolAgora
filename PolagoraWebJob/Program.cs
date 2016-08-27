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
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            //add try catch
            PolagoraContext DBContext = new PolagoraContext();
            DBUpdater Updater = new DBUpdater();

            Updater.FacebookToken = ConfigurationManager.AppSettings["FacebookToken"];
            Updater.TwitterToken = ConfigurationManager.AppSettings["TwitterBearer"];

            await Updater.Update(DBContext);
        }

    }
}
