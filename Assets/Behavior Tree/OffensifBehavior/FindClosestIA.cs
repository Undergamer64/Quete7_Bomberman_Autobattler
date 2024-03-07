using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindClosestIA : Node
{
    private GameObject m_gridManager;
    private GameObject m_character;
    private Pathfinding m_pathfinding;
    private List<S_Tile> dist = new List<S_Tile>();

    public FindClosestIA(GameObject gridRef, GameObject charRef)
    {
        m_gridManager = gridRef;
        m_pathfinding = new Pathfinding();
        m_character = charRef;

    }


    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        for (int i = 0; i < m_character.transform.parent.childCount; i++)
        {
            GameObject currentChild = m_character.transform.parent.transform.GetChild(i).gameObject;
            if (currentChild != m_character) 
            {
                dist.Add(currentChild.GetComponent<S_Character>().m_currentTile);
            }
        }
        foreach (S_Tile distance in dist)
        {
            distance.m_Dist = m_pathfinding.CalculateDistance(distance, m_character.GetComponent<S_Character>().m_currentTile);
        }
        dist = dist.OrderBy(x => x.m_Dist).ToList();
        List<S_Tile> path = null;
        foreach (S_Tile IA in dist)
        {
            path = m_pathfinding.FindPath(m_character.GetComponent<S_Character>().m_currentTile, IA);
            if (path != null)
            {
                m_gridManager.GetComponent<S_GridManager>().UpdateDanger();
                if (!m_gridManager.GetComponent<S_GridManager>().m_DangerousTiles.Contains(path[0]))
                {
                    dist.Clear();
                    state = NodeState.SUCCESS;
                    break;
                }
                path.Clear();
            }
        }
        dist.Clear();
        parent.parent.SetData("path", path);
        return state;
    }
}
