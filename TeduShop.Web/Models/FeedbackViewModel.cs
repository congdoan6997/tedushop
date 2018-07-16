using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [StringLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        [Required(ErrorMessage ="Tên không được bỏ trống")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Tin nhắn không được quá 500 ký tự")]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Phải nhập trạng thái")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}