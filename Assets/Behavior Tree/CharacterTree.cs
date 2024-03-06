using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTree : BehaviorTree.Tree
{
    public GameObject m_ListOfBombs;
    public GameObject m_GridManager;
    public GameObject m_Character;
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
            //neutral
            new Selector(
                new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new Node()
                    }),
                    new Sequence(new List<Node>
                    {
                        new FindClosestWall(m_GridManager,m_Character),
                        new MoveTo(m_Character)
                    })
                })
        }
        );

        return root;
    }
}
