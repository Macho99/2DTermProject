using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEnter : StateBase<Customer.State, Customer>
{
    Vector2Int tablePos;
    List<Vector2Int> path;
    int curIdx;
    public CustomerEnter(Customer owner, StateMachine<Customer.State, Customer> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        curIdx = 0;
        RestauSceneFlowController SFC = RestauSceneFlowController.Instance;

        Vector2Int entrance = SFC.Entrance;
        owner.transform.position = new Vector3(entrance.x, entrance.y, 0f);
        owner.tableIdx = SFC.AllocateTable();
        tablePos = SFC.GetTablePos(owner.tableIdx);
        owner.SetAnimFloat("Speed", 1f);

        AStar.PathFinding(SFC.Map, entrance, tablePos, out path);

        owner.SetStateViewActive(false);
        owner.SetWaitMaskRatio(0f);
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
            stateMachine.ChangeState(Customer.State.Choose);
            return;
        }

        Vector2 dir = new Vector3(path[curIdx].x, path[curIdx].y) - owner.transform.position;

        if(dir.sqrMagnitude < 0.01f)
        {
            curIdx++;
        }

        owner.SetVel(dir.normalized * owner.MoveSpeed);
    }
}