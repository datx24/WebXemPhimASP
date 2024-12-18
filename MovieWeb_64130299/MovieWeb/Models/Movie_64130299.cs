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
    
    public partial class Movie_64130299
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movie_64130299()
        {
            this.Comment_64130299 = new HashSet<Comment_64130299>();
            this.Favorite_64130299 = new HashSet<Favorite_64130299>();
            this.MovieUrls_64130299 = new HashSet<MovieUrls_64130299>();
            this.Rating_64130299 = new HashSet<Rating_64130299>();
            this.WatchHistory_64130299 = new HashSet<WatchHistory_64130299>();
        }
    
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<bool> GenreId { get; set; }
        public string GenreName { get; set; }
        public string DirectorName { get; set; }
        public string ActorName { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string AccessLevel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment_64130299> Comment_64130299 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite_64130299> Favorite_64130299 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovieUrls_64130299> MovieUrls_64130299 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating_64130299> Rating_64130299 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WatchHistory_64130299> WatchHistory_64130299 { get; set; }
    }
}
