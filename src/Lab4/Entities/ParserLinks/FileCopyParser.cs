using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class FileCopyParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 2;
    private const int FlagsExactNumber = 0;

    public FileCopyParser()
        : base(new ParsingStrategy(new[] { "file", "copy" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber)
            return new CommandWithIllFormedFlags();

        return new FileCopyCommand(
            parseSuccess.MainArguments.First(),
            parseSuccess.MainArguments.Last(),
            new SystemTypeBasedStrategy<IFileCopier>(
                new LocalFileSystem(),
                new LocalFileCopier(new LocalPathHandler())));
    }
}