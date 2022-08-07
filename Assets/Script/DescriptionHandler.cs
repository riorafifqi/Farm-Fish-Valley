using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionHandler : MonoBehaviour
{

    public Animator descAnimator;
    public TMP_Text descText;

    public bool isFinish = false;

    private void Awake()
    {
        descAnimator = gameObject.GetComponent<Animator>();
    }

    public void CloseDescriptionPanel()
    {
        isFinish = true;
    }
}
