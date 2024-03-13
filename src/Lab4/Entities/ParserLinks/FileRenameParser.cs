using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Common.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.CommandStrategies.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public class FileRenameParser : ParserChainLinkBase
{
    private const int MainArgsNumber = 2;
    private const int FlagsExactNumber = 0;

    public FileRenameParser()
        : base(new ParsingStrategy(new[] { "file", "rename" }, MainArgsNumber))
    {
    }

    public override IFileSystemCommand Handle(Request request)
    {
        ParseResult parseResult = ParseRequest(request);
        if (parseResult is not ParseSuccess parseSuccess)
            return HandleNext(request);

        if (parseSuccess.Flags.Count != FlagsExactNumber)
            return new CommandWithIllFormedFlags();

        return new FileRenameCommand(
            parseSuccess.MainArguments.First(),
            parseSuccess.MainArguments.Last(),
            new SystemTypeBasedStrategy<IFileRenamer>(
                new LocalFileSystem(),
                new LocalFileRenamer(new LocalPathHandler())));
    }
}