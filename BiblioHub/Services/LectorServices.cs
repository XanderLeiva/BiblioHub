using BiblioHub.DTOs;
using BiblioHub.Modelo;

namespace BiblioHub.Services;

public class LectorServices
{
    private readonly FirebaseService _firebaseService;

    public LectorServices(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    public async Task<Lector> Create(LectorDto dto, string userId)
    {
        var task = new Lector()
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
            DisplayName =  dto.DisplayName,
            PhoneNumber =  dto.PhoneNumber,
            UserId = userId,
            CreatedAt =  DateTime.UtcNow,
            BirthDate = dto.BirthDate,
            Country = dto.Country,
            Bio = dto.Bio,
            Role = dto.Role
        };

        await _firebaseService.GetCollection("task")
            .Document(task.Id)
            .SetAsync(new Dictionary<string, object>
            {
                { "Id",  task.Id },
                { "Email", task.Email},
                { "DisplayName", task.DisplayName },
                { "PhoneNumber", task.PhoneNumber },
                { "UserId", task.UserId },
                { "CreatedAt", task.CreatedAt },
                { "BirthDate", task.BirthDate },
                { "Country", task.Country },
                { "Bio", task.Bio },
                { "Role", task.Role }
            });
        return task;
    }

    public async Task<List<Lector>> GetByUser(string userId)
    {
        var snapshot = await _firebaseService.GetCollection("task")
            .WhereEqualTo("UserId", userId)
            .GetSnapshotAsync();
        
        var task = new List<Lector>();

        foreach (var doc in snapshot.Documents)
        {
            var data = doc.ToDictionary();
            task.Add(new Lector()
            {
                Id = data["Id"].ToString()!,
                Email = data["Email"].ToString()!,
                DisplayName = data["DisplayName"].ToString()!,
                PhoneNumber = data["PhoneNumber"].ToString()!,
                UserId = data["UserId"].ToString()!,
                CreatedAt = ((Google.Cloud.Firestore.Timestamp)data["CreatedAt"]).ToDateTime(),
                BirthDate = data["BirthDate"].ToString()!,
                Country = data["Country"].ToString()!,
                Bio = data["Bio"].ToString()!,
                Role = data["Role"].ToString()!,
            });
        }
        return task;
    }
}