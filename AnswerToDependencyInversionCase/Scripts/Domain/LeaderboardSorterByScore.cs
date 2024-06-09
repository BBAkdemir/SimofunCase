using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    public class LeaderboardSorterByScore : ILeaderboardSorter
    {
        public IEnumerable<ILeaderboardItem> Sort(IEnumerable<ILeaderboardItem> items)
        {
            return items.OrderByDescending(i => i.Score);
        }
    }
}