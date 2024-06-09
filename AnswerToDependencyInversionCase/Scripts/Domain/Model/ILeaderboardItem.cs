namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model
{
    public interface ILeaderboardItem
    {
        int Score { get; set; }

        string Name { get; set; }
    }
}
