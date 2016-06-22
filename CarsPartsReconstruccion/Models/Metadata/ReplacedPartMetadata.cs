namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ReplacedPartMetadata
    {

        [Display(Name = "Supplier")]
        public Nullable<int> supplierId { get; set; }

        [Display(Name = "Part")]
        public Nullable<int> partId { get; set; }

        [Display(Name = "Reconstructed Part")]
        public Nullable<int> servicePartId { get; set; }

        [Required()]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal replacedPartPrice { get; set; }

        [Required()]
        [Display(Name = "Status")]
        public int statusId { get; set; }

        [StringLength(250)]
        [Display(Name = "Observations")]
        public string observations { get; set; }

    }

    [MetadataType(typeof(ReplacedPartMetadata))]
    public partial class ReplacedPart
    {
    }
}
