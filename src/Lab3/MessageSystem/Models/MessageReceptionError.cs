namespace Itmo.ObjectOrientedProgramming.Lab3.MessageSystem.Models;

public sealed record MessageReceptionError(string? Description) : MessageReceptionResult;