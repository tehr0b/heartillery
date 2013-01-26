using System;
using System.Collections.Generic;

/// <summary>
/// This class stores the difficulty level
/// by the total kills required to reach the level and the number of pastas at that level
/// </summary>
[Serializable]
public class DifficultyLevel
{
    /// <summary>
    /// The number of kills required to reach this difficulty level
    /// </summary>
    public int totalKillThreshold;

    /// <summary>
    /// The number of pizzas at this difficulty level
    /// </summary>
    public int pizzaCount;
}

/// <summary>
/// This class sorts the difficulty levels by the total kill threshold descending
/// </summary>
public class SortDifficultyLevelDescending : IComparer<DifficultyLevel>
{
    int IComparer<DifficultyLevel>.Compare(DifficultyLevel a, DifficultyLevel b)
    {
        if (a.totalKillThreshold > b.totalKillThreshold)
            return -1;
        if (a.totalKillThreshold < b.totalKillThreshold)
            return 1;
        else
            return 0;
    }
}
