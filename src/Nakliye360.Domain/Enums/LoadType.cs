namespace Nakliye360.Domain.Enums;

/// <summary>
/// Specifies the general category of a load.  This helps match vehicles with appropriate cargo.
/// </summary>
public enum LoadType
{
    Unknown = 0,
    Earthwork = 1, // Hafriyat
    Goods = 2,     // Eşya
    Food = 3       // Gıda
}
