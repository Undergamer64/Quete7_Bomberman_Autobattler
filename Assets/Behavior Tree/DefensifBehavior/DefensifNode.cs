using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class DefensifNode : Node
{
    public DefensifNode() : base() { }
    public DefensifNode(List<Node> children) : base(children) { }


    public override NodeState Evaluate()
    {
        //code for the behavior here
        state = NodeState.FAILURE;

        //Debug.Log($"{this} : {state}");
        return state;
    }
}