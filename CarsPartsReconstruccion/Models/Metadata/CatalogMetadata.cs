
namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class CatalogMetadata
    {
        [Required()]
        [Display(Name = "Catalog")]
        [StringLength(50)]
        public string catalogName { get; set; }

        [Required()]
        [Display(Name = "Value")]
        [StringLength(150)]
        public string catalogValue { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        public Nullable<int> parentCatalog { get; set; }

    }

    [MetadataType(typeof(CatalogMetadata))]
    public partial class Catalog
    {
    }
}
