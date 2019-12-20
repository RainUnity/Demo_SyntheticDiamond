using UnityEngine;

public class MainScenes_Control : MonoBehaviour
{
    MainScenesObj_Control m_ObjControl;
    EnumModel m_EnumModel = EnumModel.GetInstance();

    void Awake()
    {
        m_ObjControl = GameObject.Find("ScenesControl").GetComponent<MainScenesObj_Control>();
        m_ObjControl.m_UIButton[0].onClick.AddListener(ButtonOnClick_BrowseMode);
        m_ObjControl.m_UIButton[1].onClick.AddListener(ButtonOnClick_StudyMode);
        m_ObjControl.m_UIButton[2].onClick.AddListener(ButtonOnClick_ExamMode);
        m_ObjControl.m_UIButton[3].onClick.AddListener(delegate () { ShowAndClose_Event(m_ObjControl.m_UIPanel[2], false); });
        m_EnumModel.m_Mode = Mode.Default;
    }

    void Update()
    {
        if (m_EnumModel.m_Mode == Mode.BrowseMode)
        {
            BrowseMode_Event();
        }
        else if (m_EnumModel.m_Mode == Mode.StudyMode)
        {
            StudyMode_Event();
        }
        else if (m_EnumModel.m_Mode == Mode.ExamMode)
        {
            ExamMode_Event();
        }
    }

    public void ShowAndClose_Event(GameObject obj, bool b)
    {
        if (b)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }

