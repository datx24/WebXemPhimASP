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

    public partial class AdminUsers_64130299
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DisplayName("Mật khẩu")]
        public string PasswordHash { get; set; }
    }
}
