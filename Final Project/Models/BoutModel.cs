using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class BoutModel
    {
        [Required(ErrorMessage = "This Field is Required")]
        public int FirstFencerId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public int SecondFencerId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Range(0, 5, ErrorMessage = "A fencing bout's score must be between 0 and 5.")]
        public int FirstFencerScore { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Range(0, 5, ErrorMessage = "A fencing bout's score must be between 0 and 5.")]
        public int SecondFencerScore { get; set; }

    }
}