using Itmo.Dev.Platform.Postgres.Models;
using Lab5.Application;
using Lab5.Infrastructure.DataAccess.Repositories;
using Lab5.Presentation.Console;
using Lab5.Presentation.Console.Scenarios;
using Spectre.Console;

var configuration = new PostgresConnectionConfiguration
{
    Host = "localhost",
    Port = 6432,
    Username = "postgres",
    Password = "postgres",
    Database = "postgres",
    SslMode = "Prefer",
};

var passwordRepository = new SystemPasswordRepository(configuration);
var accountRepository = new AccountRepository(configuration);

var passwordService = new PasswordService(passwordRepository);
var adminLoginService = new AdminLoginService(accountRepository, passwordRepository);
var accountLoginService = new AccountLoginService(accountRepository);

var initialScenario = new InitialScenario(passwordService, adminLoginService, accountLoginService);
string initialTitle = string.Empty;

var scenarioRunner = new ScenarioRunner(initialTitle, initialScenario);

while (scenarioRunner.TryRunScenario())
{
    AnsiConsole.Clear();
}