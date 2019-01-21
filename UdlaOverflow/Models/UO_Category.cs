using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Category
    {
        public int UO_CategoryID { get; set; }

        public string DescriptionCategory { get; set; }

        public virtual ICollection<UO_Question> UO_Questions { get; set; } //connection with UO_Question tbl

        //public virtual ICollection<UO_Answer> UO_Answers { get; set; } //connection with UO_Answer tbl
    }
}