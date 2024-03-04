using System.Collections.Generic;
using BehaviorTree;

public class DefensifNode : Node
{
    public DefensifNode() : base() { }
    public DefensifNode(List<Node> children) : base(children) { }


    public override NodeState Evaluate()
    {
        state = NodeState.FAILURE;
        return state;
    }
}