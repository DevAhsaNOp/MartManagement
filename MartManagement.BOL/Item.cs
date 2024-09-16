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

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Stocks = new HashSet<Stock>();
        }

        [Display(Name = "Item Id")]
        public int Item_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "SubCategory Id")]
        public Nullable<int> SubCategory_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public string Item_Name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Buy Price")]
        public int Item_BuyPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Stock")]
        public int Item_Stock { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Total Price")]
        public decimal Item_TotalPrice { get; set; }
    
        public virtual SubCategory SubCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
