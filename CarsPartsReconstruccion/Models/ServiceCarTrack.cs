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
    
    public partial class ServiceCarTrack
    {
        public int serviceCarTrackId { get; set; }
        public System.DateTime date { get; set; }
        public int serviceCarId { get; set; }
        public int statusId { get; set; }
        public string observations { get; set; }
    
        public virtual ServiceCar ServiceCar { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}
