using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    public class LeaderboardSorterByName : ILeaderboardSorter
    {
        public IEnumerable<ILeaderboardItem> Sort(IEnumerable<ILeaderboardItem> items)
        {
            return items.OrderBy(i => i.Name);
        }
    }
}
