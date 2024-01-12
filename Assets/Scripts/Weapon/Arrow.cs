using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] ParticleSystem trailParticle;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private int damage;
    private Vector2 dir;
    private float speed;
    private float knockbackForce;
    private float offTime;
    private Coroutine angleAdjustCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Init(int damage, Vector2 pos, Vector2 dir, float speed = 10f, float knockbackForce = 1f, float offTime = 10f)
    {
        this.damage = damage;
        transform.position = pos;
        transform.right = dir;
        rb.velocity = dir * speed;
        this.knockbackForce = knockbackForce;
        trailParticle.gameObject.SetActive(true);
        col.enabled = true;
        angleAdjustCoroutine = StartCoroutine(CoAngleAdjust());

        _ = StartCoroutine(CoOff(offTime));
    }

    private void OnDisable()
    {
        rb.gravityScale = 0.2f;
        rb.isKinematic = false;
    }

    private IEnumerator CoAngleAdjust()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.right = rb.velocity;
        }
    }

    private IEnumerator CoOff(float offTime)
    {
        yield return new WaitForSeconds(offTime);
        ObjReturn();
    }

    public void ObjReturn()
    {
        FieldObjPool.Instance.ReturnObj(ObjPoolType.Arrow, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.TakeDamage(damage, rb.velocity / 3);
            gameObject.transform.parent = monster.ArrowHolder;
            col.enabled = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            StopCoroutine(angleAdjustCoroutine);
            _= StartCoroutine(CoTrailOff());
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            col.enabled = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            StopCoroutine(angleAdjustCoroutine);
            _ = StartCoroutine(CoTrailOff());
        }
    }

    private IEnumerator CoTrailOff()
    {
        yield return new WaitForSeconds(0.5f);
        trailParticle.gameObject.SetActive(false);
    }
}