namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ReplacedPieceMetadata
    {

        [Required()]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal replacedPiecePrice { get; set; }

        [Required()]
        [Display(Name = "Status")]
        public int statusId { get; set; }

        [StringLength(250)]
        [Display(Name = "Observations")]
        public string observations { get; set; }

    }

    [MetadataType(typeof(ReplacedPieceMetadata))]
    public partial class ReplacedPiece
    {
    }
}
