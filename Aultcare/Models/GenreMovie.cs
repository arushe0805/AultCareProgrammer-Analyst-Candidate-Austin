using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aultcare.Models
{
    public class GenreMovie
    {

        [Key]
        public int GenreMovieID { get; set; }

        [ForeignKey("Movie")]
        [Required(ErrorMessage = "This is a required field.")]
        public int MovieID { get; set; }

        [ForeignKey("Genre")]
        [Required(ErrorMessage = "This is a required field.")]
        public int GenreID { get; set; }

        [Display(Name = "Genre Rating For Movie")]
        [Range(typeof(int), "0", "5", ErrorMessage = "Can only be between 0 and 5")]
        [Required(ErrorMessage = "This is a required field.")]
        [DefaultValue(3)]
        public int GenreRatingForMovie { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Genre Genre { get; set; }

    }
}
