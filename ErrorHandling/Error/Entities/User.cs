namespace ErrorHandling.Error.Entities;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}