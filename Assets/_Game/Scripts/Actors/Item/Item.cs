using System;
using UnityEngine;

public class Item : GameflowBehavior
{
    public Action OnItemKill;

    private ItemRank_Controller m_itemRankController;

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
