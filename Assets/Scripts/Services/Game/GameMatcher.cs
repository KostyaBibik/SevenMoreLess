using System;
using Enums;
using Zenject;

namespace Services.Game
{
    public class GameMatcher : IInitializable, IDisposable
    {
        public static EComparisonStatus ComparisonChoice;
        
        public void Initialize()
        {
            ResetMatcherData();
        }
        
        private void ResetMatcherData()
        {
            ComparisonChoice = EComparisonStatus.None;
        }
        
        public void Dispose()
        {
            ResetMatcherData();
        }
    }
}