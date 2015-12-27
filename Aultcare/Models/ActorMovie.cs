using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aultcare.Models
{

    public class ActorMovie
    {
        [Key]
        public int ActorMovieID { get; set; }

        [ForeignKey("Movie")]
        [Required(ErrorMessage = "This is a required field.")]
        public int MovieID { get; set; }

        [ForeignKey("Actor")]
        [Required(ErrorMessage = "This is a required field.")]
        public int ActorID { get; set; }

        [Display(Name = "Character Name")]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        [Required(ErrorMessage = "This is a required field.")]
        public string CharacterName { get; set; }

        [Display(Name = "Character Type")]
        [Required(ErrorMessage = "This is a required field.")]
        [StringLength(10, ErrorMessage = "First name cannot be longer than 10 characters.")]
        public string CharacterType { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Actor Actor { get; set; }

    }

}
