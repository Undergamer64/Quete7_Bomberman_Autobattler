using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bomb : MonoBehaviour
{
    public float m_TimeBeforeExploding;
    public S_Tile m_Tile;
    public int m_Range;
    private bool m_encounterObstacle = false;
    [SerializeField] private GameObject m_PrefabExplosion;
    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(m_TimeBeforeExploding);

        var newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY].transform.position, Quaternion.identity);
        newExplosion.name = $"Explosion";
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].CompareTag("Desctructable"))
            {
                //S_GridManager.Instance.m_GridList[tile.m_TileX + i][tile.m_TileY].
                m_encounterObstacle = true;
                break;
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX + i][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
            }
            break;
        }
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].CompareTag("Desctructable"))
            {
                //S_GridManager.Instance.m_GridList[tile.m_TileX + i][tile.m_TileY].
                m_encounterObstacle = true;
                break;
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX - i][m_Tile.m_TileY].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
            }
            break;
        }
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].CompareTag("Desctructable"))
            {
                //S_GridManager.Instance.m_GridList[tile.m_TileX + i][tile.m_TileY].
                m_encounterObstacle = true;
                break;
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY + i].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
            }
            break;
        }
        for (int i = 1; i < m_Range + 1; i++)
        {
            if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].CompareTag("Desctructable"))
            {
                //S_GridManager.Instance.m_GridList[tile.m_TileX + i][tile.m_TileY].
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
                m_encounterObstacle = true;
                break;
            }
            else if (S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].CompareTag("Wall"))
            {
                m_encounterObstacle = true;
                break;
            }
            else
            {
                newExplosion = Instantiate(m_PrefabExplosion, S_GridManager.Instance.m_GridList[m_Tile.m_TileX][m_Tile.m_TileY - i].transform.position, Quaternion.identity);
                newExplosion.name = $"Explosion";
            }
            break;
        }
        Destroy(gameObject);
    }

}    
