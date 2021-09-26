using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.AppStart
{
    public static class FirebaseServiceConfig
    {
        public static void InitFirebase(this IServiceCollection services)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential =
                    GoogleCredential.FromFile("Resources/spiritastro-2bfba-firebase-adminsdk-qk1a8-2c46bb7e8a.json")
            });

            services.AddScoped<IFirebaseService, FirebaseService>();
        }
    }
}