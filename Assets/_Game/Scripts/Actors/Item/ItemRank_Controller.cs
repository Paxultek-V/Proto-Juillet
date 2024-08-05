using System;
using TMPro;
using UnityEngine;

public class ItemRank_Controller : MonoBehaviour
{
    public Action<int> OnUpdateRank;
    public static Action<int> OnBroadcastRank;

    [SerializeField] private int m_maxRank = 5;

    [SerializeField] private TMP_Text m_rankText = null;

    private int m_currentRank;

    public int CurrentRank
    {
        get => m_currentRank;
    }


    public void Initialize(int startRank = 0)
    {
        m_currentRank = startRank;
        OnUpdateRank?.Invoke(m_currentRank);

        m_rankText.text = (m_currentRank + 1).ToString();
    }

    public bool IsMaxxed()
    {
        return m_currentRank == m_maxRank;
    }

    public void IncreaseRank()
    {
        if (m_currentRank == m_maxRank)
            return;

        m_currentRank++;
        OnUpdateRank?.Invoke(m_currentRank);
        m_rankText.text = (m_currentRank + 1).ToString();
        OnBroadcastRank?.Invoke(m_currentRank);
    }
}