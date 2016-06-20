namespace CarsPartsReconstruccion.Models
{
    using CarsPartsReconstruccion.ValidationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PartMetadata
    {

        [Required()]
        [Display(Name = "Part")]
        [StringLength(50)]
        public string partName { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string partDescription { get; set; }

        [Display(Name = "Ref Price")]
        [DataType(DataType.Currency)]
        ////[ValidateStockPriceAttribute(100)]
        public Nullable<decimal> partPrice { get; set; }

    }

    [MetadataType(typeof(PartMetadata))]
    public partial class Part
    {
        [Display(Name = "AverageSuppliers")]
        [DataType(DataType.Currency)]
        public decimal? AverageSuppliersPrice { get; set; }
    }
}
