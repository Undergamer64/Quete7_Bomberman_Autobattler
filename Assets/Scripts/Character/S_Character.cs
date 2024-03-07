using System.Collections;
using UnityEngine;

public class S_Character : MonoBehaviour
{
    [SerializeField] 
    private GameObject m_listOfBombs;

    [SerializeField]
    private S_CharacterStats m_stats;

    [SerializeField]
    private GameObject m_playerList;

    [SerializeField]
    private GameObject m_bombPrefab;

    [SerializeField]
    private float m_timeToWait;

    [SerializeField]
    private bool m_canMove=true;

    [SerializeField]
    private bool m_canTakeDamage = true;
    private int m_lives;

    private S_Tile tempoTile;
    public int m_NbOfBombs = 1;
    public S_Tile m_currentTile;

    private void Start()
    {
        m_timeToWait = m_stats.m_Speed;
        m_NbOfBombs = m_stats.m_nbTraps;
        m_lives = m_stats.m_Lives;
    }

    public IEnumerator Invulnerability()
    {
        yield return new WaitForSeconds(1f);
        m_canTakeDamage = true;
    }

    public IEnumerator Temporisation()
    {
        m_canMove = false;
        yield return new WaitForSeconds(m_timeToWait);
        yield return new WaitForSeconds(Random.Range(0,0.2f));
        transform.position= new Vector3(tempoTile.m_TileX, tempoTile.m_TileY, -1);
        tempoTile = null;
        m_canMove = true;
    }

    public bool MoveToTile(S_Tile tile)
    {
        if (m_canMove)
        {
            if (m_currentTile.CanReach(tile))
            {
                if (!tile.gameObject.CompareTag("Wall") && !tile.gameObject.CompareTag("Destructable") && !tile.m_Character)
                {
                    m_currentTile.m_Character = null;
                    m_currentTile.m_IsWalkable = true;
                    m_currentTile = tile;
                    m_currentTile.m_IsWalkable = false;
                    m_currentTile.m_Character = this;
                    //transform.position = new Vector3(m_currentTile.m_TileX, m_currentTile.m_TileY, -1);
                    tempoTile = tile;
                    StartCoroutine(Temporisation());
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else 
        {
            transform.position=Vector3.Lerp(transform.position, new Vector3(tempoTile.m_TileX, tempoTile.m_TileY, -1), Time.deltaTime*(10-m_timeToWait));
        }
        return true;
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

    public bool PlaceBomb()
    {
        if (!m_currentTile.m_Bomb && m_NbOfBombs > 0 && m_canMove)
        {
            m_NbOfBombs -= 1;
            GameObject bomb = Instantiate(m_bombPrefab, transform.position, Quaternion.identity, m_listOfBombs.transform);
            bomb.GetComponent<S_Bomb>().m_Range = m_stats.m_Range;
            bomb.GetComponent<S_Bomb>().m_MaxPerforation = m_stats.m_Perforation;
            bomb.GetComponent<S_Bomb>().m_Tile = m_currentTile;
            bomb.GetComponent<S_Bomb>().m_Tile.m_IsWalkable = false;
            bomb.GetComponent<S_Bomb>().m_Character = this;
            m_currentTile.m_Bomb = bomb.GetComponent<S_Bomb>();
            S_GridManager.Instance.UpdateDanger();
            return true;
        }
        return false;
    }

    public void TakeDamage()
    {
        if (m_canTakeDamage)
        {
            m_canTakeDamage = false;
            m_lives--;
            if (m_lives <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Invulnerability());
            }
        }
    }
}
