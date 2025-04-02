namespace VaccinaCare.GraphQLClient.Models;

public partial class HealthGuideCategory
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<HealthGuide> HealthGuides { get; set; } = new List<HealthGuide>();
}