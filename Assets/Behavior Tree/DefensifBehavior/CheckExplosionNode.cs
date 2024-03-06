using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckExplosionNode : Node
{
    private GameObject m_listOfBombs;
    private GameObject m_character;
    private List<S_Tile> m_dangerousPlaces = new List<S_Tile>();

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
        return !S_GridManager.Instance.m_DangerousTiles.Contains(m_character.GetComponent<S_Character>().m_currentTile);
    }

}