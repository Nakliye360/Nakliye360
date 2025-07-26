namespace Nakliye360.Domain.Enums;

/// <summary>
/// Represents the direction of a load request. Loads can be either outbound (gidiş) or return (dönüş).
/// </summary>
public enum LoadDirection
{
    /// <summary>
    /// Outbound loads represent shipments moving from the origin to the destination.
    /// </summary>
    Outbound = 0,

    /// <summary>
    /// Return loads represent shipments that take place on the return journey of a vehicle after completing the primary task.
    /// </summary>
    Return = 1
}
