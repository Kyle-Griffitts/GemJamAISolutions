using GemJamAISolutions.Data;
using GemJamAISolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GemJamAISolutions.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", async (
            [FromBody] RegisterRequest request,
            UserManager<ApplicationUser> userManager) =>
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Results.Ok(new AuthResponse
                {
                    Success = true,
                    Message = "User registered successfully"
                });
            }

            return Results.BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Registration failed",
                Errors = result.Errors.Select(e => e.Description).ToList()
            });
        });

        group.MapPost("/login", async (
            [FromBody] LoginRequest request,
            SignInManager<ApplicationUser> signInManager) =>
        {
            var result = await signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                request.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Results.Ok(new AuthResponse
                {
                    Success = true,
                    Message = "Login successful"
                });
            }

            return Results.Unauthorized();
        });

        group.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok(new AuthResponse
            {
                Success = true,
                Message = "Logout successful"
            });
        });

        group.MapGet("/user", async (
            ClaimsPrincipal user,
            UserManager<ApplicationUser> userManager) =>
        {
            if (user.Identity?.IsAuthenticated == true)
            {
                var appUser = await userManager.GetUserAsync(user);
                if (appUser != null)
                {
                    return Results.Ok(new UserInfo
                    {
                        Email = appUser.Email ?? string.Empty,
                        IsAuthenticated = true
                    });
                }
            }

            return Results.Ok(new UserInfo
            {
                Email = string.Empty,
                IsAuthenticated = false
            });
        });
    }
}
