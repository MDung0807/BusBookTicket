namespace BusBookTicket.Core.Models.Entity;

public class Province : BaseEntity
{
    #region -- Properties --

    public string FullName { get; set; }
    public string FullNameEnglish { get; set; }
    public string CodeName { get; set; }
    public string Name { get; set; }
    public string NameEnglish { get; set; }

    #endregion -- Properties --

    #region -- Relationship --
    public AdministrativeRegion AdministrativeRegion { get; set; }
    public AdministrativeUnit AdministrativeUnit { get; set; }
    public HashSet<District> Districts { get; set; }

    #endregion -- Relationship --
}