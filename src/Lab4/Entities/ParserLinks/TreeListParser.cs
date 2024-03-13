using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class TreeListParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 0;
    private const int FlagsMaxNumber = 1;
    private const int DefaultDepth = 1;
    private readonly TreeListSymbols _symbols;

    public TreeListParser(TreeListSymbols symbols)
        : base(new ParsingStrategy(new[] { "tree", "list" }, MainArgsNumber))
    {
        ParserException.ThrowIfNull(symbols, nameof(symbols));

        _symbols = symbols;
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count > FlagsMaxNumber ||
            (parseSuccess.Flags.Count > 0 && parseSuccess.Flags.First().Name != "-d"))
            return new CommandWithIllFormedFlags();
        int depth = DefaultDepth;
        if ((parseSuccess.Flags.Count > 0 && !int.TryParse(parseSuccess.Flags.First().Value.First(), out depth)) ||
            depth < 0)
        {
            return new CommandWithIllFormedFlags();
        }

        return new TreeListCommand(
            depth,
            new SystemTypeBasedStrategy<IFileSystemTraverser>(
                new LocalFileSystem(),
                new LocalSystemTraverser(_symbols.FileSymbol, _symbols.DirectorySymbol, _symbols.IndentSymbol)));
    }
}