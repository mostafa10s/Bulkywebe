using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key] 
                
        public int Id { get; set; }
        [Required]
        public string? Titel { get; set; }
        public string? Description{ get; set; }
        [Required]
        public string? ISBN { get; set; }
        [Required]
        public string? Auther { get; set; }
        [Required]
        [Display(Name ="list price")]
        [Range(0,1000)]
        public double Listprice { get; set; }
        [Required]
        [Display(Name = "price for 1 to 50")]
        [Range(0, 1000)]
        public double price { get; set; }
        [Required]
        [Display(Name = "price for 50+")]
        [Range(0, 1000)]
        public double price50 { get; set; }
        [Required]
        [Display(Name = "price for 100+")]
        [Range(0, 1000)]
        public double price100 { get; set; }
   
        public int CategoryId { get; set;}
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

         public string? ImageUrl { get; set; }
    }
}




