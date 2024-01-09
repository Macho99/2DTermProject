using System;
using System.Collections.Generic;
using UnityEngine;

public static class TimeExtension
{
    public static float UnscaledDeltaTime
    {
        get
        {
            if(Time.timeScale < 0.01f)
            {
                return 0f;
            }
            return Time.unscaledDeltaTime;
        }
    }
}
