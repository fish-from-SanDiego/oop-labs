using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.ParserLinks;

public interface IParserChainLink
{
    IParserChainLink AddNext(IParserChainLink link);
    IFileSystemCommand Handle(Request request);
}
