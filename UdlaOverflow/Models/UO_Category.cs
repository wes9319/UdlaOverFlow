using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Category
    {
        public int UO_CategoryID { get; set; }

        [Display(Name = "Categoría")]
        public string DescriptionCategory { get; set; }

        public virtual ICollection<UO_Question> UO_Questions { get; set; } //connection with UO_Question tbl

    }
}