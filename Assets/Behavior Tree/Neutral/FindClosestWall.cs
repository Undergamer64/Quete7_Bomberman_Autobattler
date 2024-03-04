using System.Collections.Generic;
using BehaviorTree;

public class FindClosestWall : Node
{
    public FindClosestWall() : base() { }
    public FindClosestWall(List<Node> children) : base(children) { }


    public override NodeState Evaluate()
    {
        //code for the check here
        state = NodeState.FAILURE;
        return state;
    }
}