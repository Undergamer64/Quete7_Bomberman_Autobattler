using UnityEngine;

public class S_Tile : MonoBehaviour
{
    public int m_TileX;
    public int m_TileY;

    public S_Character m_Character;
    public S_Bomb m_Bomb;
    public int m_GCost;
    public int m_HCost;
    public int m_FCost;
    public bool m_IsWalkable=true;
    public S_Tile m_PreviousTile;

    public bool CanReach(S_Tile tile)
    {
        if (!tile.m_Character && !tile.m_Bomb)
        {
            var GridList = S_GridManager.Instance.m_GridList;

            if (tile == GridList[m_TileX + 1][m_TileY] && m_TileX + 1 <= S_GridManager.Instance.m_Width)
            {
                return true;
            }

            if (tile == GridList[m_TileX - 1][m_TileY] && m_TileX - 1 >= 0)
            {
                return true;
            }

            if (tile == GridList[m_TileX][m_TileY + 1] && m_TileY + 1 <= S_GridManager.Instance.m_Height)
            {
                return true;
            }

            if (tile == GridList[m_TileX][m_TileY - 1] && m_TileY - 1 >= 0)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeTag(string tag)
    {
        switch (tag)
        {
            case "Untagged":
                this.gameObject.GetComponent<SpriteRenderer>().sprite = S_GridManager.Instance.m_TileSprite;
                m_IsWalkable = true;
                break;
            case "Wall":
                this.gameObject.GetComponent<SpriteRenderer>().sprite = S_GridManager.Instance.m_WallSprite;
                break;
            case "Destructable":
                this.gameObject.GetComponent<SpriteRenderer>().sprite = S_GridManager.Instance.m_DestructableWallSprite;
                break;
            default:
                throw new System.ArgumentException("Tag provided is invalid");
        }
        this.gameObject.tag = tag;
    }

    public void CalculateFCost() 
    {
        m_FCost = m_GCost + m_HCost;
    }
}
