namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SupplierPartMetadata
    {

        [Required()]
        [Display(Name = "Supplier")]
        public int supplierId { get; set; }

        [Required()]
        [Display(Name = "Part")]
        public int partId { get; set; }

        [Required()]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [Required()]
        [Display(Name = "Existence")]
        [Range(0, int.MaxValue, ErrorMessage = "Existence must be a positive number")]
        public int existence { get; set; }
    }

    [MetadataType(typeof(SupplierPartMetadata))]
    public partial class SupplierPart
    {
    }
}
