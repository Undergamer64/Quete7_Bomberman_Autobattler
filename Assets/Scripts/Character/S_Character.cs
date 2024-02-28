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
            if ((tile.gameObject.CompareTag("Wall") || tile.gameObject.CompareTag("Destructable")) && !tile.m_isPlayer)
            {
                m_currentTile.m_isPlayer = false;
                m_currentTile = tile;
                m_currentTile.m_isPlayer = true;
                return true;
            }
        }
        return false;
    }

    

}
