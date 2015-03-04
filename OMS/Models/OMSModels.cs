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

        #region Navigation Properties

        // Foreign Key, with Leaves
        public virtual Leaves Leaves { get; set; }

        // Foreign Key, with Leave Applications
        public virtual ICollection<LeaveApplications> LeaveApplications { get; set; }

        // Foreign Key, with Leave Approvals
        public virtual ICollection<LeaveApprovals> LeaveApprovals { get; set; }

        // Foreign Key, with Resource
        public virtual ICollection<Resource> Resource { get; set; }

        #endregion
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

        #region Employee ID (foreign key)

        [Required]
        [Column("EmployeeID")]
        [Display(Name = "Employee", GroupName = "Employee's Leaves Details")]
        [ForeignKey("Employee")]
        [Key]
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

    [Table("Leave Applications")]
    public class LeaveApplications : RecordDetails
    {
        // Setting Default Values using Constructor
        public LeaveApplications()
        {
            AppliedOn = DateTime.Today;
            Approved = false;
        }

        [Key]
        [Display(Name = "ID", GroupName = "Leave Applications Details")]
        public int ID { get; set; }

        #region Employee ID (foreign key)

        [Required]
        [Column("EmployeeID")]
        [Display(Name = "Employee", GroupName = "Leave Applications Details")]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        #endregion

        [Required]
        [Column("StartDate", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date", GroupName = "Leave Applications Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date", GroupName = "Leave Applications Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Column("Approved")]
        [Display(Name = "Is Approved?", GroupName = "Leave Applications Details")]
        public bool Approved { get; set; }

        [Column("AppliedOn", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Applied On", GroupName = "Leave Applications Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppliedOn { get; set; }

        #region Navigation Properties

        // Foreign Key, with Leave Approvals
        public virtual LeaveApprovals LeaveApprovals { get; set; }

        #endregion

    }

    [Table("Leave Approvals")]
    public class LeaveApprovals : RecordDetails
    {
        #region Application ID (foreign key)

        [Required]
        [Column("ApplicationID")]
        [Display(Name = "Application", GroupName = "Leave Approvals Details")]
        [ForeignKey("Application")]
        [Key]
        public int ApplicationID { get; set; }

        public LeaveApplications Application { get; set; }

        #endregion

        [Required]
        [Column("ApprovedOn", TypeName = "Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Approved On", GroupName = "Leave Approvals Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "Not Approved Yet.")]
        public DateTime ApprovedOn { get; set; }

        #region Approved By (foreign key)

        [Required]
        [Column("ApprovedBy")]
        [Display(Name = "Approved By", GroupName = "Leave Approvals Details")]
        [ForeignKey("Employee")]
        public int ApprovedBy { get; set; }

        public Employee Employee { get; set; }

        #endregion

    }

    [Table("Tasks")]
    public class Tasks : RecordDetails
    {
        // Setting Default Values using Constructor
        public Tasks()
        {
            OrderID = 0;
            ParentID = null;
        }

        [Key]
        [Display(Name = "ID", GroupName = "Task Details")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Order", GroupName = "Task Details")]
        [Column("OrderID")]
        public int OrderID { get; set; }

        #region Parent Id (foreign key)

        [Column("ParentID")]
        [Display(Name = "Parent Task", GroupName = "Task Details")]
        public int? ParentID { get; set; }

        public Tasks ParentTask { get; set; }

        #endregion

        [Required]
        [Column("StartDate", TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date", GroupName = "Task Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column("DueDate", TypeName = "DateTime2")]
        [Display(Name = "Due Date", GroupName = "Task Details")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Required]
        [Column("PercentComplete")]
        [Display(Name = "% Complete", GroupName = "Task Details")]
        public decimal PercentComplete { get; set; }

        [Column("SummaryTask")]
        [Display(Name = "Summary Task", GroupName = "Task Details")]
        public bool SummaryTask { get; set; }

        [Column("Description")]
        [Display(Name = "Display", GroupName = "Task Details")]
        [StringLength(300)]
        public string Description { get; set; }

        #region Navigation Properties

        // Foreign Key, with Tasks
        public virtual Tasks ParentTasks { get; set; }

        // Foreign Key, with Resource Assignment
        public virtual ICollection<ResourceAssignment> ResourceAssignment { get; set; }

        // Foreign Key, with Task Dependencies
        public virtual ICollection<TaskDependency> PredecessorTasks { get; set; }

        // Foreign Key, with Task Dependencies
        public virtual ICollection<TaskDependency> SuccessorTasks { get; set; }

        #endregion

    }

    [Table("Resources")]
    public class Resource : RecordDetails
    {
        [Key]
        [Display(Name = "ID", GroupName = "Resource Details")]
        public int ID { get; set; }

        #region Employee ID (foreign key)

        [Required]
        [Column("EmployeeID")]
        [Display(Name = "Employee", GroupName = "Resource Details")]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }

        #endregion

        [Required]
        [Column("Color")]
        [StringLength(6)]
        [Display(Name = "Color", GroupName = "Resource Details")]
        public string Color { get; set; }

        #region Navigation Properties

        // Foreign Key, with Resource Assignment
        public virtual ICollection<ResourceAssignment> ResourceAssignment { get; set; }

        #endregion

    }

    [Table("Resource Assignments")]
    public class ResourceAssignment : RecordDetails
    {
        [Key]
        [Display(Name = "ID", GroupName = "Resource Assignment Details")]
        public int ID { get; set; }

        #region Resource ID (foreign key)

        [Required]
        [Column("ResourceID")]
        [Display(Name = "Resource", GroupName = "Resouce Assignment Details")]
        [ForeignKey("Resource")]
        public int ResourceID { get; set; }

        public Resource Resource { get; set; }

        #endregion

        #region Task ID (foreign key)

        [Required]
        [Column("TaskID")]
        [Display(Name = "Task", GroupName = "Resouce Assignment Details")]
        [ForeignKey("Task")]
        public int TaskID { get; set; }

        public Tasks Task { get; set; }

        #endregion

    }

    [Table("Task Dependencies")]
    public class TaskDependency : RecordDetails
    {
        [Key]
        [Display(Name = "ID", GroupName = "Task Dependency Details")]
        public int ID { get; set; }

        #region Task ID (foreign key)

        [Required]
        [Column("PredecessorID")]
        [Display(Name = "Predecessor", GroupName = "Task Dependency Details")]
        [ForeignKey("Predecessor")]
        public int PredecessorID { get; set; }

        public Tasks Predecessor { get; set; }

        #endregion

        #region Task ID (foreign key)

        [Required]
        [Column("SuccessorID")]
        [Display(Name = "Successor", GroupName = "Task Dependency Details")]
        [ForeignKey("Successor")]
        public int SuccessorID { get; set; }

        public Tasks Successor { get; set; }

        #endregion
    }

}