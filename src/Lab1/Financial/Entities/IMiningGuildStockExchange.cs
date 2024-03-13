using Itmo.ObjectOrientedProgramming.Lab1.Financial.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Financial.Entities;

public interface IMiningGuildStockExchange<TProduct>
{
    MiningGuildCredits GetProductPrice(TProduct product);
    bool ContainsProduct(TProduct product);
    void AddRate(TProduct product, MiningGuildCredits price);
    void RemoveRate(TProduct product);
}