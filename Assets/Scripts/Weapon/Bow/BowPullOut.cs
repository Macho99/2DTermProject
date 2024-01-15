using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BowPullOut : StateBase<Bow.State, Bow>
{
    float endDelay = 1f;
    float enterTime;

    public BowPullOut(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        owner.Player.PlayAnim("PullOut");
        owner.Player.SetAnimAttackSpeed(0.2f);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(owner.transform.position + Vector3.up * 0.5f,
            new Vector2(0.5f, 1f), 0f, Vector2.right * owner.Player.dir, 10f, LayerMask.GetMask("Monster"));

        foreach(RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Monster>().ArrowPullOut(owner.Damage / 2);
            GameObject effect = FieldObjPool.Instance.AllocateObj(ObjPoolType.BloodExplosionParticle);
            effect.transform.position = hit.transform.position;
        }
    }

    public override void Exit()
    {
        owner.Player.SetAnimAttackSpeed();
    }

    public override void Setup()
    {

    }

    public override void Transition()
    {
        if (Time.time > enterTime + endDelay)
        {
            stateMachine.ChangeState(Bow.State.Idle);
        }
    }

    public override void Update()
    {

    }
}