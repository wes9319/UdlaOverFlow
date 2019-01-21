using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Question
    {
        public int UO_QuestionID { get; set; }

        public int UO_UserID { get; set; }//FK UO_User

        public int UO_CategoryID { get; set; }//FK UO_Category

        public string TitleQuestion { get; set; }

        public string DescriptionQuestion { get; set; }

        public DateTime DateQuestion { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }//connection with UO_User table

        public virtual UO_Category UO_Category { get; set; }//connection with UO_Category table 

        public virtual ICollection<UO_Answer> UO_Answers { get; set; }//back connection with UO_Answers table

    }
}