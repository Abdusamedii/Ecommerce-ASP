using System.Runtime.Serialization;

namespace Ecomm.enums;

public enum PaymentStatus
{
    [EnumMember(Value = "Cancelled")] Cancelled,
    [EnumMember(Value = "InProgress")] InProgress,
    [EnumMember(Value = "Paid")] Paid
}