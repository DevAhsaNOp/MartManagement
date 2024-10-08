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

    public partial class Stock
    {
        [Display(Name = "Stock Id")]
        public int Stock_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Id")]
        public Nullable<int> Item_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock Quantity")]
        public int Stock_Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock Retail Price")]
        public int Stock_RetailPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stock Total Price")]
        public decimal Stock_TotalPrice { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
