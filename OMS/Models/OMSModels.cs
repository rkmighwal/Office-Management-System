using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Models
{
    public class RecordDetails
    {
        // Setting Default Values using Constructor
        public RecordDetails()
        {
            ModifiedOn = DateTime.Now;
        }

        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created", GroupName = "Record Details")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Last Modified On", GroupName = "Record Details")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        public DateTime ModifiedOn { get; set; }
    }

    [Table("Employee")]
    public class Employee : RecordDetails
    {
        [Key]
        [Display(Name = "ID", GroupName = "Employee Details")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee ID", GroupName = "Employee Details")]
        [Column("EmployeeID")]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string EmployeeID { get; set; }

        [Display(Name = "Profile Picture", GroupName = "Employee Details")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Designation", GroupName = "Employee Details")]
        [Column("Designation")]
        [StringLength(20)]
        public string Designation { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salary", GroupName = "Employee Details")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Salary { get; set; }
    }

    [Table("Leaves")]
    public class Leaves : RecordDetails
    {
        // Setting Default Values using Constructor
        public Leaves()
        {
            Balance = 0;
            Applied = 0;
            MonthlyLimit = 1;
        }

        [Key]
        [Display(Name = "ID", GroupName = "Employee's Leaves Details")]
        public int ID { get; set; }

        #region Employee ID (foreign key)

        [Required]
        [Column("EmployeeID")]
        [Display(Name = "Employee", GroupName = "Employee's Leaves Details")]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        #endregion

        [Required]
        [Display(Name = "Leave Balance", GroupName = "Employee's Leaves Details")]
        [Column("Balance", TypeName = "TinyInt")]
        public byte Balance { get; set; }

        [Required]
        [Display(Name = "Leave Applied", GroupName = "Employee's Leaves Details")]
        [Column("Applied", TypeName = "TinyInt")]
        public byte Applied { get; set; }

        [Required]
        [Display(Name = "Leaves Limit (Monthly)", GroupName = "Employee's Leaves Details")]
        [Column("MonthlyLimit", TypeName = "TinyInt")]
        public byte MonthlyLimit { get; set; }

        [Column("LastAppliedOn", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Last Applied On", GroupName = "Employee's Leaves Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "No Leave Applied.")]
        public DateTime LastApplied { get; set; }

        [Column("LastApprovedOn", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Last Approved On", GroupName = "Employee's Leaves Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "No Leave Applied.")]
        public DateTime LastApproved { get; set; }

    }

}