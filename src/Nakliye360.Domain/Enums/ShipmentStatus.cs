namespace Nakliye360.Domain.Enums;

/// <summary>
/// Represents the various states that a shipment can be in during its lifecycle.
/// </summary>
public enum ShipmentStatus
{
    Pending = 0,
    InTransit = 1,
    Delivered = 2,
    Cancelled = 3
}