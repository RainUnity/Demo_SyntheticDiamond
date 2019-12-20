using UnityEngine;
using UnityEngine.UI;

public class RingProcess : MonoBehaviour
{
    //进度条速度
    public float speed;
    //一个图片一个文字
    public Transform m_Image;
    public Transform m_Text;
    public Text m_TextShow;
    //进度控制
    public float targetProcess = 100;
    private float currentAmout = 0;

    private void Awake()
    {
        m_TextShow.text = "自动泄压中...";
    }
    void Update()
    {
        if (currentAmout < targetProcess)
        { 
            currentAmout += speed;
            if (currentAmout == targetProcess)
            {
                currentAmout = targetProcess;
                m_TextShow.text = "泄压完成！";
            }
            m_Text.GetComponent<Text>().text = ((int)currentAmout).ToString() + "秒";
            m_Image.GetComponent<Image>().fillAmount = currentAmout / 45.0f;
        }
    }
}
