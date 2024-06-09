namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    using System;
    using UnityEngine;

    // Observer arayüzü
    public interface ILeaderboardSortTypeObserver
    {
        void OnSortTypeChanged(int newSortType);
    }

    // LeaderboardController'ın sıralama türü değiştiğinde haberleşmesini sağlamak için bir Subject sınıfı
    public class LeaderboardSortTypeSubject
    {
        public event Action<int> SortTypeChanged;

        public void ChangeSortType(int newSortType)
        {
            SortTypeChanged?.Invoke(newSortType);
        }
    }
}