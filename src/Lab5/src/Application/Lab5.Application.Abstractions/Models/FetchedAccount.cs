using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Application.Models;

namespace Lab5.Application.Abstractions.Models;

public record FetchedAccount(Account? Account, GetResult GetResult);