using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollageApp.Models
{
    public class StudentDTO
    {
        [ValidateNever] // Bỏ qua tính hợp lệ
        public int Id { get; set; }
        [Required] // Không được bỏ trống
        [StringLength(20)] // Độ dài tối đa là 20
        public string Name { get; set; }
        [Range(1, 100)] //Nằm trong khoảng từ 1-100
        public int Age { get; set; }
        [Required] //Không được bỏ trống
        public string Address { get; set; }
        [EmailAddress(ErrorMessage = "Please enter valid email")] // Phải hợp lệ Form Email
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))] // Phải đúng mật khẩu
        public string ConfirmPassword { get; set; }

    }
}
