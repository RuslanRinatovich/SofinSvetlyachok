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
    
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            this.MaterialSuppliers = new HashSet<MaterialSupplier>();
            this.MaterialWriteOffs = new HashSet<MaterialWriteOff>();
            this.ProductMaterials = new HashSet<ProductMaterial>();
        }
    
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialTypeId { get; set; }
        public double CountInPack { get; set; }
        public int UnitTypeId { get; set; }
        public int MinimalCount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    
        public virtual MaterialType MaterialType { get; set; }
        public virtual UnitType UnitType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialSupplier> MaterialSuppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialWriteOff> MaterialWriteOffs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
