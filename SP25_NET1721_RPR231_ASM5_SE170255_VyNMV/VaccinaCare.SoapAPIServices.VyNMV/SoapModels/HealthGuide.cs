using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace VaccinaCare.SoapAPIServices.VyNMV.SoapModels;

[DataContract]
public partial class HealthGuide
{
    [Key]
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Title { get; set; }
    [DataMember]
    public string Content { get; set; }
    [DataMember]
    public int? HealthGuideCategorieId { get; set; }
    [DataMember]
    public string Author { get; set; }
    [DataMember]
    public DateTime CreatedAt { get; set; }
    [DataMember]
    public DateTime? UpdatedAt { get; set; }
    [DataMember]
    public bool IsActive { get; set; }
    [DataMember]
    public int Views { get; set; }
    [DataMember]
    public string ImageUrl { get; set; }
    [DataMember]
    public virtual HealthGuideCategory HealthGuideCategorie { get; set; }
}