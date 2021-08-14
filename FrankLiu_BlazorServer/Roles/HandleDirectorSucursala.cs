using Microsoft.AspNetCore.Authorization;

namespace FrankLiu_BlazorServer.Roles
{
    public class HandleDirectorSucursala : AuthorizationHandler<DirectorSucursala>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       DirectorSucursala requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "Sucursala") &&
                context.User.HasClaim(x => x.Type == "DataNastere"))

                return Task.CompletedTask;

            var dataNastere = DateTime.Parse(context.User.FindFirst(x => x.Type == "DataNastere").Value);
            var vechime = DateTime.Now.Year - dataNastere.Year;

            var sucursala = context.User.FindFirst(x => x.Type == "Sucursala").Value;

            if (vechime >= requirement.AniVechime && sucursala == "GARSP")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
