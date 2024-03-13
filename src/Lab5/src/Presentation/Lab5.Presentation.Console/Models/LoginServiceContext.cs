using Lab5.Application.Contracts;

namespace Lab5.Presentation.Console.Models;

public record LoginServiceContext(IAdminLoginService AdminLoginService, IAccountLoginService AccountLoginService);