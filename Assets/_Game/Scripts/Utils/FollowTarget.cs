using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform m_targetToFollow = null;

    [SerializeField] private Vector3 m_offset = Vector3.zero;

    private void Update()
    {
        transform.position = m_targetToFollow.position + m_offset;
    }
}
