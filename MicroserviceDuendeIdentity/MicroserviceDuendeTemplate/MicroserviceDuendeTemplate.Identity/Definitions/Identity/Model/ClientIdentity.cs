namespace MicroserviceDuendeTemplate.Identity.Definitions.Identity.Model;

public class ClientIdentity
{
    public string Name { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<string>? Scopes { get; set; }  

    public static IEnumerable<ClientIdentity> GetTestClients()
        => new List<ClientIdentity>
        {
            new ClientIdentity()
            {
                Name = "Microservice.Identity",
                Id = "Microservice.Identity-4523f-21321",
                Secret = "Microservice.Identity-4523f-21321_21354g",
                Url = "https://localhost:7189",
                Type = "Bearer",
            }
        };
}
