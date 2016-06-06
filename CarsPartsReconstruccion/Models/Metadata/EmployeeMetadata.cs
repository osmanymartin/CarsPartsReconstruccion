namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EmployeeMetadata
    {

        [Required()]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string employeeName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string eMail { get; set; }

        [Display(Name = "Login")]
        [StringLength(20)]
        public string userLogin { get; set; }

        [Required(ErrorMessage = "You have to select a Position")]
        [Display(Name = "Position")]
        public int positionId { get; set; }

    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
    }
}
