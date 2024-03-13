using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Common;
using Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Space.Models.FuelTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Financial.Entities;

public class MiningGuildFuelStockExchange : IMiningGuildStockExchange<FuelType>
{
    private readonly Dictionary<FuelType, MiningGuildCredits> _rates;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public MiningGuildFuelStockExchange()
    {
        _rates = new Dictionary<FuelType, MiningGuildCredits>();
    }

    public MiningGuildCredits GetProductPrice(FuelType product)
    {
        CheckProductPresence(product);
        return _rates[product];
    }

    public bool ContainsProduct(FuelType product)
    {
        return _rates.ContainsKey(product);
    }

    public void AddRate(FuelType product, MiningGuildCredits price)
    {
        CheckProductAbsence(product);
        _rates.Add(product, price);
    }

    public void RemoveRate(FuelType product)
    {
        CheckProductPresence(product);
        _rates.Remove(product);
    }

    private void CheckProductPresence(FuelType product)
    {
        if (!_rates.ContainsKey(product))
        {
            throw new StockExchangeException(
                "Product-connected exception",
                new KeyNotFoundException("Product has no price assigned to it"));
        }
    }

    private void CheckProductAbsence(FuelType product)
    {
        if (_rates.ContainsKey(product))
        {
            throw new StockExchangeException(
                "Product-connected exception",
                new KeyNotFoundException("Product already has price assigned to it"));
        }
    }
}