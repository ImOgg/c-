using System;

namespace API.Entities;

public class AppUser
{
    public int MyProperty { get; set; }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public required string DisplayName { get; set; } = string.Empty;

    public required string Email { get; set; } = string.Empty;
}