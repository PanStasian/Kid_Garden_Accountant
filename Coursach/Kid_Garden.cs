//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Coursach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kid_Garden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kid_Garden()
        {
            this.Kids__Personal_Account = new HashSet<Kids__Personal_Account>();
        }
    
        public int Kid_Garden_Number { get; set; }
        public string Kid_Garden_Name { get; set; }
        public double Pay_Sum { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kids__Personal_Account> Kids__Personal_Account { get; set; }
    }
}