//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppOrbit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialSupplier
    {
        public int MaterialSupplierId { get; set; }
        public int MaterialId { get; set; }
        public int SupplierId { get; set; }
        public int Quality { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int Count { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
