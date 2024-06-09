namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
    using System.Collections.Generic;

    public interface ILeaderboardProvider
    {
        IEnumerable<ILeaderboardItem> GetItems();
    }
}