using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] protected string curState;
    [SerializeField] SpriteLibraryAsset spriteLibraryAsset;

    public Player player;
    public int Damage { get { return damage; } }
    public SpriteLibraryAsset SpriteLibraryAsset { get { return spriteLibraryAsset; } }
    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    protected virtual void OnEnable()
    {
        if(player.CurWeapon == null)
        {
            player.CurWeapon = this;
        }
        else
        {
            gameObject.SetActive(false);
        }
        StateMachineSetUp();
    }

    protected virtual void OnDisable()
    {
        if(player.CurWeapon == this)
        {
            player.CurWeapon = null;
            if(true == player.IsAttackState)
            {
                player.ChangeState(PlayerStateType.Idle);
            }
        }
        CallStateExit();
    }
    protected abstract void StateMachineSetUp();

    protected abstract void CallStateExit();

    public abstract void WeaponUpdate();
    public abstract void ForceIdle();


    //임의로 설정한 넉백 방향
    public void BoxAttack(int damage, int dir, float distance, Vector2 knockback, float stunTime, float delayTime)
    {
        _ = StartCoroutine(CoBoxAttack(damage, dir, distance, knockback, stunTime, delayTime));
    }

    //자동 설정된 넉백 방향
    public void BoxAttack(int damage, int dir, float distance, float knockbackForce, float stunTime, float delayTime)
    {
        _ = StartCoroutine(CoBoxAttack(damage, dir, distance, knockbackForce, stunTime, delayTime));
    }

    public IEnumerator CoBoxAttack(int damage, int dir, float distance, Vector2 knockback, float stunTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Vector2 origin = transform.position;
        origin.y += 0.5f;
        RaycastHit2D hit = Physics2D.BoxCast(origin, Vector2.one, 0f, Vector2.right * dir, distance, LayerMask.GetMask("Monster"));


        if (hit.collider != null && hit.collider.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(damage, knockback, stunTime);
        }
    }

    public IEnumerator CoBoxAttack(int damage, int dir, float distance, float knockbackForce, float stunTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Vector2 origin = transform.position;
        origin.y += 0.5f;
        RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(0.5f, 1f), 0f, Vector2.right * dir, distance, LayerMask.GetMask("Monster"));


        if (hit.collider != null && hit.collider.TryGetComponent<Monster>(out Monster monster))
        {
            Vector2 knockbackDir;
            //거리가 가까우면 플레이어의 공격 방향으로 넉백 되도록
            if ((monster.transform.position - transform.position).sqrMagnitude < 0.4f * 0.4f)
            {
                knockbackDir = Vector2.right * dir;
            }
            else
            {
                knockbackDir = monster.transform.position - transform.position;
                knockbackDir.Normalize();
            }
            monster.TakeDamage(damage, knockbackDir * knockbackForce, stunTime);
        }
    }
}