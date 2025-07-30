using System.Runtime.Serialization;

namespace Ecomm.enums;

public enum Role
{
    [EnumMember(Value = "customer")]
    Customer,
    [EnumMember(Value = "admin")]
    Admin,
}