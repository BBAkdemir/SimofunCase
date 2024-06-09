namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    using System;

    // LeaderboardSorterFactory arayüzü
    public interface ILeaderboardSorterFactory
    {
        ILeaderboardSorter CreateSorter();
    }

    // LeaderboardSorterByScore ve LeaderboardSorterByName sınıflarını oluşturan fabrika sınıfı
    public class LeaderboardSorterFactory : ILeaderboardSorterFactory
    {
        public ILeaderboardSorter CreateSorter(int sortType)
        {
            switch (sortType)
            {
                case 0:
                    return new LeaderboardSorterByScore();
                case 1:
                    return new LeaderboardSorterByName();
                default:
                    throw new ArgumentException("Invalid sort type.");
            }
        }
    }
}