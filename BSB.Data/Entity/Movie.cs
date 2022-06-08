using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSB.Data.Entity
{
    public class Movie : Base
    {
        [Required]
        [Display(Name ="Movie Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Movie Image")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Movie Genre")]
        public string Genre { get; set; }
        [Required]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Movie Director")]
        public string Director { get; set; }
        public virtual ICollection<UserFavMovie> FavOnUsers { get; set; }
    }
}
