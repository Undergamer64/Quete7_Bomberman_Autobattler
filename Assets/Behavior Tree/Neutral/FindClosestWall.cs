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

    public FindClosestWall(GameObject gridRef, GameObject charRef, Pathfinding pathfindingRef)
    {
        m_gridManager = gridRef;
        m_pathfinding = pathfindingRef;
        m_character = charRef;
    }


    public override NodeState Evaluate()
    {/*
        //code for the check here
        for (int i = 0; i < m_gridManager.transform.childCount; i++)
        {
            if (m_gridManager.transform.GetChild(i).CompareTag("Wall"))
            {
                dist.Add(m_gridManager.transform.GetChild(i).gameObject);
                int potentialDistance=dist.OrderBy(x=>m_pathfinding.CalculateDistance(x.GetComponent<S_Tile>(),m_character.GetComponent<S_Character>().m_currentTile)))
            }
        }*/
        state = NodeState.FAILURE;
        return state;
    }
}