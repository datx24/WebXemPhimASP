﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MovieDatabase_64130299Entities : DbContext
    {
        public MovieDatabase_64130299Entities()
            : base("name=MovieDatabase_64130299Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comment_64130299> Comment_64130299 { get; set; }
        public virtual DbSet<Favorite_64130299> Favorite_64130299 { get; set; }
        public virtual DbSet<Movie_64130299> Movie_64130299 { get; set; }
        public virtual DbSet<Rating_64130299> Rating_64130299 { get; set; }
        public virtual DbSet<User_64130299> User_64130299 { get; set; }
        public virtual DbSet<WatchHistory_64130299> WatchHistory_64130299 { get; set; }
    }
}
