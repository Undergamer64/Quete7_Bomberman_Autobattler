using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckExplosionNode : Node
{
    private GameObject m_listOfBombs;

    public CheckExplosionNode() : base() { }
    public CheckExplosionNode(List<Node> children) : base(children) { }

    public CheckExplosionNode(GameObject ListOfBombs)
    {
        m_listOfBombs = ListOfBombs;
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
        for (int i = 0; i < m_listOfBombs.transform.childCount; i++)
        {
            GameObject currentChild = m_listOfBombs.transform.GetChild(i).gameObject;

            bool encounterObstacle = false;
            int range;
            for (int j = 0; j <= currentChild.GetComponent<S_Bomb>().m_Range; j++)
            {
                range = j;
                if (!S_GridManager.Instance.m_GridList[currentChild.GetComponent<S_Bomb>().m_Tile.m_TileX+j][currentChild.GetComponent<S_Bomb>().m_Tile.m_TileY].CompareTag("Untagged"))
                {
                    encounterObstacle = true;
                }
                if (encounterObstacle)
                {
                    break;
                }
            }
            encounterObstacle = false;
            /*if ((
                && )
                && 
                )
            */

        }
        return true;
    }

}