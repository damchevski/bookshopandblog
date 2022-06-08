using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Dto
{
    public class AddToFavouritesDto
    {
        public Movie SelectedMovie { get; set; }

        public Guid MovieId { get; set; }

    }
}
