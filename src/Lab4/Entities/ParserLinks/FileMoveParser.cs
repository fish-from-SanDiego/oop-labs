using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class FileMoveParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 2;
    private const int FlagsExactNumber = 0;

    public FileMoveParser()
        : base(new ParsingStrategy(new[] { "file", "move" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber)
            return new CommandWithIllFormedFlags();

        return new FileMoveCommand(
            parseSuccess.MainArguments.First(),
            parseSuccess.MainArguments.Last(),
            new SystemTypeBasedStrategy<IFileMover>(new LocalFileSystem(), new LocalFileMover(new LocalPathHandler())));
    }
}