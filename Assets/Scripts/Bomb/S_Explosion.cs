using System.Collections;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    public S_Tile m_Tile;
    [SerializeField]
    private float m_explosionTime;
    public float m_speed;
    private void Start()
    {
        Vector2 dist;
        dist.x = m_Tile.transform.position.x - transform.position.x;
        dist.y = m_Tile.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(m_explosionTime);
        transform.position = m_Tile.transform.position;
        m_Tile.m_MoveCost = 0;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(m_Tile.m_TileX, m_Tile.m_TileY, -1), Time.deltaTime * (10-m_speed));
        if (m_Tile.m_Character)
        {
            m_Tile.m_Character.TakeDamage();
        }
    }
}
