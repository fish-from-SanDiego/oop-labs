using Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Services;

public interface ILogger
{
    void Log(LogMessage logMessage);
}