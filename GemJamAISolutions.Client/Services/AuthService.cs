using System.Net.Http.Json;

namespace GemJamAISolutions.Client.Services;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}

public class AuthResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
}

public class UserInfo
{
    public string Email { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
}

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthResponse>()
                ?? new AuthResponse { Success = false, Message = "Unknown error" };
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return errorResponse ?? new AuthResponse { Success = false, Message = "Registration failed" };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthResponse>()
                ?? new AuthResponse { Success = false, Message = "Unknown error" };
        }

        return new AuthResponse { Success = false, Message = "Invalid email or password" };
    }

    public async Task<AuthResponse> LogoutAsync()
    {
        var response = await _httpClient.PostAsync("/api/auth/logout", null);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthResponse>()
                ?? new AuthResponse { Success = false, Message = "Unknown error" };
        }

        return new AuthResponse { Success = false, Message = "Logout failed" };
    }

    public async Task<UserInfo> GetUserInfoAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserInfo>("/api/auth/user")
                ?? new UserInfo { IsAuthenticated = false };
        }
        catch
        {
            return new UserInfo { IsAuthenticated = false };
        }
    }
}
