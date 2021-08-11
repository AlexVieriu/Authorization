using Microsoft.AspNetCore.Authorization;

namespace FrankLiu.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public int ProbationMonths { get; }
        public HRManagerProbationRequirement(int probrationMonths)
        {
            ProbationMonths = probrationMonths;
        }
    }
}
