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
    
    public partial class Rating_64130299
    {
        public int RatingId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MovieId { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
    
        public virtual Movie_64130299 Movie_64130299 { get; set; }
        public virtual User_64130299 User_64130299 { get; set; }
    }
}
