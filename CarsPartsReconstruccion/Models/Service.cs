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
    
    public partial class Service
    {
        public Service()
        {
            this.ServiceMiscellaneousCosts = new HashSet<ServiceMiscellaneousCost>();
            this.ServiceParts = new HashSet<ServicePart>();
            this.ServiceTracks = new HashSet<ServiceTrack>();
            this.ServiceCars = new HashSet<ServiceCar>();
        }
    
        public int serviceId { get; set; }
        public int customerId { get; set; }
        public System.DateTime serviceDate { get; set; }
        public Nullable<int> employeeId { get; set; }
        public Nullable<decimal> estimatedPrice { get; set; }
        public Nullable<decimal> realPrice { get; set; }
        public string serviceDescription { get; set; }
        public int statusId { get; set; }
        public string statusObservations { get; set; }
    
        public virtual Catalog Catalog { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ServiceMiscellaneousCost> ServiceMiscellaneousCosts { get; set; }
        public virtual ICollection<ServicePart> ServiceParts { get; set; }
        public virtual ICollection<ServiceTrack> ServiceTracks { get; set; }
        public virtual ICollection<ServiceCar> ServiceCars { get; set; }
    }
}
