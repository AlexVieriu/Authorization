using Microsoft.AspNetCore.Authorization;

namespace FrankLiu_BlazorServer.Roles
{
    public class DirectorSucursala : IAuthorizationRequirement
    {
        public int AniVechime { get; set; }

        public DirectorSucursala(int aniVechime)
        {
            AniVechime = aniVechime;
        }
    }
}
