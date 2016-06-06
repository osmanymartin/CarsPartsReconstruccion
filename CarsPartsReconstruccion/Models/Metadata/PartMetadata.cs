namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PartMetadata
    {
        
        [Required()]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string partName { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string partDescription { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> partPrice { get; set; }

    }

    [MetadataType(typeof(PartMetadata))]
    public partial class Part
    {
    }
}
