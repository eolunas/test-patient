namespace COLAPP.Domain.Entities;

public class Patient
{
    public long Id { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; } 
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}