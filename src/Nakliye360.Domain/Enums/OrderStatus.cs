namespace Nakliye360.Domain.Enums
{
    /// <summary>
    /// Enumerates the possible states of an order.
    /// </summary>
    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        InTransit = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
