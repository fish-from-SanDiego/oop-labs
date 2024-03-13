using System;
using System.Collections.Generic;
using System.IO;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities;

public class FileSystemManager : IFileSystemManager
{
    private readonly IStringTokenizer _tokenizer;
    private readonly IParserChainLink _parser;
    private ICommandExecutionContext _context;

    public FileSystemManager(IStringTokenizer tokenizer, IParserChainLink parser)
    {
        FileSystemException.ThrowIfNull(tokenizer, nameof(tokenizer));
        FileSystemException.ThrowIfNull(parser, nameof(parser));

        _context = new CommandExecutionContext(new FileSystemDisconnected(), string.Empty, string.Empty);
        _tokenizer = tokenizer;
        _parser = parser;
    }

    public string CurrentPath => Path.GetFullPath(Path.Combine(_context.ConnectionPath, _context.CurrentPath));

    public string Handle(string request)
    {
        IReadOnlyCollection<string> tokens = _tokenizer.TokenizeLine(request);
        IFileSystemCommand command = _parser.Handle(new Request(tokens));
        CommandExecutionResult commandExecutionResult = command.Execute(_context);

        switch (commandExecutionResult)
        {
            case CommandExecutionSuccess success:
                _context = success.NewContext;
                return success.Output;
            case CommandExecutionError error:
                return $"Error: {error.Message}";
            default:
                throw new FileSystemException(new InvalidOperationException("unexpected result"));
        }
    }
}