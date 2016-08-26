using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Microsoft.Azure.WebJobs;
using Polagora.UtilityClasses;
using Polagora.Models;
using System.Diagnostics;
using System;

namespace SnapshotsWebJob
{
    public class Functions
    {
        //PolagoraContext db = new PolagoraContext();
        
        // This function will be triggered based on the schedule you have set for this WebJob
        [NoAutomaticTrigger]
        public async Task stuff (PolagoraContext value)
        {
            DBUpdater Updater = new DBUpdater();
            await Updater.Update(value);
            Debug.WriteLine("Updated DB");
            Console.WriteLine("Updated DB");
        }
        
        // This function will enqueue a message on an Azure Queue called queue
        //[NoAutomaticTrigger]
        //public static void ManualTrigger(TextWriter log, int value, [Queue("queue")] out string message)
        //{
        //    log.WriteLine("Function is invoked with value={0}", value);
        //    message = value.ToString();
        //    log.WriteLine("Following message will be written on the Queue={0}", message);
        //}
    }
}
