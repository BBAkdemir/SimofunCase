namespace Simofun.DevCaseStudy.Unity.DependencyInversion.Presentation.View
{
    using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain;
    using Simofun.DevCaseStudy.Unity.DependencyInversion.Domain.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    public class LeaderboardView : MonoBehaviour
    {
        #region Fields
        private int index;
        #endregion

        #region Unity Methods        
        /// <inheritdoc />
        protected virtual void Start()
        {
            var leaderboard = new LeaderboardController();
            var items = leaderboard.GetItems().ToList();
            DisplayLeaderboard(items);
        }
        #endregion

        #region Methods
        void DisplayLeaderboard(List<ILeaderboardItem> items)
        {
            foreach (var item in items)
            {
                Debug.Log(PrintLeaderboardItem(item));
            }
        }

        string PrintLeaderboardItem(ILeaderboardItem leaderboardItem)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Index: {++this.index}, ");
            stringBuilder.Append($"{nameof(ILeaderboardItem.Name)}: {leaderboardItem.Name}, ");
            stringBuilder.Append($"{nameof(ILeaderboardItem.Score)}: {leaderboardItem.Score}");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
