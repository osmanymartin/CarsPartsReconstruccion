namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PieceMetadata
    {

        [Required()]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string pieceName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(250)]
        public string pieceDescription { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> piecePrice { get; set; }

    }

    [MetadataType(typeof(PieceMetadata))]
    public partial class Piece
    {
    }
}

