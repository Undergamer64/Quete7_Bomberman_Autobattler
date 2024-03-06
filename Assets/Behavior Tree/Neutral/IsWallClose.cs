using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IsWallClose : Node
{
    private GameObject m_character;
    private Pathfinding m_pathfinding;
    public IsWallClose(GameObject charRef)
    {
        m_character = charRef;
        m_pathfinding = new Pathfinding();
    }


    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        List<S_Tile> neighbour = m_pathfinding.GetNeighbourTile(m_character.GetComponent<S_Character>().m_currentTile);
        foreach (S_Tile neighbourTile in neighbour.Where(x=>x.CompareTag("Destructable")))
        {
            if (neighbourTile != null)
            {
                state=NodeState.SUCCESS; break;
            }
        }
        return state;
    }
}