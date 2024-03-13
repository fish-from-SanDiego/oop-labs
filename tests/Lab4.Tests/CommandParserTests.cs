using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class CommandParserTests
{
    private readonly IParserChainLink _sut1;
    private readonly IStringTokenizer _sut2;

    public CommandParserTests()
    {
        _sut1 = new ConnectParser()
            .AddNext(new DisconnectParser())
            .AddNext(new FileCopyParser())
            .AddNext(new FileDeleteParser())
            .AddNext(new FileMoveParser())
            .AddNext(new FileRenameParser())
            .AddNext(new FileShowParser())
            .AddNext(new TreeGotoParser())
            .AddNext(new TreeListParser(new TreeListSymbols('%', '^', ' ')));

        _sut2 = new StringTokenizer('\"', new[] { '\t', ' ' });
    }

    public static IEnumerable<object[]> GenerateDifferentRequestResultPairs()
    {
        // Test case 1
        yield return new object[]
        {
            "connect [Address] -idontknowsuchaflag Mode",
            typeof(CommandWithIllFormedFlags),
        };

        // Test case 2
        yield return new object[]
        {
            "disconnect",
            typeof(DisconnectCommand),
        };

        // Test case 3
        yield return new object[]
        {
            "tree goto [Path]",
            typeof(TreeGotoCommand),
        };

        // Test case 4
        yield return new object[]
        {
            "tree list -badflag 5",
            typeof(CommandWithIllFormedFlags),
        };

        // Test case 5
        yield return new object[]
        {
            "file show [Path] -m unknownmode", // not ill-formed, just unknown mode
            typeof(FileShowCommand),
        };

        // Test case 6
        yield return new object[]
        {
            "file move [SourcePath] [DestinationPath]",
            typeof(FileMoveCommand),
        };

        // Test case 7
        yield return new object[]
        {
            "file copy [SourcePath] [DestinationPath]",
            typeof(FileCopyCommand),
        };

        // Test case 8
        yield return new object[]
        {
            "file delete [Path]",
            typeof(FileDeleteCommand),
        };

        // Test case 9
        yield return new object[]
        {
            "file rename [Path] [Name]",
            typeof(FileRenameCommand),
        };

        // Test case 10
        yield return new object[]
        {
            "file rename [Path] [Name] [Name]",
            typeof(UnknownCommand),
        };

        // Test case 11
        yield return new object[]
        {
            "I am command",
            typeof(UnknownCommand),
        };
    }

    [Fact]
    public void ShouldParseConnectWithCorrectParameter()
    {
        // Arrange
        string request = "connect \"fs\" -m local";

        // Act
        IFileSystemCommand command = _sut1.Handle(new Request(_sut2.TokenizeLine(request)));

        // Assert
        Assert.IsType<ConnectCommand>(command);
        var connectCommand = command as ConnectCommand;
        Assert.NotNull(connectCommand);
        Assert.Equal(new LocalFileSystem(), connectCommand.Mode);
    }

    [Fact]
    public void ShouldParseTreeListWithCorrectParameter()
    {
        // Arrange
        string request = "tree list -d 12";

        // Act
        IFileSystemCommand command = _sut1.Handle(new Request(_sut2.TokenizeLine(request)));

        // Assert
        Assert.IsType<TreeListCommand>(command);
        var treeListCommand = command as TreeListCommand;
        Assert.NotNull(treeListCommand);
        Assert.Equal(12, treeListCommand.Depth);
    }

    [Fact]
    public void ShouldParseFileShowWithCorrectParameter()
    {
        // Arrange
        string request = "file show path -m console";

        // Act
        IFileSystemCommand command = _sut1.Handle(new Request(_sut2.TokenizeLine(request)));

        // Assert
        Assert.IsType<FileShowCommand>(command);
        var fileShowCommand = command as FileShowCommand;
        Assert.NotNull(fileShowCommand);
        Assert.Equal(new ConsoleOutput(), fileShowCommand.OutputMode);
    }

    [Theory]
    [MemberData(nameof(GenerateDifferentRequestResultPairs))]
    public void ShouldCorrectlyParseDifferentCommands(string request, Type expectedResultType)
    {
        // Arrange

        // Act
        IFileSystemCommand command = _sut1.Handle(new Request(_sut2.TokenizeLine(request)));

        // Assert
        Assert.IsType(expectedResultType, command);
    }
}