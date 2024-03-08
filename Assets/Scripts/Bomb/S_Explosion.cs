using System.Collections;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    public S_Tile m_Tile;

    [SerializeField]
    private float m_explosionTime;
    private void Start()
    {
        m_Tile.m_IsWalkable = false;
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(m_explosionTime);
        Destroy(gameObject);
        m_Tile.m_MoveCost = 0;
        m_Tile.m_IsWalkable = true;
    }

    private void Update()
    {
        if (m_Tile.m_Character)
        {
            m_Tile.m_Character.TakeDamage();
        }
    }
}
