using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDetector : MonoBehaviour
{
    Monster owner;
    private void Awake()
    {
        owner = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<FieldPlayer>(out FieldPlayer player))
        {
            _ = StartCoroutine(Notify(player));
        }
    }

    private IEnumerator Notify(FieldPlayer player)
    {
        yield return new WaitForSeconds(0.5f);
        owner.DetectPlayer(player);
    }
}
