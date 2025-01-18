using System.Collections;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    public S_Tile m_Tile;
    [SerializeField]
    private float m_explosionTime;
    public float m_speed;
    private Vector2 m_travellingDistance;
    public bool m_RandomTravel=false;
    private void Start()
    {
        Vector2 dist;
        dist.x = m_Tile.transform.position.x - transform.position.x;
        dist.y = m_Tile.transform.position.y - transform.position.y;
        m_travellingDistance=new Vector3(dist.x-Random.Range(0,dist.x), dist.y - Random.Range(0, dist.y))+transform.position;
        
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        m_Tile.m_IsWalkable = false;
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(m_explosionTime);
        transform.position = m_Tile.transform.position;
        m_Tile.m_MoveCost = 0;
        Destroy(gameObject);
        m_Tile.m_MoveCost = 0;
        m_Tile.m_IsWalkable = true;
    }

    private void Update()
    {
        if (!m_RandomTravel)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(m_Tile.m_TileX, m_Tile.m_TileY, -1), Time.deltaTime * (10 - m_speed));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(m_travellingDistance.x, m_travellingDistance.y, -1), Time.deltaTime * (10 - m_speed));
        }
        if (m_Tile.m_Character)
        {
            m_Tile.m_Character.TakeDamage();
        }
    }
}
