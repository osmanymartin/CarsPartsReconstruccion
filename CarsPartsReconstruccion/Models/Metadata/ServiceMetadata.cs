
namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class ServiceMetadata
    {

        [Required()]
        [Display(Name = "Customer")]
        public int customerId { get; set; }

        [Required()]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime serviceDate { get; set; }

        [Display(Name = "Employee")]
        public Nullable<int> employeeId { get; set; }

        [Display(Name = "EstimatedPrice")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> estimatedPrice { get; set; }

        [Display(Name = "RealPrice")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> realPrice { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string serviceDescription { get; set; }

        [Required()]
        [Display(Name = "Status")]
        public int statusId { get; set; }

        [Display(Name = "Status Observations")]
        [StringLength(250)]
        public string statusObservations { get; set; }
    }

    [MetadataType(typeof(ServiceMetadata))]
    public partial class Service
    {
    }
}
