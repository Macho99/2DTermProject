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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Init(int damage, Vector2 pos, Vector2 dir, float speed = 10f, float knockbackForce = 1f, float offTime = 3f)
    { 
        this.damage = damage;
        rb.position = pos;
        rb.velocity = dir * speed;
        transform.localScale = dir.x > 0f ? Vector3.one : new Vector3(-1, 1, 1);
        this.knockbackForce = knockbackForce;
        trailParticle.gameObject.SetActive(true);
        _ = StartCoroutine(CoOff(offTime));
    }

    private void OnDisable()
    {
        col.enabled = true;
        rb.gravityScale = 0.2f;
        rb.isKinematic = false;
    }
    private IEnumerator CoOff(float offTime)
    {
        yield return new WaitForSeconds(offTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trailParticle.gameObject.SetActive(false);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.TakeDamage(damage, rb.velocity / 3);
            gameObject.transform.parent = monster.ArrowHolder;
            col.enabled = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
