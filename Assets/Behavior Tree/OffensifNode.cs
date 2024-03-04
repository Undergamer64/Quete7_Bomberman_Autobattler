using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class OffesifNode : Node
{
    public OffesifNode() : base() { }
    public OffesifNode(List<Node> children) : base(children) { }


    public override NodeState Evaluate()
    {
        //code for the behavior here
        state = NodeState.FAILURE;

        //Debug.Log($"{this} : {state}");
        return state;
    }
}