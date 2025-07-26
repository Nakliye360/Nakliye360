namespace Nakliye360.Domain.Enums;

/// <summary>
/// Categorises vehicles by their general purpose or construction.  This is used when matching loads to vehicles.
/// </summary>
public enum VehicleType
{
    /// <summary>
    /// The type is unknown or not specified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// A standard flatbed or rigid truck suitable for general goods.
    /// </summary>
    Truck = 1,

    /// <summary>
    /// A dump truck used primarily for earthwork (hafriyat) and construction materials.
    /// </summary>
    DumpTruck = 2,

    /// <summary>
    /// A panel van or light commercial vehicle.
    /// </summary>
    Van = 3,

    /// <summary>
    /// A refrigerated vehicle suitable for transporting perishable goods and food.
    /// </summary>
    RefrigeratedTruck = 4,

    /// <summary>
    /// A pickup truck, typically open-backed and suited for small loads.
    /// </summary>
    Pickup = 5,

    /// <summary>
    /// A vehicle designed to tow a trailer or semi-trailer, often used for long haul freight.
    /// </summary>
    SemiTrailer = 6,

    /// <summary>
    /// Other or specialised vehicle types not covered by the above categories.
    /// </summary>
    Other = 99
}
