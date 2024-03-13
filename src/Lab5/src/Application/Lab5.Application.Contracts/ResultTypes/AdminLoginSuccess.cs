namespace Lab5.Application.Contracts.ResultTypes;

public sealed record AdminLoginSuccess(IAdminService AdminService) : AdminLoginResult;