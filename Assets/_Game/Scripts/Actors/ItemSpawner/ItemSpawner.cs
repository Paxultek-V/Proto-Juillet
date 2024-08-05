using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : GameflowBehavior
{
    [SerializeField] private Item m_itemPrefab = null;

    [SerializeField] private Transform m_spawnPosition = null;

    [SerializeField] private Transform m_itemParent = null;

    [SerializeField] private float m_spawnCooldown = 1f;

    [SerializeField] private int m_minItemRank = 0;

    [SerializeField] private int m_maxItemRank = 2;

    [SerializeField] private int m_rankOffset = 3;

    private Item m_itemBuffer;
    private Rigidbody m_bodyBuffer;
    private float m_cooldownTimer;
    private bool m_canSpawn;
    private bool m_hasReleased;
    private bool m_isSpawningEnabled;

    private int m_currentMinItemRank;
    private int m_currentMaxItemRank;
    private int m_currentMaxRankReached;

    protected override void OnEnable()
    {
        base.OnEnable();
        Controller.OnRelease += OnRelease;
        ItemRank_Controller.OnBroadcastRank += OnBroadcastRank;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Controller.OnRelease -= OnRelease;
        ItemRank_Controller.OnBroadcastRank -= OnBroadcastRank;
    }

    private void Start()
    {
        m_currentMinItemRank = m_minItemRank;
        m_currentMaxItemRank = m_maxItemRank;
    }

    private void Update()
    {
        ManageCooldown();
    }


    protected override void OnMainMenu()
    {
        base.OnMainMenu();
        m_isSpawningEnabled = false;
        m_cooldownTimer = 0f;
    }

    protected override void OnGameplay()
    {
        base.OnGameplay();

        if (m_spawnPosition.childCount > 0)
            Destroy(m_spawnPosition.transform.GetChild(0));

        m_isSpawningEnabled = true;
        m_canSpawn = true;
        m_hasReleased = false;
        SpawnItem();
    }

    protected override void OnGameover()
    {
        base.OnGameover();
        m_isSpawningEnabled = false;
    }

    protected override void OnVictory()
    {
        base.OnVictory();
        m_isSpawningEnabled = false;
    }

    private void ManageCooldown()
    {
        if (m_isSpawningEnabled == false)
            return;

        if (m_canSpawn == false && m_hasReleased)
        {
            m_cooldownTimer += Time.deltaTime;

            if (m_cooldownTimer > m_spawnCooldown)
            {
                m_canSpawn = true;

                SpawnItem();
            }
        }
    }

    private void OnRelease(Vector3 cursorPosition)
    {
        ReleaseItem();
    }

    private void OnBroadcastRank(int rank)
    {
        UpdateRankSpawnRange(rank);
    }


    private void UpdateRankSpawnRange(int reachedRank)
    {
        if (reachedRank > m_rankOffset)
        {
            if (m_currentMaxRankReached < reachedRank)
                m_currentMaxRankReached = reachedRank;
            else
                return;

            int diff = m_currentMaxRankReached - m_rankOffset;

            m_currentMaxItemRank = m_maxItemRank + diff;
            m_currentMinItemRank = m_minItemRank + diff;
        }
    }

    private void SpawnItem()
    {
        m_canSpawn = false;
        m_hasReleased = false;
        m_cooldownTimer = 0f;

        m_itemBuffer = Instantiate(m_itemPrefab, m_spawnPosition.position, Quaternion.identity, m_spawnPosition);

        m_itemBuffer.Initialize(Random.Range(m_currentMinItemRank, m_currentMaxItemRank + 1));

        m_bodyBuffer = m_itemBuffer.transform.GetComponent<Rigidbody>();

        m_bodyBuffer.isKinematic = true;
    }

    private void ReleaseItem()
    {
        m_hasReleased = true;

        if (m_bodyBuffer == null)
            return;

        m_bodyBuffer.transform.parent = m_itemParent;
        m_bodyBuffer.isKinematic = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_spawnPosition.position, 1f);
    }
}