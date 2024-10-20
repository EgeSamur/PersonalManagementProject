using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities;

public class Employee : BaseEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public int DepartmentId { get; set; }

    // Şifre ve Salt ekliyoruz
    public byte[] PasswordHash { get; set; } // Şifre hash'lenmiş olarak saklanır
    public byte[] PasswordSalt { get; set; } // Şifre için salt değer

    // Navigation property
    public ICollection<EmployeeRole> EmployeeRoles { get; set; }
    public ICollection<EmployeePermission> EmployeePermissions { get; set; }
    public Department Department { get; set; }
    public ICollection<Leave> Leaves { get; set; }
    public ICollection<SalaryPayment> SalaryPayments { get; set; }
    public ICollection<PerformanceReview> PerformanceReviews { get; set; }
}



