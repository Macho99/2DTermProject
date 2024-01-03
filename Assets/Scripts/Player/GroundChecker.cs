using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
           || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
        {
            player.isGroundChecked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")
           || collision.gameObject.layer == LayerMask.NameToLayer("BedRock"))
        {
            player.isGroundChecked = true;
        }
    }
}
