using System.Text;
using System.Text.Json;

internal class Program
{
    private const string DefaultApiUrl = "https://localhost:7100/api/Patient";
    private const int DefaultPatientsToCreate = 100;

    private static readonly HttpClient Http = new();
    private static readonly Random Rng = new();

    private static async Task Main(string[] args)
    {
        var patientsToCreate = args.Length > 0 && int.TryParse(args[0], out var count)
            ? count
            : DefaultPatientsToCreate;

        var apiUrl = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : DefaultApiUrl;


        Console.WriteLine("Creating random patients...");

        for (int i = 0; i < patientsToCreate; i++)
        {
            var patient = RandomPatientFactory.Create();

            var json = JsonSerializer.Serialize(
                patient,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

            using var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await Http.PostAsync(apiUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Failed ({(int)response.StatusCode})");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Patient created ({i + 1})");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed ({ex.Message})");
                Console.ResetColor();
            }
            
            // Add some jitter
            await Task.Delay(Rng.Next(100, 400));
        }

        Console.WriteLine("Done.");
    }
}
