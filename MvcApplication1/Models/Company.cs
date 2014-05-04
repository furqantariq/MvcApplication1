//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.ComponentModel.DataAnnotations;
namespace MvcApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company
    {
        public Company()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
           [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
           [Required(ErrorMessage = "Enter Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
           [Required(ErrorMessage = "Enter Description ")]
        public string Description { get; set; }
           [Required(ErrorMessage = "Enter Category")]
        public string Category { get; set; }
           [Required(ErrorMessage = "Enter Email")]
           [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
           [Required(ErrorMessage = "Etner Country")]
        public string Country { get; set; }
           [Required(ErrorMessage = "Etner Address")]
        public string Address { get; set; }
        public Nullable<int> Phone { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}