//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MartManagement.BOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SubCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCategory()
        {
            this.Items = new HashSet<Item>();
        }

        public int SubCategory_Id { get; set; }

        public Nullable<int> Category_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "SubCategory Name")]
        public string SubCategory_Name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "SubCategory Description")]
        public string SubCategory_Description { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}