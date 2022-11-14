using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public void Start()
    {
        transform.LeanScale(new Vector2(1.1f, 1.1f), 1f).setEaseInOutQuart().setLoopPingPong().setIgnoreTimeScale(true);
    }
}
