namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SupplierPieceMetadata
    {

        [Required()]
        [Display(Name = "Supplier")]
        public int supplierId { get; set; }

        [Required()]
        [Display(Name = "Piece")]
        public int pieceId { get; set; }

        [Required()]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

        [Required()]
        [Display(Name = "Existence")]
        [Range(0, int.MaxValue, ErrorMessage = "Existence must be a positive number")]
        public int existence { get; set; }
    }

    [MetadataType(typeof(SupplierPieceMetadata))]
    public partial class SupplierPiece
    {
        [Display(Name = "AverageSuppliers")]
        [DataType(DataType.Currency)]
        public decimal? AverageSuppliersPrice { get; set; }
    }
}
