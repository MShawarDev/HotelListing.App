using HotelListing.App.Application.Contracts;
using HotelListing.App.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.App.Application.Services;

// Validate API keys against the database
public class ApiKeyValidatorService(AppDBContext db) : IApiKeyValidatorService
{
    public async Task<bool> IsValidAsync(string apiKey, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) return false;

        var apiKeyEntity = await db.ApiKeys
            .AsNoTracking()
            .FirstOrDefaultAsync(k => k.Key == apiKey, ct);

        if (apiKeyEntity is null) return false;

        // If there is no expiry date or the expiry date does not exceed today's date.
        return apiKeyEntity.IsActive;
    }
}
