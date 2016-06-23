namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    public partial class ServiceCarMetadata
    {
        int presentYear = Convert.ToInt32(ConfigurationManager.AppSettings["presentYear"]);

        [Required()]
        [Display(Name = "Service")]
        public int serviceId { get; set; }

        [Required()]
        [Display(Name = "Brand")]
        public int carBrandId { get; set; }

        [Required()]
        [Display(Name = "Year")]
        [Range(1900, 2016, ErrorMessage = "This Year is not valid.")]
        public int year { get; set; }

        [Required()]
        [Display(Name = "Model")]
        public int carModelId { get; set; }

        [Display(Name = "EstimatedPrice")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> estimatedPrice { get; set; }

        [Display(Name = "RealPrice")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> realPrice { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string serviceCarDescription { get; set; }

        [Required()]
        [Display(Name = "Status")]
        public int statusId { get; set; }

        [Display(Name = "Observations")]
        [StringLength(250)]
        public string observations { get; set; }
    }

    [MetadataType(typeof(ServiceCarMetadata))]
    public partial class ServiceCar
    {

    }
}
