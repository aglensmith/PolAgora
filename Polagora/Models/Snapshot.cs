using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polagora.Models
{
    public class Snapshot
    {
        // Key needs to be composite of: 
        // candidate id
        // DateTime or Timestamp
        //TwitterFollowers
        //FacebookLikes

        [Key]
        [Required]
        [Column(Order = 1)]
        public int CandidateID { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public DateTime Time { get; set; }

        public int FacebookLikes { get; set; }
        public int TwitterFollowers { get; set; }
    }
}