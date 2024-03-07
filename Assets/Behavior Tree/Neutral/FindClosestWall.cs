using System.Collections.Generic;
using System.Linq;
using BehaviorTree;
using UnityEngine;

public class FindClosestWall : Node
{
    private GameObject m_gridManager;
    private GameObject m_character;
    private Pathfinding m_pathfinding;
    private List<GameObject> dist = new List<GameObject>();

    public FindClosestWall(GameObject gridRef, GameObject charRef)
    {
        m_gridManager = gridRef;
        m_pathfinding = new Pathfinding();
        m_character = charRef;
        
    }


    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        for (int i = 0; i < m_gridManager.transform.childCount; i++)
        {
            if (m_gridManager.transform.GetChild(i).CompareTag("Destructable"))
            {
                dist.Add(m_gridManager.transform.GetChild(i).gameObject);
            }
        }
        dist.OrderBy(x => m_pathfinding.CalculateDistance(x.GetComponent<S_Tile>(), m_character.GetComponent<S_Character>().m_currentTile));
        List<S_Tile> path = null;
        foreach (GameObject wall in dist)
        {
            path = m_pathfinding.FindPath(m_character.GetComponent<S_Character>().m_currentTile, wall.GetComponent<S_Tile>());
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