using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_User
    {
        public int UO_UserID { get; set; }//PK

        //public int PrivilegiosID { get; set; } //FK

        public string NameUser { get; set; }

        public string LastnameUser { get; set; }

        public string NicknameUser { get; set; }

        public string MailUser { get; set; }

        public string PasswordUser { get; set; }

        public int AnswerNumberUser { get; set; }

        public int QuestionNumberUser { get; set; }

        public virtual ICollection<UO_Question> UO_Questions { get; set; } //connection with UO_Question tbl

        public virtual ICollection<UO_Answer> UO_Answers { get; set; } //connection with UO_Answer tbl



    }
}