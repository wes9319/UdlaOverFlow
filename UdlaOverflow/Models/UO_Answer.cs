using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Answer
    {
        public int UO_AnswerID { get; set; }

        public int UO_QuestionID { get; set; }//FK UO_Question

        public string UO_UserID { get; set; }//FK UO_User

        public int UO_CategoryID { get; set; }//FK UO_Category

        [Display(Name = "Tema")]
        public string TopicAnswer { get; set; }

        [Display(Name = "Respuesta")]
        public string DescriptionAnswer { get; set; }

        public virtual UO_Question UO_Question { get; set; }//connection with UO_Question

        public virtual ApplicationUser ApplicationUsers { get; set; }//connection with UO_User

    }
}