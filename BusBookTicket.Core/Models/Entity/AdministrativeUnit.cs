namespace BusBookTicket.Core.Models.Entity;

public class AdministrativeUnit : BaseEntity
{
    #region -- Properties --

    public string FullName { get; set; }
    public string FullNameEnglish { get; set; }
    public string CodeName { get; set; }
    public string CodeNameEnglish { get; set; }
    public string ShortName { get; set; }
    public string ShortNameEnglish { get; set; }

    #endregion -- Properties --

    #region -- Relationship --
    public HashSet<Province> Provinces { get; set; }
    public HashSet<District> Districts { get; set; }
    public HashSet<Ward> Wards { get; set; }
    #endregion -- Relationship --
}