using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Control : MonoBehaviour
{
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateColorAlpha();
    }

    //设置的图片
    public Image m_Sprite;
    //透明值
    private float m_Alpha;
    private bool m_FadeStatu = false;
    void UpdateColorAlpha()
    {
        //控制透明值变化
        if (m_FadeStatu == false)
        {
            m_Alpha += 0.5f * Time.deltaTime;
        }
        else if (m_FadeStatu)
        {
            m_Alpha -= 0.5f * Time.deltaTime;
        }
        //获取到图片的透明值
        Color ss = m_Sprite.color;
        ss.a = m_Alpha;
        //将更改过透明值的颜色赋值给图片
        m_Sprite.color = ss;
        //透明值等于的1的时候 转换成淡出效果
        if (m_Alpha > 1f)
        {
            m_Alpha = 1f;
            m_FadeStatu = true;
        }
        //值为0的时候跳转场景
        else if (m_Alpha < 0)
        {
            Debug.Log("结束");
        }
    }
}
