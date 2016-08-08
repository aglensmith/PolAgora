using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polagora.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string TwitterID { get; set; }
        public string FacebookID { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        public string Party { get; set; }

        public string TwitterURL { get; set; }
        public string FacebookURL { get; set; }
        public string CampaignURL { get; set; }

        public string FacebookCoverPhoto { get; set; }
        public string TwitterProfilePic { get; set; }

        [Display(Name = "Twitter Followers")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TwitterFollowers { get; set; }

        [Display(Name = "Facebook Likes")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int FacebookLikes { get; set; }

        public virtual ICollection<Snapshot> Snapshots { get; set; }
    }
}