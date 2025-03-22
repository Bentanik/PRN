using System.ComponentModel.DataAnnotations;

namespace PE_PRN231_SP25_TrialTest_NguyenMaiVietVy_FE.Models;

public partial class CosmeticInformation
{
    [Key]
    [Required(ErrorMessage = "CosmeticId is required.")]
    public string CosmeticId { get; set; }

    [Required(ErrorMessage = "CosmeticName is required.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "CosmeticName must be between 2 and 80 characters.")]
    [RegularExpression(@"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$", ErrorMessage = "Each word of CosmeticName must start with a capital letter and only contain a-z, A-Z, space, @, #, and 0-9.")]
    public string CosmeticName { get; set; }

    [Required(ErrorMessage = "SkinType is required.")]
    public string SkinType { get; set; }

    [Required(ErrorMessage = "ExpirationDate is required.")]
    public string ExpirationDate { get; set; }

    [Required(ErrorMessage = "CosmeticSize is required.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "CosmeticSize must be between 2 and 80 characters.")]
    public string CosmeticSize { get; set; }

    [Required(ErrorMessage = "DollarPrice is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "DollarPrice must be greater than 0.")]
    public decimal DollarPrice { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public string CategoryId { get; set; }

    public virtual CosmeticCategory? Category { get; set; }
}