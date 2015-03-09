using System.ComponentModel.DataAnnotations;

namespace OMS.Models
{

    public enum Roles
    {
        Employee = 1,
        Manager = 2,
        Accountant = 3
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class UserViewModel
    {
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID", GroupName = "User Details")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "User Name", GroupName = "User Details")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", GroupName = "User Details")]
        public string Email { get; set; }

        [Display(Name = "Email Confirmed", GroupName = "User Details")]
        public bool EmailConfirmed { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number", GroupName = "User Details")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", GroupName = "User Details")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Retype Password", GroupName = "User Details")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Lockout Enabled", GroupName = "User Details")]
        public bool LockoutEnabled { get; set; }

        [Required]
        [Display(Name = "Role", GroupName = "User Details")]
        public Roles Role { get; set; }

        [Required]
        [Display(Name = "Name", GroupName = "User Details")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Surname", GroupName = "User Details")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Gender", GroupName = "User Details")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Street Address", GroupName = "User Details")]
        [StringLength(200)]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "State/Province", GroupName = "User Details")]
        [StringLength(50)]
        public string StateOrProvince { get; set; }

        [Required]
        [Display(Name = "Postal Code", GroupName = "User Details")]
        [StringLength(10)]
        public string PostalCode { get; set; }
    }
}