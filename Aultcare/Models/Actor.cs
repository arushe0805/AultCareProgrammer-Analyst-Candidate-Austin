using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aultcare.Models
{
    public class Actor
    {

        [Key]
        public int ActorID { get; set; }

        [Display(Name = "Actor Name")]
        [Required(ErrorMessage ="This is a required field.")]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string ActorName { get; set; }

        public virtual ICollection<ActorMovie> ActorMovies { get; set; }

    }

}
