using UnityEngine;

public class S_Character : MonoBehaviour
{
    [SerializeField]
    private S_CharacterStats m_stats;

    [SerializeField]
    private GameObject m_bombPrefab;

    public int m_NbOfBombs = 1;
    public S_Tile m_currentTile;

    private void Start()
    {
        m_NbOfBombs = m_stats.m_nbTraps;
    }

    public bool MoveToTile(S_Tile tile)
    {
        if (m_currentTile.CanReach(tile))
        {
            if (!tile.gameObject.CompareTag("Wall") && !tile.gameObject.CompareTag("Destructable") && !tile.m_Character)
            {
                m_currentTile.m_Character = null;
                m_currentTile = tile;
                m_currentTile.m_Character = this;
                transform.position = new Vector3( m_currentTile.m_TileX, m_currentTile.m_TileY, -1);
                return true;
            }
        }
        return false;
    }

    public void MoveUp()
    {
        MoveToTile(S_GridManager.Instance.m_GridList[m_currentTile.m_TileX][m_currentTile.m_TileY + 1]);
    }

    public void MoveDown()
    {
        MoveToTile(S_GridManager.Instance.m_GridList[m_currentTile.m_TileX][m_currentTile.m_TileY - 1]);
    }

    public void MoveLeft()
    {
        MoveToTile(S_GridManager.Instance.m_GridList[m_currentTile.m_TileX - 1][m_currentTile.m_TileY]);
    }

    public void MoveRight()
    {
        MoveToTile(S_GridManager.Instance.m_GridList[m_currentTile.m_TileX + 1][m_currentTile.m_TileY]);
    }

    public void PlaceBomb()
    {
        if (!m_currentTile.m_Bomb && m_NbOfBombs > 0)
        {
            m_NbOfBombs -= 1;
            GameObject bomb = Instantiate(m_bombPrefab, transform.position, Quaternion.identity);
            bomb.GetComponent<S_Bomb>().m_Range = m_stats.m_Range;
            bomb.GetComponent<S_Bomb>().m_MaxPerforation = m_stats.m_Perforation;
            bomb.GetComponent<S_Bomb>().m_Tile = m_currentTile;
            bomb.GetComponent<S_Bomb>().m_Character = this;
            m_currentTile.m_Bomb = bomb.GetComponent<S_Bomb>();
        }
    }
}
