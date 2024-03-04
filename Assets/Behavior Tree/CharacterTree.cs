using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTree : BehaviorTree.Tree
{
    public GameObject m_ListOfBombs;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node> 
                {
                new CheckExplosionNode(m_ListOfBombs), 
                new DefensifNode()
                }
            ),
            new Sequence(new List<Node> 
                {
                new OffensifCheckNode(), 
                new OffesifNode()
                }
            ),
            new NeutralNode()
        }
        );

        return root;
    }
}
