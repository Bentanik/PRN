using System.ComponentModel.DataAnnotations;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

public partial class SystemAccount
{
    [Key]
    public int AccountId { get; set; }

    public string AccountPassword { get; set; }

    public string EmailAddress { get; set; }

    public string AccountNote { get; set; }

    public int? Role { get; set; }
}