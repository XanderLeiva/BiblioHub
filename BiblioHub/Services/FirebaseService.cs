using Google.Cloud.Firestore;

namespace BiblioHub.Services;

public class FirebaseService
{
    private readonly FirestoreDb _firestoreDb;

    public FirebaseService()
    {
        var credentialPath = Path.Combine(AppContext.BaseDirectory, "Config", "firebase-credentials.json");
        
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
        
        _firestoreDb = FirestoreDb.Create("bibliohub-f9be2");
    }
    
    public CollectionReference GetCollection (string collectionName)
    {
        return _firestoreDb.Collection(collectionName);
    }
}