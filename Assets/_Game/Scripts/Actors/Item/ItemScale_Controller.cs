using System;
using UnityEngine;

public class ItemScale_Controller : MonoBehaviour
{
    [SerializeField] private Transform m_scaleController = null;

    [SerializeField] private ItemData_SO m_itemRankData = null;
    
    private ItemRank_Controller m_itemRankController;

    private void Awake()
    {
        m_itemRankController = GetComponent<ItemRank_Controller>();
    }

    private void OnEnable()
    {
        m_itemRankController.OnUpdateRank += UpdateScale;
    }
    
    private void OnDisable()
    {
        m_itemRankController.OnUpdateRank -= UpdateScale;
    }

    public void UpdateScale(int newRank)
    {
        m_scaleController.localScale = Vector3.one * m_itemRankData.GetItemRankData(newRank).Scale;
    }
}
