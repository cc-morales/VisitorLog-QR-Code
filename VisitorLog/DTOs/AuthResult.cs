namespace VisitorLog.DTOs;

public class AuthResult
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Message { get; set; }
}