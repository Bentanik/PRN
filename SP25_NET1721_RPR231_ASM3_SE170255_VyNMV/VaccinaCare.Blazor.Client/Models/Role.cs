namespace VaccinaCare.Blazor.Client.Models;

public partial class Role
{
    public string RoleName { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public int Id { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}