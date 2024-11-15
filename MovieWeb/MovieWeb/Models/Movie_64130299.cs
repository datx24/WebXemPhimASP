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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Mã Phim")]
        public string MovieId { get; set; }
        [DisplayName("Tên phim")]
        [Required(ErrorMessage = "Tên phim không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên phim không được vượt quá 100 ký tự.")]
        public string Title { get; set; }
        [DisplayName("Mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string Description { get; set; }
        [DisplayName("Định dạng")]
        [Required(ErrorMessage = "Định dạng không được để trống.")]
        public Nullable<bool> GenreId { get; set; }
        [DisplayName("Thể loại")]
        [StringLength(50, ErrorMessage = "Tên thể loại không được vượt quá 50 ký tự.")]
        public string GenreName { get; set; }
        [DisplayName("Đạo diễn")]
        [StringLength(100, ErrorMessage = "Tên đạo diễn không được vượt quá 100 ký tự.")]
        public string DirectorName { get; set; }
        [DisplayName("Diễn viên")]
        [StringLength(200, ErrorMessage = "Tên diễn viên không được vượt quá 200 ký tự.")]
        public string ActorName { get; set; }
        [DisplayName("Quốc gia")]
        [StringLength(50, ErrorMessage = "Tên quốc gia không được vượt quá 50 ký tự.")]
        public string Country { get; set; }
        [DisplayName("Ngày phát hành")]
        [DataType(DataType.Date, ErrorMessage = "Ngày phát hành không hợp lệ.")]
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        [DisplayName("Poster URL")]
        [Url(ErrorMessage = "URL của Poster không hợp lệ.")]
        public string PosterUrl { get; set; }
        [DisplayName("Trailer URL")]
        [Url(ErrorMessage = "URL của Trailer không hợp lệ.")]
        public string TrailerUrl { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> CreatedAt { get; set; }
        [DisplayName("Ngày cập nhật")]
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    
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
