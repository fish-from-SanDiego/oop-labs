using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class DisconnectParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 0;
    private const int FlagsExactNumber = 0;

    public DisconnectParser()
        : base(new ParsingStrategy(new[] { "disconnect" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber)
            return new CommandWithIllFormedFlags();

        return new DisconnectCommand(
            new SystemTypeBasedStrategy<IFileSystemDisconnector>(new LocalFileSystem(), new LocalSystemDisconnector()));
    }
}