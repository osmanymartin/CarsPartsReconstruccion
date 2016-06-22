namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    public partial class ServicePartMetadata
    {

        [Required()]
        [Display(Name = "Service")]
        public int serviceId { get; set; }

        [Required()]
        [Display(Name = "Part")]
        public int partId { get; set; }

        [Display(Name = "Estimated Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> estimatedPrice { get; set; }

        [Display(Name = "Real Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> realPrice { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string servicePartDescription { get; set; }

        [Required()]
        [Display(Name = "Status")]
        public int statusId { get; set; }

        [Display(Name = "Observations")]
        [StringLength(250)]
        public string observations { get; set; }

    }

    [MetadataType(typeof(ServicePartMetadata))]
    public partial class ServicePart
    {

    }
}
