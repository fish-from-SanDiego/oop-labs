using System.Collections.Generic;
using System.Linq;
using Lab5.Application;
using Lab5.Application.Abstractions.Models;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Contracts.ResultTypes;
using Lab5.Application.Models;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class BusinessLogicTests
{
    [Fact]
    public void DepositShouldCorrectlyUpdateAccountBalance()
    {
        // Arrange
        var balanceHistory = new List<long> { 33 };
        IAccountRepository repository = MockedRepository(balanceHistory);
        var sut = new CurrentAccountService(repository, 1);

        // Act
        OperationResult<Transaction> result = sut.DepositMoney(1000);
        var success = result as OperationSuccess<Transaction>;

        // Assert
        Assert.IsType<OperationSuccess<Transaction>>(result);
        Assert.NotNull(success);
        Assert.Equal(1033, balanceHistory.Last());
        Assert.Equal(33, success.Value.BalanceBeforeTransaction);
        Assert.Equal(1033, success.Value.BalanceAfterTransaction);
    }

    [Fact]
    public void DepositShouldNotPassWhenAmountIsNotPositive()
    {
        // Arrange
        var balanceHistory = new List<long> { 42 };
        IAccountRepository repository = MockedRepository(balanceHistory);
        var sut = new CurrentAccountService(repository, 1);

        // Act
        OperationResult<Transaction> result = sut.DepositMoney(-12);

        // Assert
        Assert.IsType<OperationError<Transaction>>(result);
        Assert.Equal(42, balanceHistory.Last());
    }

    [Fact]
    public void WithdrawShouldCorrectlyUpdateAccountBalance()
    {
        // Arrange
        var balanceHistory = new List<long> { 400 };
        IAccountRepository repository = MockedRepository(balanceHistory);
        var sut = new CurrentAccountService(repository, 1);

        // Act
        OperationResult<Transaction> result = sut.WithdrawMoney(370);
        var success = result as OperationSuccess<Transaction>;

        // Assert
        Assert.IsType<OperationSuccess<Transaction>>(result);
        Assert.NotNull(success);
        Assert.Equal(30, balanceHistory.Last());
        Assert.Equal(400, success.Value.BalanceBeforeTransaction);
        Assert.Equal(30, success.Value.BalanceAfterTransaction);
    }

    [Fact]
    public void WithdrawShouldNotPassWhenAmountIsNotPositive()
    {
        // Arrange
        var balanceHistory = new List<long> { 74536 };
        IAccountRepository repository = MockedRepository(balanceHistory);
        var sut = new CurrentAccountService(repository, 1);

        // Act
        OperationResult<Transaction> result = sut.WithdrawMoney(-44);

        // Assert
        Assert.IsType<OperationError<Transaction>>(result);
        Assert.Equal(74536, balanceHistory.Last());
    }

    [Fact]
    public void WithdrawShouldNotPassWhenItsAmountIsMoreThanBalance()
    {
        // Arrange
        var balanceHistory = new List<long> { 500 };
        IAccountRepository repository = MockedRepository(balanceHistory);
        var sut = new CurrentAccountService(repository, 1);

        // Act
        OperationResult<Transaction> result = sut.WithdrawMoney(650);

        // Assert
        Assert.IsType<OperationError<Transaction>>(result);
        Assert.Equal(500, balanceHistory.Last());
    }

    private static IAccountRepository MockedRepository(ICollection<long> balanceHistory)
    {
        IAccountRepository repository = Substitute.For<IAccountRepository>();
        Substitute.For<IAccountRepository>();
        repository.FindAccountById(1)
            .Returns(new FetchedAccount(new Account(1, balanceHistory.Last(), 0), new GetSuccess()));
        repository.UpdateAccountBalance(1, Arg.Any<long>()).Returns(new UpdateSuccess("message"));
        repository
            .When(r => r.UpdateAccountBalance(1, Arg.Any<long>()))
            .Do(info => balanceHistory.Add((long)info[1]));

        return repository;
    }
}