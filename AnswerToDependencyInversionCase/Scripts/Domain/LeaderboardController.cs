namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
    using System.Collections.Generic;
    using UnityEngine;

    public class LeaderboardController
    {
        private readonly ILeaderboardProvider leaderboardProvider;
        private readonly ILeaderboardSorterFactory sorterFactory;

        public LeaderboardController(ILeaderboardProvider leaderboardProvider, ILeaderboardSorterFactory sorterFactory)
        {
            this.leaderboardProvider = leaderboardProvider;
            this.sorterFactory = sorterFactory;
        }

        public IEnumerable<ILeaderboardItem> GetItems(int sortType)
        {
            // SorterFactory'yi kullanarak sıralama stratejisini oluştur
            var sorter = sorterFactory.CreateSorter(sortType);

            // Oluşturulan sıralama stratejisini kullanarak lider tablosunu al
            return sorter.Sort(leaderboardProvider);
        }
    }
}
