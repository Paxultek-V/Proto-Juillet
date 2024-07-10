using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_Text_TargetRank : MonoBehaviour
{
    [SerializeField] private TMP_Text m_targetRank = null;

    [SerializeField] private Color m_transparentColor = Color.white;
    
    [SerializeField] private Color m_fullColor = Color.white;

    [SerializeField] private float m_animationDuration = 0.5f;
    
    
    
    private void OnEnable()
    {
        Level.OnSendTargetRank += OnSendTargetRank;
    }

    private void OnDisable()
    {
        Manager_GameState.OnSendCurrentGameState -= OnSendCurrentGameState;
    }

    private void OnSendTargetRank(int targetRank)
    {
        m_targetRank.text = "Target rank :\n" + (targetRank + 1);
    }
    
    private void OnSendCurrentGameState(GameState state)
    {
        if (state == GameState.Gameplay)
        {
            m_targetRank.color = m_transparentColor;
            m_targetRank.DOColor(m_fullColor, m_animationDuration).SetEase(Ease.Linear);
        }
    }
    
}
