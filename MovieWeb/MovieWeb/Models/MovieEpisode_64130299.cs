//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovieEpisode_64130299
    {
        public int EpisodeId { get; set; }
        public Nullable<int> MovieId { get; set; }
        public int EpisodeNumber { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
    
        public virtual Movie_64130299 Movie_64130299 { get; set; }
    }
}
