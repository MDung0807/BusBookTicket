namespace BusBookTicket.Core.Models.Entity;

public class AdministrativeRegion : BaseEntity
{
    #region -- Properties --

    public string Name { get; set; }
    public string NameEnglish { get; set; }
    public string CodeName { get; set; }
    public string CodeNameEnglish { get; set; }

    #endregion -- Properties --

    #region -- Relationship --

    public HashSet<Province> Provinces { get; set; }

    #endregion -- Relationship --
}