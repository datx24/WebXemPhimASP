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
    
    public partial class Genre_64130299
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre_64130299()
        {
            this.Movie_64130299 = new HashSet<Movie_64130299>();
        }
    
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movie_64130299> Movie_64130299 { get; set; }
    }
}
