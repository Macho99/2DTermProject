using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimiter : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        //∏  ≈ª√‚ πÊ¡ˆ
        if(collision.TryGetComponent<FieldPlayer>(out FieldPlayer player))
        {
            player.transform.position = Vector3.zero;
        }
    }
}
