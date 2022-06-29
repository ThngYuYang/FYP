using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace L11.Models
{
    public class Warehouse
    {
        [Required(ErrorMessage = "Please enter an Id")]
        [StringLength(800, MinimumLength = 100, ErrorMessage = "Id must be 100-800")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Alias cannot be null")]
        [StringLength(29, MinimumLength = 1, ErrorMessage = "Maximum 29 Characters")]
        [Remote(action: "CheckAlias", controller: "Warehouse")]
        public string Alias { get; set; }
        [RegularExpression("[(02|03|07)] /d{4} /d{4} ", ErrorMessage = "Incorrect phone format (0N NNNN NNNN)")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address must be entered")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Maximum 50 characters")]
        public string AddressLine1 { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Maximum 50 characters")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter the name of a city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter the abbreviation for a state")]
        [RegularExpression("(QLD|NSW|VIC) ", ErrorMessage = "Only QLD, NSW and VIC")]
        public string State { get; set; }
        [Required(ErrorMessage = "Postcode is required")]
        [Range(2000, 4980, ErrorMessage = "Postcode must be 2000-4980")]
        public int Postcode { get; set; }
    }
}
