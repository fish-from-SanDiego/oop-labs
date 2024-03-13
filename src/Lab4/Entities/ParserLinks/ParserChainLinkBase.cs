using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public abstract class ParserChainLinkBase : IParserChainLink
{
    private readonly IParsingStrategy _parsingStrategy;
    private IParserChainLink? _next;

    protected ParserChainLinkBase(IParsingStrategy parsingStrategy)
    {
        ParserException.ThrowIfNull(parsingStrategy, nameof(parsingStrategy));

        _parsingStrategy = parsingStrategy;
    }

    public IParserChainLink AddNext(IParserChainLink link)
    {
        if (_next is null)
        {
            _next = link;
        }
        else
        {
            _next.AddNext(link);
        }

        return this;
    }

    public abstract IFileSystemCommand Handle(Request request);

    protected IFileSystemCommand HandleNext(Request request)
    {
        return _next is null ? new UnknownCommand() : _next.Handle(request);
    }

    protected ParseResult ParseRequest(Request request)
    {
        ParserException.ThrowIfNull(request, nameof(request));

        return _parsingStrategy.Execute(request.Values);
    }
}