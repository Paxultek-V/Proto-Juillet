using System;
using UnityEngine;

public class Level : LevelBase
{
    public static Action<int> OnSendTargetRank;

    [SerializeField] private int m_targetRankToReach = 4;

    protected override void InitializeLevel()
    {
        base.InitializeLevel();
        OnSendTargetRank?.Invoke(m_targetRankToReach);
    }
}
