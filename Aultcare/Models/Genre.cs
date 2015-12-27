using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Aultcare.Models
{
    public class Genre
    {

        [Key]
        public int GenreID { get; set; }

        [Display(Name = "Genre Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "This is a required field.")]
        public string GenreName { get; set; }

        public virtual ICollection<GenreMovie> GenreMovies { get; set; }

    }
}
