using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BOL.ValidationClasses
{
    public class CategoryValidation
    {
        [Required(ErrorMessage ="Category Name is Required*")]
        public string Category_Name { get; set; }
        
        [Required(ErrorMessage ="Category Description is Required*")]
        public string Category_Description { get; set; }
    }

    //[MetadataType(typeof(CategoryValidation))]
    //public partial class Category
    //{

    //}
}
