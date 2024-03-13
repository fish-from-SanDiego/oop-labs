using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class TreeGotoParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 1;
    private const int FlagsExactNumber = 0;

    public TreeGotoParser()
        : base(new ParsingStrategy(new[] { "tree", "goto" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber)
            return new CommandWithIllFormedFlags();

        return new TreeGotoCommand(
            parseSuccess.MainArguments.First(),
            new SystemTypeBasedStrategy<IFileSystemPathChanger>(
                new LocalFileSystem(),
                new LocalSystemPathChanger(new LocalPathHandler())));
    }
}