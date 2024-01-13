using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class BowArrowRain : StateBase<Bow.State, Bow>
{
    int curShot;
    const int maxShot = 10;
    
    float lastShotTime;
    const float attackDelay = 0.1f;
    const float endDelay = 3f;
    const float shottedEndDelay = 1f;
    float enterTime;
    public BowArrowRain(Bow owner, StateMachine<Bow.State, Bow> stateMachine) : base(owner, stateMachine)
    {
    }

    public override void Enter()
    {
        enterTime = Time.time;
        lastShotTime = 0f;
        curShot = 0;

        owner.Player.SetAnimAttackSpeed(2f);
        owner.Player.onAttackBtn1Pressed.AddListener(Shot);
        owner.Player.onAttackBtn2Pressed.AddListener(GoPullOut);

        Shot();
    }

    public override void Exit()
    {
        owner.Player.SetAnimAttackSpeed();
        owner.Player.onAttackBtn1Pressed.RemoveListener(Shot);
        owner.Player.onAttackBtn2Pressed.RemoveListener(GoPullOut);
        owner.Player.SetMultiPurposeBar(0f);
    }

    public override void Setup()
    {

    }

    private void GoPullOut()
    {
        stateMachine.ChangeState(Bow.State.PullOut);
    }

    private void Shot()
    {
        if(curShot > maxShot) {
            stateMachine.ChangeState(Bow.State.Idle);
            return; 
        }
        owner.Player.SetMultiPurposeBar((float)curShot / maxShot);

        if(Time.time > lastShotTime + attackDelay) {
            owner.Player.PlayAnim("Shot");
            Vector2 direction = Vector2.right * owner.Player.dir;
            direction.y += Random.Range(0f, 0.2f);
            owner.ShotArrow(owner.Damage / 5, direction, 10f, 0f);
            lastShotTime = Time.time;
            curShot++;
        }
    }

    public override void Transition()
    {
        if(curShot == 0)
        {
            if (Time.time > enterTime + endDelay)
            {
                stateMachine.ChangeState(Bow.State.Idle);
            }
        }
        else
        {
            if(Time.time > lastShotTime + shottedEndDelay)
            {
                stateMachine.ChangeState(Bow.State.Idle);
            }
        }
    }

    public override void Update()
    {

    }
}