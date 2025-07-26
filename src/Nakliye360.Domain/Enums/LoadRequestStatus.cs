namespace Nakliye360.Domain.Enums;

/// <summary>
/// Represents the current state of a load request posted on the platform.
/// </summary>
public enum LoadRequestStatus
{
    /// <summary>
    /// The load request is open and awaiting acceptance by a carrier.
    /// </summary>
    Open = 0,

    /// <summary>
    /// The load request has been assigned to a carrier but not yet completed.
    /// </summary>
    Assigned = 1,

    /// <summary>
    /// The load has been delivered and the request is complete.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// The load request has been cancelled by the posting user or the platform.
    /// </summary>
    Cancelled = 3
}
