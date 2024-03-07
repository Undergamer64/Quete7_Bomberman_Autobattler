using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckForPlayers : Node
{
    private GameObject m_character;
    private List<S_Tile> m_reach = new List<S_Tile>();

    public CheckForPlayers(GameObject Character)
    {
        m_character = Character;
    }

    public override NodeState Evaluate()
    {
        if (FoundPlayer())
        {
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }
        return state;
    }

    private bool FoundPlayer()
    {
        m_reach.Clear();

        for (int i = 0; i < m_character.transform.childCount; i++)
        {
            GameObject currentChild = m_character.transform.GetChild(i).gameObject;

            if (currentChild.name != m_character.name)
            {
                bool encounterObstacle = false;
                int range = 0;
                int Perforation = currentChild.GetComponent<S_Character>().m_BombPerforation;
                for (int j = 0; j <= currentChild.GetComponent<S_Character>().m_BombRange; j++)
                {
                    range = j;
                    S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Character>().m_currentTile.m_TileX + j][currentChild.GetComponent<S_Character>().m_currentTile.m_TileY];
                    if (!currentTile.CompareTag("Untagged"))
                    {
                        Perforation--;
                        if (currentTile.CompareTag("Wall") || Perforation <= 0)
                        {
                            encounterObstacle = true;
                        }
                    }
                    if (encounterObstacle)
                    {
                        break;
                    }
                    if (!m_reach.Contains(currentTile))
                    {
                        m_reach.Add(currentTile);
                    }
                }
                encounterObstacle = false;
                range = 0;
                Perforation = currentChild.GetComponent<S_Character>().m_BombPerforation;
                for (int j = 0; j <= currentChild.GetComponent<S_Character>().m_BombRange; j++)
                {
                    range = j;
                    S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Character>().m_currentTile.m_TileX - j][currentChild.GetComponent<S_Character>().m_currentTile.m_TileY];
                    if (!currentTile.CompareTag("Untagged"))
                    {
                        Perforation--;
                        if (currentTile.CompareTag("Wall") || Perforation <= 0)
                        {
                            encounterObstacle = true;
                        }
                    }
                    if (encounterObstacle)
                    {
                        break;
                    }
                    if (!m_reach.Contains(currentTile))
                    {
                        m_reach.Add(currentTile);
                    }
                }
                encounterObstacle = false;
                range = 0;
                Perforation = currentChild.GetComponent<S_Character>().m_BombPerforation;
                for (int j = 0; j <= currentChild.GetComponent<S_Character>().m_BombRange; j++)
                {
                    range = j;
                    S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Character>().m_currentTile.m_TileX][currentChild.GetComponent<S_Character>().m_currentTile.m_TileY + j];
                    if (!currentTile.CompareTag("Untagged"))
                    {
                        Perforation--;
                        if (currentTile.CompareTag("Wall") || Perforation <= 0)
                        {
                            encounterObstacle = true;
                        }
                    }
                    if (encounterObstacle)
                    {
                        break;
                    }
                    if (!m_reach.Contains(currentTile))
                    {
                        m_reach.Add(currentTile);
                    }
                }
                encounterObstacle = false;
                range = 0;
                Perforation = currentChild.GetComponent<S_Character>().m_BombPerforation;
                for (int j = 0; j <= currentChild.GetComponent<S_Character>().m_BombRange; j++)
                {
                    range = j;
                    S_Tile currentTile = S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Character>().m_currentTile.m_TileX][currentChild.GetComponent<S_Character>().m_currentTile.m_TileY - j];
                    if (!currentTile.CompareTag("Untagged"))
                    {
                        Perforation--;
                        if (currentTile.CompareTag("Wall") || Perforation <= 0)
                        {
                            encounterObstacle = true;
                        }
                    }
                    if (encounterObstacle)
                    {
                        break;
                    }
                    if (!m_reach.Contains(currentTile))
                    {
                        m_reach.Add(currentTile);
                    }
                }
            }
        }

        return m_reach.Contains(m_character.GetComponent<S_Character>().m_currentTile);
    }

}