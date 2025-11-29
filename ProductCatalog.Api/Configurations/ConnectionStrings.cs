using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Api.Configurations;

public sealed class ConnectionStrings
{
    [Required]
    public string Postgres { get; init; } = null!;
}
