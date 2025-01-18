using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class S_Bomb : MonoBehaviour
{
    public float m_TimeBeforeExploding;
    public S_Tile m_Tile;
    public int m_Range;
    public int m_Perforation;
    public int m_MaxPerforation;
    private bool m_encounterObstacle = false;

    [SerializeField] 
    private GameObject m_PrefabExplosion;

    public S_Character m_Character;

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(m_TimeBeforeExploding);
        int maxKnife = 5;

        var newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
        newExplosion.name = $"Explosion";
        newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY];

        m_Perforation = m_MaxPerforation;
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].CompareTag("Destructable"))
            {
                if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].GetComponent<SpriteRenderer>().sprite == S_GridManager.Instance.m_DestructableWallSprite[2])
                {
                    if (m_Character.gameObject != null)
                    {
                        m_Character.GainCoin();
                    }
                }

                S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].ChangeTag("Untagged");
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY];
                }

                m_Perforation -= 1;
                if (m_Perforation <= 0)
                {
                    m_encounterObstacle = true;
                }
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY];
                }
            }
            if (m_encounterObstacle)
            {
                break;
            }
        }
        m_Perforation = m_MaxPerforation;
        m_encounterObstacle = false;
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].CompareTag("Destructable"))
            {
                if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].GetComponent<SpriteRenderer>().sprite == S_GridManager.Instance.m_DestructableWallSprite[2])
                {
                    if (m_Character.gameObject != null)
                    {
                        m_Character.GainCoin();
                    }
                }

                S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].ChangeTag("Untagged");
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY];
                }

                m_Perforation -= 1;
                if (m_Perforation <= 0)
                {
                    m_encounterObstacle = true;
                }
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY];
                }
            }
            if (m_encounterObstacle)
            {
                break;
            }
        }
        m_Perforation = m_MaxPerforation;
        m_encounterObstacle = false;
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].CompareTag("Destructable"))
            {
                if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].GetComponent<SpriteRenderer>().sprite == S_GridManager.Instance.m_DestructableWallSprite[2])
                {
                    if (m_Character.gameObject != null)
                    {
                        m_Character.GainCoin();
                    }
                }

                S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].ChangeTag("Untagged");
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i];
                }

                m_Perforation -= 1;
                if (m_Perforation <= 0)
                {
                    m_encounterObstacle = true;
                }
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i];
                }
            }
            if (m_encounterObstacle)
            {
                break;
            }
        }
        m_Perforation = m_MaxPerforation;
        m_encounterObstacle = false;
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].CompareTag("Destructable"))
            {
                if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].GetComponent<SpriteRenderer>().sprite == S_GridManager.Instance.m_DestructableWallSprite[2])
                {
                    if (m_Character.gameObject != null)
                    {
                        m_Character.GainCoin();
                    }
                }

                S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].ChangeTag("Untagged");
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i];
                }

                m_Perforation -= 1;
                if (m_Perforation <= 0)
                {
                    m_encounterObstacle = true;
                }
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].CompareTag("Wall"))
            {

                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                newExplosion.GetComponent<S_Explosion>().m_speed = 5;
                newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i];
                for (int j = Random.Range(0, maxKnife); j < maxKnife; j++)
                {
                    newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
                    newExplosion.name = $"Explosion";
                    newExplosion.GetComponent<S_Explosion>().m_speed = Random.Range(1, 5);
                    newExplosion.GetComponent<S_Explosion>().m_RandomTravel = true;
                    newExplosion.GetComponent<S_Explosion>().m_Tile = S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i];
                }
            }
            if (m_encounterObstacle)
            {
                break;
            }
        }
        m_Tile.m_IsWalkable = true;
        m_Character.m_NbOfBombs += 1;
        Destroy(gameObject);
    }

}    
