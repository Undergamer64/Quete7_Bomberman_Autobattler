using BehaviorTree;
using System.Collections.Generic;

public class CharacterTree : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        { 
            new Sequence(new List<Node> {}),
            new Sequence(new List<Node> {}),
            new Sequence(new List<Node> {})
        }
            );

        return root;
    }
}
