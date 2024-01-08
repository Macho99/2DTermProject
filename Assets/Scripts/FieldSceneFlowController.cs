using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSceneFlowController : MonoBehaviour
{
    private static FieldSceneFlowController instance;
    private static Player player;
    public static Player Player { get {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            return player;
        }}
    public static FieldSceneFlowController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
}
