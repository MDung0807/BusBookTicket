namespace BusBookTicket.Core.Models.Entity;

public class District : BaseEntity
{
    #region -- Properties --

    public string FullName { get; set; }
    public string FullNameEnglish { get; set; }
    public string CodeName { get; set; }
    public string Name { get; set; }
    public string NameEnglish { get; set; }

    #endregion -- Properties --

    #region -- RelationShip --
    public HashSet<Ward> Wards { get; set; }
    public AdministrativeUnit AdministrativeUnit { get; set; }
    public Province Province { get; set; }
    #endregion -- RelationShip --
}