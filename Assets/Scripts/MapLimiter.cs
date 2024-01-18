using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapLimiter : MonoBehaviour
{
    public UnityEvent onExit;
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�� Ż�� ����
        if(collision.TryGetComponent<FieldPlayer>(out FieldPlayer player))
        {
            onExit?.Invoke();
        }
    }
}
