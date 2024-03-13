using System;
using Itmo.ObjectOrientedProgramming.Lab4.Entities;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

IParserChainLink parser = new ConnectParser()
    .AddNext(new DisconnectParser())
    .AddNext(new FileCopyParser())
    .AddNext(new FileDeleteParser())
    .AddNext(new FileMoveParser())
    .AddNext(new FileRenameParser())
    .AddNext(new FileShowParser())
    .AddNext(new TreeGotoParser())
    .AddNext(new TreeListParser(new TreeListSymbols('%', '^', ' ')));

IFileSystemManager manager = new FileSystemManager(new StringTokenizer('\"', new[] { ' ', '\"' }), parser);
while (true)
{
    string? line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
        break;
    Console.WriteLine();
    Console.WriteLine(manager.Handle(line));
    Console.WriteLine();
}