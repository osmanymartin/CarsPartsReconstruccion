//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarsPartsReconstruccion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServicePart
    {
        public ServicePart()
        {
            this.ReplacedPieces = new HashSet<ReplacedPiece>();
            this.ServicePartMiscellaneousCosts = new HashSet<ServicePartMiscellaneousCost>();
            this.ServicePartTracks = new HashSet<ServicePartTrack>();
            this.ReplacedParts = new HashSet<ReplacedPart>();
        }
    
        public int servicePartId { get; set; }
        public int serviceId { get; set; }
        public int partId { get; set; }
        public Nullable<decimal> estimatedPrice { get; set; }
        public Nullable<decimal> realPrice { get; set; }
        public string servicePartDescription { get; set; }
        public int statusId { get; set; }
        public string observations { get; set; }
    
        public virtual ICollection<ReplacedPiece> ReplacedPieces { get; set; }
        public virtual ICollection<ServicePartMiscellaneousCost> ServicePartMiscellaneousCosts { get; set; }
        public virtual ICollection<ServicePartTrack> ServicePartTracks { get; set; }
        public virtual Part Part { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<ReplacedPart> ReplacedParts { get; set; }
    }
}
