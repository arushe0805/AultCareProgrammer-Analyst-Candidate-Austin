using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aultcare.Models
{
    public class Movie
    {

        [Key]
        public int MovieID { get; set; }

        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "This is a required field.")]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string MovieName { get; set; }

        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        [Required(ErrorMessage = "This is a required field.")]
        public string Director { get; set; }

        [Display(Name = "Released On")]
        [Required(ErrorMessage = "This is a required field.")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Released { get; set; }

        [Display(Name = "Length (Minutes)")]
        [Required(ErrorMessage = "This is a required field.")]
        public int Length { get; set; }

        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
        
        public virtual ICollection<GenreMovie> GenreMovies { get; set; }

    }

}