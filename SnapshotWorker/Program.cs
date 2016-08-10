using System.Threading.Tasks;
using Polagora.UtilityClasses;
using Polagora.Models;

namespace SnapshotWorker
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static async void doStuff()
        {
            PolagoraContext db = new PolagoraContext();
            
            //add handling for System.ComponentModel.Win32Exception: The wait operation timed out
            DBUpdater Updater = new DBUpdater();
            await Updater.Update(db);
        }
    }
}
