using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class FileShowParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 1;
    private const int FlagsMaxNumber = 1;

    public FileShowParser()
        : base(new ParsingStrategy(new[] { "file", "show" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count > FlagsMaxNumber ||
            (parseSuccess.Flags.Count > 0 && parseSuccess.Flags.First().Name != "-m"))
            return new CommandWithIllFormedFlags();
        string mode = "console";
        if (parseSuccess.Flags.Count > 0)
        {
            mode = parseSuccess.Flags.First().Value.First();
        }

        return new FileShowCommand(
            mode == "console" ? new ConsoleOutput() : new UnknownOutputMode(),
            parseSuccess.MainArguments.First(),
            new[] { new OutputModeBasedStrategy<ITextWriter>(new ConsoleOutput(), new ConsoleTextWriter()) },
            new[]
            {
                new SystemTypeBasedStrategy<IFileShower>(
                    new LocalFileSystem(),
                    new LocalFileShower(new LocalPathHandler(), new SimpleFileReader())),
            });
    }
}