using System.ComponentModel.DataAnnotations;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

public partial class CosmeticCategory
{
    [Key]
    public string CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string UsagePurpose { get; set; }

    public string FormulationType { get; set; }

    public virtual ICollection<CosmeticInformation> CosmeticInformations { get; set; } = new List<CosmeticInformation>();
}