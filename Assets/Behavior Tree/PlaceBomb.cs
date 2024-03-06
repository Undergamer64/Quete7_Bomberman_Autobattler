using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceBomb : Node
{
    private GameObject m_character;
    private Pathfinding m_pathfinding;
    public PlaceBomb(GameObject charRef)
    {
        m_character = charRef;
        m_pathfinding = new Pathfinding();
    }


    public override NodeState Evaluate()
    {
        if (m_character.GetComponent<S_Character>().PlaceBomb())
        {
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }
        return state;
    }
}