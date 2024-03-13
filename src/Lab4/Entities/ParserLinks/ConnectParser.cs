using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class ConnectParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 1;
    private const int FlagsExactNumber = 1;

    public ConnectParser()
        : base(new ParsingStrategy(new[] { "connect" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber || parseSuccess.Flags.First().Name != "-m")
            return new CommandWithIllFormedFlags();
        if (parseSuccess.Flags.First().Value.First() == "local")
        {
            return new ConnectCommand(
                new LocalSystemConnector(),
                parseSuccess.MainArguments.First(),
                new LocalFileSystem());
        }

        return new ConnectCommand(
            new LocalSystemConnector(),
            parseSuccess.MainArguments.First(),
            new FileSystemUnknown());
    }
}