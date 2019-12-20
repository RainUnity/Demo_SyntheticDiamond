using UnityEngine;
using UnityEngine.UI;

public class GetTime : MonoBehaviour
{
    int hour;
    int minute;
    int second;
    int millisecond;
    // 已经花费的时间 
    float timeSpend = 0.0f;
    // 显示时间区域的文本 
    Text text_timeSpend;

    void Start()
    {
        text_timeSpend = GetComponent<Text>();
    }

    void Update()
    {
        timeSpend += Time.deltaTime;

        hour = (int)timeSpend / 3600;
        minute = ((int)timeSpend - hour * 3600) / 60;
        second = (int)timeSpend - hour * 3600 - minute * 60;
        millisecond = (int)((timeSpend - (int)timeSpend) * 1000);

        text_timeSpend.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
    }
}
