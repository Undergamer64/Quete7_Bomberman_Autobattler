using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class MoveTo : Node
{
    private GameObject m_character;
    public MoveTo(GameObject charRef)
    {
        m_character = charRef;
    }
    public override NodeState Evaluate()
    {
        List<S_Tile> path = (List<S_Tile>)GetData("path");
        if (m_character.GetComponent<S_Character>().MoveToTile(path[0]))
        {
            state = NodeState.SUCCESS;
        }
        state = NodeState.FAILURE;
        return state;
    }
}