using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class S_Character : MonoBehaviour
{
    [SerializeField] 
    private GameObject m_listOfBombs;

    [SerializeField]
    private S_CharacterStats m_stats;

    [SerializeField]
    private GameObject m_bombPrefab;

    [SerializeField]
    private float m_timeToWait;

    [SerializeField]
    private bool m_canMove=true;

    [SerializeField]
    private TextMeshProUGUI lives;

    [SerializeField]
    private TextMeshProUGUI m_coins;

    [SerializeField]
    private GameObject m_PBCoin;

    [SerializeField]
    private GameObject m_CanvasCoin;

    [SerializeField]
    private bool m_canTakeDamage = true;
    public int m_lives;
    public int m_BombRange;
    public int m_BombPerforation;
    public float m_speed;
    public int coins = 0;

    private S_Tile tempoTile;
    public int m_NbOfBombs = 1;
    public S_Tile m_currentTile;

    private void Start()
    {
        m_NbOfBombs = m_stats.m_nbTraps;
        m_lives = m_stats.m_Lives;
        m_BombRange = m_stats.m_Range;
        m_BombPerforation = m_stats.m_Perforation;
        m_speed = m_stats.m_Speed;
    }

    public IEnumerator Invulnerability()
    {
        yield return new WaitForSeconds(1f);
        m_canTakeDamage = true;
    }

    public IEnumerator Temporisation()
    {
        m_canMove = false;
        float time = 0;
        while (true)
        {
            if(Vector2.Distance(transform.position,new Vector3(tempoTile.m_TileX, tempoTile.m_TileY, -1)) < 0.1f)
            {
                break;
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(tempoTile.m_TileX, tempoTile.m_TileY, -1), Time.deltaTime * (10 - m_speed));
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime*m_speed;
        }

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
                    Vector2 dist;
                    dist.x = m_currentTile.transform.position.x - tile.transform.position.x;
                    dist.y = m_currentTile.transform.position.y - tile.transform.position.y;
                    float angle = Mathf.Atan2(dist.y,dist.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
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
    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector2 moveDirection = ctx.ReadValue<Vector2>();
            switch (moveDirection.x, moveDirection.y) 
            {
                case (1,0) :
                    MoveRight();
                    break;
                case (-1,0) :
                    MoveLeft();
                    break;
                case(0,1) :
                    MoveUp();
                    break;
                case (0,-1) :
                    MoveDown();
                    break;
                default:
                    break;

            }

        }
    }

    public void PlaceBombAction()
    {
        PlaceBomb();
    }
    
    public bool PlaceBomb()
    {
        if (!m_currentTile.m_Bomb && m_NbOfBombs > 0 && m_canMove)
        {
            m_NbOfBombs -= 1;
            GameObject bomb = Instantiate(m_bombPrefab, transform.position, Quaternion.identity, m_listOfBombs.transform);
            bomb.GetComponent<S_Bomb>().m_Range = m_BombRange;
            bomb.GetComponent<S_Bomb>().m_MaxPerforation = m_BombPerforation;
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

            if (gameObject.transform == gameObject.transform.parent.transform.GetChild(0))
            {
                lives.text = m_lives.ToString() + " left";
            }

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

    public void GainCoin()
    {
        coins += Random.Range(1, 3);
        if (gameObject.transform == gameObject.transform.parent.transform.GetChild(0))
        {
            Instantiate(m_PBCoin, new Vector3(160,1000 + Random.Range(1,201), 0), Quaternion.identity, m_CanvasCoin.transform);
            UpdateCoinDisplay();
        }
    }

    public void UpdateCoinDisplay()
    {
        m_coins.text = coins.ToString() + " coins";
    }
}
