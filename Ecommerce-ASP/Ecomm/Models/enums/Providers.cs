using System.Runtime.Serialization;

namespace Ecomm.enums;

public enum Providers
{
    [EnumMember(Value = "Cash")] Cash,
    [EnumMember(Value = "Card")] Card
}