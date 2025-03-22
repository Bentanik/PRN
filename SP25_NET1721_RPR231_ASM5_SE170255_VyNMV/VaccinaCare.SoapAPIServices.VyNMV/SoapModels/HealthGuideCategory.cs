using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace VaccinaCare.SoapAPIServices.VyNMV.SoapModels;

[DataContract]
public partial class HealthGuideCategory
{
    [Key]
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public bool IsActive { get; set; }
    [DataMember]
    public virtual ICollection<HealthGuide>? HealthGuides { get; set; } = new List<HealthGuide>();
}