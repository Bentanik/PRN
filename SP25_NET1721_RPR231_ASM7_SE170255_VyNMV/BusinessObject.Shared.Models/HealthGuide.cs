namespace BusinessObject.Shared.Models;

public partial class HealthGuide
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public int? HealthGuideCategorieId { get; set; }

    public string Author { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public int Views { get; set; }

    public string ImageUrl { get; set; }

    public virtual HealthGuideCategory? HealthGuideCategorie { get; set; }
}