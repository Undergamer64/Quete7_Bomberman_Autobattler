using System.Collections.Generic;
using BehaviorTree;

public class OffensifCheckNode : Node
{
    public OffensifCheckNode() : base() { }
    public OffensifCheckNode(List<Node> children) : base(children) { }


    public override NodeState Evaluate()
    {
        //code for the check here
        state = NodeState.FAILURE;
        return state;
    }
}