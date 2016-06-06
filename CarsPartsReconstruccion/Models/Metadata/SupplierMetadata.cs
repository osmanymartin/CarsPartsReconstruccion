namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SupplierMetadata
    {

        [Required()]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string supplierName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string supplierEmail { get; set; }

        [Display(Name = "Address")]
        [StringLength(250)]
        public string address { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string description { get; set; }

         [Display(Name = "Representative")]
        public Nullable<int> idRepresentative { get; set; }

    }

    [MetadataType(typeof(SupplierMetadata))]
    public partial class Supplier
    {
    }
}
