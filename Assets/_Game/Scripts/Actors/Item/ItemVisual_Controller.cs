using System;
using DG.Tweening;
using UnityEngine;

public class ItemVisual_Controller : MonoBehaviour
{
    [SerializeField] private Transform m_visualTransform = null;
    [SerializeField] private Transform m_visualTextTransform = null;
    
    [SerializeField] private MeshRenderer m_meshRenderer = null;

    [SerializeField] private ItemData_SO m_itemRankData = null;
    
    private ItemRank_Controller m_itemRankController;

    private void Awake()
    {
        m_itemRankController = GetComponent<ItemRank_Controller>();
    }

    private void OnEnable()
    {
        m_itemRankController.OnUpdateRank += UpdateVisual;
    }

    private void OnDisable()
    {
        m_itemRankController.OnUpdateRank -= UpdateVisual;
    }

    private void Start()
    {
        m_visualTransform.localScale = Vector3.one * 0.4f;
        m_visualTransform.DOScale(Vector3.one, 0.4f);
        
        m_visualTextTransform.localScale = Vector3.one * 0.4f;
        m_visualTextTransform.DOScale(Vector3.one, 0.4f);
    }

    public void UpdateVisual(int currentRank)
    {
        m_meshRenderer.material = m_itemRankData.GetItemRankData(currentRank).Material;
    }
    
    
}
