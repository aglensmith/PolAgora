using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using Polagora.Models;
using ApiCaller;
using System.Threading.Tasks;
using System.Diagnostics;
using Polagora.UtilityClasses;

namespace Polagora.Controllers
{
    public class CandidatesController : Controller
    {
        private PolagoraContext db = new PolagoraContext();

        public async Task Update()
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

            Debug.WriteLine(FacebookIDs[0]);
            Debug.WriteLine(FacebookResponses[FacebookIDs[0]]);

            foreach (TwitterCaller.TwitterResponse Response in TwitterResponses)
            {
                Candidate Candidate = (from c in db.Candidates where c.TwitterID == Response.id_str select c).FirstOrDefault();
                Candidate.TwitterFollowers = Response.followers_count;
            }

            foreach (KeyValuePair<string, FacebookCaller.FacebookResponse> Response in FacebookResponses)
            {
                Candidate Candidate = (from c in db.Candidates where c.FacebookID == Response.Key select c).FirstOrDefault();
                Candidate.FacebookLikes = Response.Value.likes;
            }

            db.SaveChanges();

        }

        // GET: Candidates
        public async Task<ActionResult> Index()
        {
            if (db.Candidates.ToList().Count > 0)
            {
                //add handling for System.ComponentModel.Win32Exception: The wait operation timed out

                //DBUpdater Updater = new DBUpdater();
                //await Updater.Update(db);
            }
            //DBUpdater Updater = new DBUpdater();
            //await Updater.Update(db);
            return View(db.Candidates.ToList());
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TwitterID,FacebookID,LastName,FirstName,Party,TwitterURL,FacebookURL,CampaignURL,FacebookCoverPhoto,TwitterProfilePic,TwitterFollowers,FacebookLikes")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TwitterID,FacebookID,LastName,FirstName,Party,TwitterURL,FacebookURL,CampaignURL,FacebookCoverPhoto,TwitterProfilePic,TwitterFollowers,FacebookLikes")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
