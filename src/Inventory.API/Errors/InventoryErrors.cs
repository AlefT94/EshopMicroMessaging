using FluentResults;

namespace Inventory.API.Errors;

public class InventoryErrors
{
}

public class QuantityNotAvaliable : Error
{
    public QuantityNotAvaliable() : base("This quantity is not avaliable in inventory.")
    { }
}

public class QuantityGreaterThanZero: Error
{
    public QuantityGreaterThanZero() : base("Quantity must be greatter than zero")
    { }
}

public class ReserveDoesNotExists : Error
{
    public ReserveDoesNotExists() : base("This amount is not reserved")
    { }
}
