using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetector : MonoBehaviour
{
    Monster owner;
    private void Awake()
    {
        owner = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            owner.DetectPlayer(player);
        }
    }
}
