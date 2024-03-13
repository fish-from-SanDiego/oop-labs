namespace Lab5.Application.Models;

public record Transaction(long AccountId, long BalanceBeforeTransaction, long BalanceAfterTransaction);