using System;
using UnityEngine;

public class Item : GameflowBehavior
{
    public Action OnItemKill;

    private ItemRank_Controller m_itemRankController;

    protected override void OnEnable()
    {
        base.OnEnable();
        UI_ButtonDebug_NextLevel.OnDebugNextLevelButtonPressed += OnDebugNextLevelButtonPressed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UI_ButtonDebug_NextLevel.OnDebugNextLevelButtonPressed -= OnDebugNextLevelButtonPressed;
    }

    public void Initialize(int initialRank = 0)
    {
        m_itemRankController = GetComponent<ItemRank_Controller>();

        if (m_itemRankController == null)
        {
            Debug.LogError("m_itemRankController is null");
            return;
        }
            
        m_itemRankController.Initialize(initialRank);
    }


    private void OnDebugNextLevelButtonPressed()
    {
        OnItemKill?.Invoke();
        Destroy(gameObject);
    }

    protected override void OnVictory()
    {
        base.OnVictory();
        OnItemKill?.Invoke();
        Destroy(gameObject);
    }

    protected override void OnGameover()
    {
        base.OnGameover();
        OnItemKill?.Invoke();
        Destroy(gameObject);
    }
}
