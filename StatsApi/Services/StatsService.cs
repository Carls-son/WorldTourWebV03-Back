using System;

namespace StatsApi.Services
{
    public class StatsService
    {

        public double CalculateWinPercentage(int wins, int gamesPlayed)
        {
            if (gamesPlayed <= 0)
                return 0.0;

            return (double)wins / gamesPlayed * 100.0;
        }

    }
}