    public void BrowseMode_Event()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_ObjControl.m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                if (gameobj.tag == "collider")
                {
                    m_ObjControl.m_UIPanel[1].transform.position = new Vector3(Input.mousePosition.x + 200, Input.mousePosition.y - 200, Input.mousePosition.z);
                    m_ObjControl.m_UIPanel[1].SetActive(true);
                    switch (gameobj.name)
                    {
                        case "组009":
                            m_ObjControl.m_UITextHelpRight[0].text = "六面顶压机：";
                            m_ObjControl.m_UITextHelpRight[1].text = "铰链式六面顶压机是我国于1965年自主研发的一款新型六面顶压机，该装置通过与油缸连为整体的铰链梁将六个油缸固定在一起，具有结构简单紧凑的特点。通过该设备能够获得稳定的高温高压条件，并可对腔体中的温度、电阻等参数进行原位测量，铰链式六面顶压机因其结构紧凑、对中性好、加工及安装简单、造价低等优点成为国内超硬材料生产厂家的主要设备，同时也广泛应用于国内的静高压实验室中。";
                            m_ObjControl.m_BroweImage[0].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[0].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组012":
                            m_ObjControl.m_UITextHelpRight[0].text = "六面顶压机：";
                            m_ObjControl.m_UITextHelpRight[1].text = "铰链式六面顶压机是我国于1965年自主研发的一款新型六面顶压机，该装置通过与油缸连为整体的铰链梁将六个油缸固定在一起，具有结构简单紧凑的特点。通过该设备能够获得稳定的高温高压条件，并可对腔体中的温度、电阻等参数进行原位测量，铰链式六面顶压机因其结构紧凑、对中性好、加工及安装简单、造价低等优点成为国内超硬材料生产厂家的主要设备，同时也广泛应用于国内的静高压实验室中。";
                            m_ObjControl.m_BroweImage[0].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[1].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组010":
                            m_ObjControl.m_UITextHelpRight[0].text = "泄流阀、溢流阀";
                            m_ObjControl.m_UITextHelpRight[1].text = "泄流阀、溢流阀";
                            m_ObjControl.m_BroweImage[1].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[2].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组014":
                            m_ObjControl.m_UITextHelpRight[0].text = "泄流阀、溢流阀";
                            m_ObjControl.m_UITextHelpRight[1].text = "泄流阀、溢流阀";
                            m_ObjControl.m_BroweImage[1].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[3].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组011":
                            m_ObjControl.m_UITextHelpRight[0].text = "控制台";
                            m_ObjControl.m_UITextHelpRight[1].text = "控制台";
                            m_ObjControl.m_BroweImage[2].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[4].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组013":
                            m_ObjControl.m_UITextHelpRight[0].text = "控制台";
                            m_ObjControl.m_UITextHelpRight[1].text = "控制台";
                            m_ObjControl.m_BroweImage[2].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[5].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组015":
                            m_ObjControl.m_UITextHelpRight[0].text = "高压泵";
                            m_ObjControl.m_UITextHelpRight[1].text = "高压泵";
                            m_ObjControl.m_BroweImage[3].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[6].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组016":
                            m_ObjControl.m_UITextHelpRight[0].text = "高压泵";
                            m_ObjControl.m_UITextHelpRight[1].text = "高压泵";
                            m_ObjControl.m_BroweImage[3].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[7].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组017":
                            m_ObjControl.m_UITextHelpRight[0].text = "工具台";
                            m_ObjControl.m_UITextHelpRight[1].text = "工具台";
                            m_ObjControl.m_BroweImage[4].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[8].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组018":
                            m_ObjControl.m_UITextHelpRight[0].text = "工具台";
                            m_ObjControl.m_UITextHelpRight[1].text = "工具台";
                            m_ObjControl.m_BroweImage[4].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[9].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组067":
                            m_ObjControl.m_UITextHelpRight[0].text = "干燥箱";
                            m_ObjControl.m_UITextHelpRight[1].text = "干燥箱";
                            m_ObjControl.m_BroweImage[5].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[10].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组068":
                            m_ObjControl.m_UITextHelpRight[0].text = "干燥箱";
                            m_ObjControl.m_UITextHelpRight[1].text = "干燥箱";
                            m_ObjControl.m_BroweImage[5].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[11].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组065":
                            m_ObjControl.m_UITextHelpRight[0].text = "水泵";
                            m_ObjControl.m_UITextHelpRight[1].text = "水泵";
                            m_ObjControl.m_BroweImage[6].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[12].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void StudyMode_Event()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_ObjControl.m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                if (gameobj.tag == "collider")
                {
                    switch (gameobj.name)
                    {
                        case "m_1":
                            if (m_ObjControl.m_StudyStep == 1)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.打开水泵，使水泵里面的水从锤体流过，进行减温\n2.水泵开关高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_2":
                            if (m_ObjControl.m_StudyStep == 2)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查泄流阀是否拧紧\n2.泄流阀高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_3":
                            if (m_ObjControl.m_StudyStep == 3)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将溢流阀的数值调成20\n2.溢流阀高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_4":
                            if (m_ObjControl.m_StudyStep == 3)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将自动调成手动\n2.自动/手动按钮高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_5":
                            if (m_ObjControl.m_StudyStep == 5)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将手动调成自动\n2.自动/手动按钮高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            else if (m_ObjControl.m_StudyStep == 8)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.旋转开总停开关\n2.旋转开总停开关高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_6":
                            if (m_ObjControl.m_StudyStep == 6)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.手动泄压45秒后自动回程\n2.泄压按钮高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_7":
                            if (m_ObjControl.m_StudyStep == 7)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查上锤面是否有裂纹\n2.上锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_8":
                            if (m_ObjControl.m_StudyStep == 9)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查左锤面是否有裂纹\n2.左锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_9":
                            if (m_ObjControl.m_StudyStep == 10)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查右锤面是否有裂纹\n2.右锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_10":
                            if (m_ObjControl.m_StudyStep == 11)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查前锤面是否有裂纹\n2.前锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_11":
                            if (m_ObjControl.m_StudyStep == 12)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查后锤面是否有裂纹\n2.后锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_12":
                            if (m_ObjControl.m_StudyStep == 13)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查下锤面是否有裂纹\n2.下锤面高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_13":
                            if (m_ObjControl.m_StudyStep == 14)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将干燥箱打开\n2.干燥箱盖高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_14":
                            if (m_ObjControl.m_StudyStep == 15)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将组装块取出\n2.组装块高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_15":
                            if (m_ObjControl.m_StudyStep == 16)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.将组装块放置六面顶压机中心位置";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_16":
                            if (m_ObjControl.m_StudyStep == 17)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.设置温度和压力曲线\n2.保存工艺\n3.下载工艺\n4.操作台屏幕高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_17":
                            //if (m_ObjControl.m_StudyStep == 18)
                            //{
                            //    m_ObjControl.m_UITextHelpLeft[m_ObjControl.m_StudyStep + 1].text = "1.将溢流阀的数值调成20\n2.溢流阀高亮显示";
                            //    Step_Event(m_ObjControl.m_StudyStep);
                            //}
                            break;
                        case "m_18"://电脑屏幕
                            if (m_ObjControl.m_StudyStep == 18)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.点击六面顶压机上面的“空进”按钮\n2.“空进”按钮高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_19"://空进
                            if (m_ObjControl.m_StudyStep == 19)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.点击六面顶压机上面的“充液”按钮\n2.“充液”按钮高亮显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        case "m_20"://充液
                            if (m_ObjControl.m_StudyStep == 20)
                            {
                                m_ObjControl.m_UITextHelpLeft[1].text = "1.检查锤面\n2.锤面显示";
                                Step_Event(m_ObjControl.m_StudyStep);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void Step_Event(int step)
    {
        m_ObjControl.m_UITextHelpLeft[0].text = "操作提示";
        m_ObjControl.m_StudyObj[step - 1].GetComponent<HighlightableObject>().FlashingOff();
        m_ObjControl.m_StudyObj[step].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        m_ObjControl.m_StudyImageTab[step - 1].SetActive(false);
        m_ObjControl.m_StudyImageTab[step].SetActive(true);
        m_ObjControl.m_StudyImageBg[step - 1].color = m_ObjControl.m_ColorImage;
        if (m_ObjControl.m_StudyStep == 3)
        {
            m_ObjControl.m_StudyStep = 3;
        }
        else
        {
            m_ObjControl.m_StudyStep = step + 1;
        }
    }

    public void ExamMode_Event()
    {

    }

    public void ButtonOnClick_BrowseMode()
    {
        m_EnumModel.m_Mode = Mode.BrowseMode;
        for (int i = 0; i < m_ObjControl.m_UIPanel.Length; i++)
        {
            m_ObjControl.m_UIPanel[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_UIToggle.Length; i++)
        {
            m_ObjControl.m_UIToggle[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_StudyImageTab.Length; i++)
        {
            m_ObjControl.m_StudyImageTab[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        }
        m_ObjControl.m_UIPanel[0].SetActive(true);
        m_ObjControl.m_UIToggle[3].SetActive(true);
        m_ObjControl.m_UIToggle[0].SetActive(true);
        m_ObjControl.m_UITextHelpLeft[0].text = "浏览模式";
        m_ObjControl.m_UITextHelpLeft[1].text = "请点击设备，浏览设备信息。可以使用‘WSAD’四个键进行移动、鼠标右键进行视角旋转";
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void ButtonOnClick_StudyMode()
    {
        m_EnumModel.m_Mode = Mode.StudyMode;
        for (int i = 0; i < m_ObjControl.m_UIPanel.Length; i++)
        {
            m_ObjControl.m_UIPanel[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_UIToggle.Length; i++)
        {
            m_ObjControl.m_UIToggle[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_StudyImageTab.Length; i++)
        {
            m_ObjControl.m_StudyImageTab[i].SetActive(false);
        }
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOff();
        }
        m_ObjControl.m_UIPanel[0].SetActive(true);
        m_ObjControl.m_UIToggle[3].SetActive(true);
        m_ObjControl.m_UIToggle[1].SetActive(true);
        m_ObjControl.m_UITextHelpLeft[0].text = "操作提示";
        m_ObjControl.m_UITextHelpLeft[1].text = "1.打开总开关\n2.开关高亮显示";
        m_ObjControl.m_StudyObj[0].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        m_ObjControl.m_StudyImageTab[0].SetActive(true);
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<BoxCollider>().enabled = false;
        }
        m_ObjControl.m_StudyStep = 1;
        //-38.94096f,0.988704f,2.740776f
    }

    public void ButtonOnClick_ExamMode()
    {
        m_EnumModel.m_Mode = Mode.ExamMode;
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOff();
        }
        m_ObjControl.m_UIPanel[0].SetActive(true);
        m_ObjControl.m_UIToggle[2].SetActive(true);
    }
}
