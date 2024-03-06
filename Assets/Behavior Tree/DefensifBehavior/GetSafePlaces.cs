using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using System.Linq;

public class GetSafePlaces : Node
{
    private GameObject m_character;
    private Pathfinding m_pathfinding;

    public GetSafePlaces(GameObject charRef)
    {
        m_pathfinding = new Pathfinding();
        m_character = charRef;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;

        List<S_Tile> DangerousPlaces = S_GridManager.Instance.m_DangerousTiles;
        List<S_Tile> SafePlaces = new List<S_Tile>();

        for (int i = 0; i < S_GridManager.Instance.m_Width; i++)
        {
            foreach (S_Tile tile in S_GridManager.Instance.m_GridList[i])
            {
                if (!DangerousPlaces.Contains(tile) && tile.gameObject.CompareTag("Untagged"))
                {
                    SafePlaces.Add(tile);
                }
            }
        }
        SafePlaces.OrderBy(x => m_pathfinding.CalculateDistance(x.GetComponent<S_Tile>(), m_character.GetComponent<S_Character>().m_currentTile));
        List<S_Tile> path = null;
        foreach (S_Tile tile in SafePlaces)
        {
            path = m_pathfinding.FindPath(m_character.GetComponent<S_Character>().m_currentTile, tile.GetComponent<S_Tile>());
            if (path != null)
            {
                state = NodeState.SUCCESS;
                break;
            }
        }
        parent.parent.SetData("path", path);
        return state;
    }
}