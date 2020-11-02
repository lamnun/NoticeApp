using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoticeApp.Modells
{
    [Table("Notices")]
    public class Notice
    {
        //Serial Number
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //외래키
        public int? ParentId { get; set; }

        //이름
        [Required(ErrorMessage ="이름을 입력하세요.")]
        [MaxLength(255)]
        public string Name { get; set; }

        //제목
        public string Title { get; set; }

        //내용
        public string Content { get; set; }

        //상단고정
        public bool? IsPinned { get; set; } = false;

        //등록자
        public string CreatedBy { get; set; }

        //등록일 
        public DateTime? Created { get; set; }

        //수정자
        public string ModifiedBy { get; set; }

        //수정일
        public DateTime? Modified { get; set; }








    }
}
