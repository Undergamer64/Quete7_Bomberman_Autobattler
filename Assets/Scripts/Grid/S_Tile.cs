using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tile : MonoBehaviour
{
    public int m_TileX;
    public int m_TileY;

    public bool m_isPlayer;

    public bool CanReach(S_Tile tile)
    {
        if (!m_isPlayer)
        {
            var GridList = S_GridManager.Instance.m_GridList;

            if (this == GridList[m_TileX + 1][m_TileY] && m_TileX + 1 <= S_GridManager.Instance.m_Width)
            {
                return true;
            }

            if (this == GridList[m_TileX - 1][m_TileY] && m_TileX - 1 >= 0)
            {
                return true;
            }

            if (this == GridList[m_TileX][m_TileY + 1] && m_TileY + 1 <= S_GridManager.Instance.m_Height)
            {
                return true;
            }

            if (this == GridList[m_TileX][m_TileY - 1] && m_TileY - 1 >= 0)
            {
                return true;
            }
        }
        return false;
    }
}
