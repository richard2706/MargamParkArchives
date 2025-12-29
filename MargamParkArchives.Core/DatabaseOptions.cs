using System.ComponentModel.DataAnnotations;

namespace MargamParkArchives.Core;

public sealed class DatabaseOptions
{
    [Required]
    public required string Server { get; set; }

    [Required]
    public required string Database { get; set; }

    [Required]
    public required string Uid { get; set; }
}
