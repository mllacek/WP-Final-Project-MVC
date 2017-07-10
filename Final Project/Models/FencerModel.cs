using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class FencerModel
    {
        [Required(ErrorMessage = "This Field is Required")]
        public int ID { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }

        public int Victories { get; set; }

        public int TouchesScored { get; set; }

        public int TouchesReceived { get; set; }

        public int Indicator { get; set; }

        public int Placement { get; set; }
    }
}