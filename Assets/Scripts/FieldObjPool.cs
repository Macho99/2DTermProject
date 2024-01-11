using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjPool : ObjPool
{
    private static FieldObjPool instance;
    public static FieldObjPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        Init();
    }
}