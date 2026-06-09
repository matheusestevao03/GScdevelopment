using LunarColonySimulator.Application.Services;
using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.UI;

namespace LunarColonySimulator;

// Ponto de entrada da aplicação. Mantém-se intencionalmente fino:
// monta as dependências, lê o cenário do usuário e dispara o engine.
public static class Program
{
    public static int Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ConsoleMenu.PrintBanner();

        try
        {
            var (colony, days, tick) = ConsoleMenu.PromptScenario();
            var engine = DependencyContainer.BuildEngine(persistLogToFile: true);
            engine.Run(colony, days, tick);

            Console.WriteLine();
            Console.WriteLine("Simulação finalizada. Pressione ENTER para sair.");
            if (args.Length == 0) Console.ReadLine();
            return 0;
        }
        catch (InvalidColonyConfigurationException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Configuração inválida: {ex.Message}");
            Console.ResetColor();
            return 2;
        }
        catch (ColonyException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro de domínio: {ex.Message}");
            Console.ResetColor();
            return 3;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Falha inesperada: {ex.GetType().Name} — {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }
}
