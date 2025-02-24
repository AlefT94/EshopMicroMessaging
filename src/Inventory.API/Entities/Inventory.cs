using FluentResults;
using Inventory.API.Errors;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.API.Entities;

public class Inventory
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QuantityReserve { get; private set; }

    [NotMapped]
    public decimal QuantityAvaliable => Quantity-QuantityReserve;

    private Inventory(Guid productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = 0;
        QuantityReserve = 0;
    }

    public Result ReserveStock(decimal quantityToReserve)
    {
        if (quantityToReserve <= 0)
        {
            return Result.Fail(new QuantityGreaterThanZero());
        }

        if (QuantityAvaliable - quantityToReserve < 0)
        {
            return Result.Fail(new QuantityNotAvaliable());
        }
        QuantityReserve += quantityToReserve;

        return Result.Ok();
    }

    public Result ConfirmReserve(decimal quantityToConfirm)
    {
        if (quantityToConfirm <= 0)
        {
            return Result.Fail(new QuantityGreaterThanZero());
        }

        if(quantityToConfirm > QuantityReserve)
        {
            Result.Fail(new QuantityNotAvaliable());
        }

        QuantityReserve -= quantityToConfirm;
        Quantity -= quantityToConfirm;

        return Result.Ok();
    }

    public Result CancelReserve(decimal quantityToCancel)
    {
        if (quantityToCancel <= 0)
        {
            return Result.Fail(new QuantityGreaterThanZero());
        }

        if (quantityToCancel > QuantityReserve)
        {
            Result.Fail(new QuantityNotAvaliable());
        }

        QuantityReserve -= quantityToCancel;

        return Result.Ok();
    }

    public Result IncreaseQuantity(decimal quantityToIncrease)
    {
        Quantity += quantityToIncrease;
        return Result.Ok();
    }

    public static Inventory Create(Guid productId, string productName)
    {
        return new Inventory(productId, productName);
    }
}
