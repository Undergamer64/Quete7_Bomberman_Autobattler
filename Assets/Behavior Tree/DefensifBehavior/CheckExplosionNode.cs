using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckExplosionNode : Node
{
    private GameObject m_character;

    public CheckExplosionNode(GameObject ListOfBombs, GameObject Character)
    {
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
        S_GridManager.Instance.UpdateDanger();
        return !S_GridManager.Instance.m_DangerousTiles.Contains(m_character.GetComponent<S_Character>().m_currentTile);
    }

}