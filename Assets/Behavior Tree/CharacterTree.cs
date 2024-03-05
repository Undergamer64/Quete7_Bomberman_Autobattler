using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTree : BehaviorTree.Tree
{
    public GameObject m_ListOfBombs;
    public GameObject m_GridManager;
    public GameObject m_Character;
    public Pathfinding m_Pathfinding;
    public List<S_Tile> m_Path;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node> 
                {
                new CheckExplosionNode(m_ListOfBombs, m_Character), 
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
