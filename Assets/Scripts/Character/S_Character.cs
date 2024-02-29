using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Character : MonoBehaviour
{
    [SerializeField]
    private S_CharacterStats stats;
    
    public S_Tile m_currentTile;

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
}
