using System.Runtime.Serialization;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.SoapAPIServices.VyNMV.SoapModels;

[DataContract]
public partial class User
{
    [DataMember]
    public Guid Id { get; set; }
    [DataMember]
    public string FullName { get; set; }
    [DataMember]
    public string Email { get; set; }
    [DataMember]
    public bool? Gender { get; set; }
    [DataMember]
    public DateTime? DateOfBirth { get; set; }
    [DataMember]
    public string ImageUrl { get; set; }
    [DataMember]
    public string PhoneNumber { get; set; }
    [DataMember]
    public string PasswordHash { get; set; }
    [DataMember]
    public string RoleName { get; set; }
    [DataMember]
    public string Address { get; set; }
    [DataMember]
    public string RefreshToken { get; set; }
    [DataMember]
    public DateTime? RefreshTokenExpiryTime { get; set; }
    [DataMember]
    public bool IsDeleted { get; set; }
    [DataMember]
    public DateTime CreatedAt { get; set; }
    [DataMember]
    public Guid? CreatedBy { get; set; }
    [DataMember]
    public DateTime? UpdatedAt { get; set; }
    [DataMember]
    public Guid? UpdatedBy { get; set; }
    [DataMember]
    public DateTime? DeletedAt { get; set; }
    [DataMember]
    public Guid? DeletedBy { get; set; }
    [DataMember]
    public int? RoleId { get; set; }
    [DataMember]
    public virtual Role Role { get; set; }
}