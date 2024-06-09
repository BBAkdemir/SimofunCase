﻿namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Domain
{
    using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
    using System.Collections.Generic;

    public class FakeLeaderboardProvider : ILeaderboardProvider
    {
        private const int count = 10;

        public IEnumerable<ILeaderboardItem> GetItems()
        {
            for (var i = 1; i <= count; i++)
            {
                yield return new LeaderboardItem
                {
                    Name = "Name " + i,
                    Score = (count - (i - 1)) * 10
                };
            }
        }
    }
}