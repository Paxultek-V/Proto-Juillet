using UnityEngine;

public class Item_Fx : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_deathFxPrefab = null;

    private Item m_item;

    private void OnEnable()
    {
        m_item = GetComponent<Item>();

        m_item.OnItemKill += PlayDeathFx;
    }

    private void OnDisable()
    {
        m_item.OnItemKill -= PlayDeathFx;
    }

    private void PlayDeathFx()
    {
        ParticleSystem fx = Instantiate(m_deathFxPrefab, transform.position, Quaternion.identity);
        fx.transform.localScale = transform.localScale;
        Destroy(fx.gameObject, 2f);
    }
}
