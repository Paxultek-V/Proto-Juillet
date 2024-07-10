using UnityEngine;

public class ItemCollision_Controller : MonoBehaviour
{
    private ItemRank_Controller m_itemRankController;

    private void Awake()
    {
        m_itemRankController = GetComponent<ItemRank_Controller>();
    }


    private void OnCollisionEnter(Collision other)
    {
        CheckCollisionWithOtherItem(other);
    }

    private bool CheckCollisionWithOtherItem(Collision other)
    {
        if (m_itemRankController.IsMaxxed())
            return false;
        
        ItemRank_Controller otherItemRankController = other.transform.GetComponent<ItemRank_Controller>();

        if (otherItemRankController == null)
            return false;

        if (m_itemRankController.CurrentRank != otherItemRankController.CurrentRank)
            return false;

        
        m_itemRankController.IncreaseRank();

        Destroy(otherItemRankController.gameObject);

        return true;
    }
}