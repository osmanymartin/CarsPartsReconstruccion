namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CustomerMetadata
    {

        [Required()]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string customerName { get; set; }

        [Required()]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string eMail { get; set; }

        [Display(Name = "Login")]
        [StringLength(20)]
        public string userLogin { get; set; }

        [Display(Name = "Address")]
        [StringLength(250)]
        public string address { get; set; }

    }

    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }
}