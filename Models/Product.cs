using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProje.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Product Name is Required")]
        public string ProductName { get; set; }
        [Required]
        public float ProductPrice { get; set; }
        [Required]
        public int ProductStock { get; set; }
        [Required]
        public string ProductDetails { get; set; }

        //internal static object OrderBy(Func<object, object> p)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
