using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UdlaOverflow.Models
{
    public class UO_Question
    {
        public int UO_QuestionID { get; set; }

        public string UO_UserID { get; set; }//FK UO_User

        [Display(Name = "Categoría")]
        public int UO_CategoryID { get; set; }//FK UO_Category
        [Required]
        [Display(Name = "Título de la Pregunta")]
        public string TitleQuestion { get; set; }
        [Required]
        [Display(Name = "Descripción de la Pregunta")]
        public string DescriptionQuestion { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Publicación")]
        public DateTime DateQuestion { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }//connection with UO_User table

        public virtual UO_Category UO_Category { get; set; }//connection with UO_Category table 

        public virtual ICollection<UO_Answer> UO_Answers { get; set; }//back connection with UO_Answers table

    }
}