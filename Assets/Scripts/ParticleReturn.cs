using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReturn : MonoBehaviour
{
    [SerializeField] ObjPoolType type;

    private Vector3 initScale;
    private Quaternion initRotation;

    private void Awake()
    {
        initScale = transform.localScale;
        initRotation = transform.rotation;
    }

    private void OnParticleSystemStopped()
    {
        transform.localScale = initScale;
        transform.localRotation = initRotation;
        FieldObjPool.Instance.ReturnObj(type, gameObject);
    }
}