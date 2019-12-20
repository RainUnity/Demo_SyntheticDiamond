using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjRotate : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.DOLocalMove(new Vector3(-51.948f, 0.822f, 0.716f),3f,false);
        transform.DOLocalRotate(new Vector3(90, 0, -212.93f), 3f, RotateMode.FastBeyond360);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
