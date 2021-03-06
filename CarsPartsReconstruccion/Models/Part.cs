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
    
    public partial class Part
    {
        public Part()
        {
            this.ServiceParts = new HashSet<ServicePart>();
            this.SupplierParts = new HashSet<SupplierPart>();
        }
    
        public int partId { get; set; }
        public string partName { get; set; }
        public string partDescription { get; set; }
        public Nullable<decimal> partPrice { get; set; }
    
        public virtual ICollection<ServicePart> ServiceParts { get; set; }
        public virtual ICollection<SupplierPart> SupplierParts { get; set; }
    }
}
