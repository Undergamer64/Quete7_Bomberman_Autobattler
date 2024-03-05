using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckExplosionNode : Node
{
    private GameObject m_listOfBombs;
    private GameObject m_character;

    public CheckExplosionNode(GameObject ListOfBombs, GameObject Character)
    {
        m_listOfBombs = ListOfBombs;
        m_character = Character;
    }

    public override NodeState Evaluate()
    {
        if (IsSafe())
        {
            state = NodeState.FAILURE;
        }
        else
        {
            state = NodeState.SUCCESS;
        }
        return state;
    }

    private bool IsSafe()
    {
        List<S_Tile> DangerTiles = new List<S_Tile>();
        for (int i = 0; i < m_listOfBombs.transform.childCount; i++)
        {
            GameObject currentChild = m_listOfBombs.transform.GetChild(i).gameObject;

            bool encounterObstacle = false;
            int range = 0;
            for (int j = 0; j <= currentChild.GetComponent<S_Bomb>().m_Range; j++)
            {
                range = j;
                S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Bomb>().m_Tile.m_TileX + j][currentChild.GetComponent<S_Bomb>().m_Tile.m_TileY];
                if (!currentChild.CompareTag("Untagged"))
                {
                    encounterObstacle = true;
                }
                if (encounterObstacle)
                {
                    break;
                }
                if (!DangerTiles.Contains(currentTile))
                {
                    DangerTiles.Add(currentTile);
                }
            }
            encounterObstacle = false;
            range = 0;
            for (int j = 0; j <= currentChild.GetComponent<S_Bomb>().m_Range; j++)
            {
                range = j;
                S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Bomb>().m_Tile.m_TileX - j][currentChild.GetComponent<S_Bomb>().m_Tile.m_TileY];
                if (!currentChild.CompareTag("Untagged"))
                {
                    encounterObstacle = true;
                }
                if (encounterObstacle)
                {
                    break;
                }
                if (!DangerTiles.Contains(currentTile))
                {
                    DangerTiles.Add(currentTile);
                }
            }
            encounterObstacle = false;
            range = 0;
            for (int j = 0; j <= currentChild.GetComponent<S_Bomb>().m_Range; j++)
            {
                range = j;
                S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Bomb>().m_Tile.m_TileX][currentChild.GetComponent<S_Bomb>().m_Tile.m_TileY + j];
                if (!currentChild.CompareTag("Untagged"))
                {
                    encounterObstacle = true;
                }
                if (encounterObstacle)
                {
                    break;
                }
                if (!DangerTiles.Contains(currentTile))
                {
                    DangerTiles.Add(currentTile);
                }
            }
            encounterObstacle = false;
            range = 0;
            for (int j = 0; j <= currentChild.GetComponent<S_Bomb>().m_Range; j++)
            {
                range = j;
                S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Bomb>().m_Tile.m_TileX][currentChild.GetComponent<S_Bomb>().m_Tile.m_TileY - j];
                if (!currentChild.CompareTag("Untagged"))
                {
                    encounterObstacle = true;
                }
                if (encounterObstacle)
                {
                    break;
                }
                if (!DangerTiles.Contains(currentTile))
                {
                    DangerTiles.Add(currentTile);
                }
            }
        }
        return !DangerTiles.Contains(m_character.GetComponent<S_Character>().m_currentTile);
    }

}