using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Answer
    {
        public int UO_AnswerID { get; set; }

        public int UO_QuestionID { get; set; }//FK UO_Question

        public int UO_UserID { get; set; }//FK UO_User

        public int UO_CategoryID { get; set; }//FK UO_Category

        public string TopicAnswer { get; set; }

        public string DescriptionAnswer { get; set; }

        public virtual UO_Question UO_Question { get; set; }//connection with UO_Question

        public virtual ApplicationUser ApplicationUsers { get; set; }//connection with UO_User

    }
}