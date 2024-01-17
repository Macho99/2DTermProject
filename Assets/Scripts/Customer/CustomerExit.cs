using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerExit : StateBase<Customer.State, Customer>
{
    Vector2Int entrancePos;
    List<Vector2Int> path;
    int curIdx;
    public CustomerExit(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        curIdx = 0;
        RestauSceneFlowController SFC = RestauSceneFlowController.Instance;

        entrancePos = SFC.Entrance;
        owner.SetAnimFloat("Speed", 1f);

        AStar.PathFinding(SFC.Map, Vector2Int.RoundToInt(owner.transform.position), entrancePos, out path);

        owner.SetWaitMaskRatio(0f);

        owner.Flip(true);
    }

    public override void Exit()
    {
        owner.SetAnimFloat("Speed", 0f);
        owner.SetVel(Vector2.zero);
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
    }

    public override void Update()
    {
        if (curIdx >= path.Count)
        {
            GameObject.Destroy(owner.gameObject);
            return;
        }

        Vector2 dir = new Vector3(path[curIdx].x, path[curIdx].y) - owner.transform.position;

        if (dir.sqrMagnitude < 0.01f)
        {
            curIdx++;
        }

        owner.SetVel(dir.normalized * owner.MoveSpeed);
    }
}