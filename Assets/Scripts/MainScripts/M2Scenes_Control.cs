using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class M2Scenes_Control : MonoBehaviour
{
    M2ScenesObj_Control m_ObjControl;
    EnumModel m_EnumModel = EnumModel.GetInstance();

    void Awake()
    {
        m_ObjControl = GameObject.Find("ScenesControl").GetComponent<M2ScenesObj_Control>();
        m_ObjControl.m_BtnMoudule1.onClick.AddListener(BtnOnClick_M1);
        m_ObjControl.m_BtnMoudule2.onClick.AddListener(BtnOnClick_M2);
        m_ObjControl.m_BtnMoudule3.onClick.AddListener(BtnOnClick_M3);
        m_ObjControl.m_BtnMoudule4.onClick.AddListener(BtnOnClick_M4);
        m_ObjControl.m_BtnSplit.onClick.AddListener(BtnOnClick_M1_SplitEvent_Modify);
        m_ObjControl.m_BtnMerge.onClick.AddListener(BtnOnClick_M1_MergeEvent_Modify);
        m_ObjControl.m_BtnBack.onClick.AddListener(BtnOnClick_M1_BackEvent);
        m_ObjControl.m_BtnBackM2.onClick.AddListener(BtnOnClick_M2_BackEvent_Modify);
        m_ObjControl.m_BtnRestart.onClick.AddListener(BtnOnClick_M2_RestartEvent_Modify);
        m_ObjControl.m_BtnAniM2.onClick.AddListener(BtnOnClick_M2_AniEvent);
        m_ObjControl.m_BtnShowM2.onClick.AddListener(BtnOnClick_M2_ShowEvent);
        m_ObjControl.m_BtnAnalyzeOkM4.onClick.AddListener(delegate ()
        {
            ShowAndClose_Event(m_ObjControl.m_UICraftSetPanel, false);
            Init_Module();
            //左边帮助面板 的text
            m_ObjControl.m_UiTextLeftHelp[0].text = "请从屏幕上方导航选择进入实验";
            m_ObjControl.m_UiTextLeftHelp[1].text = "根据操作提示进行操作";
            m_ObjControl.m_UiTextLeftHelp[2].text = "";
            m_ObjControl.m_UiTextLeftHelp[3].text = "";
            //相机控制
            Camera_Control(true, false, false, false);
        });
        m_ObjControl.m_BtnAssessOKM4.onClick.AddListener(delegate ()
        {
            m_ObjControl.m_UiTextLeftHelp[3].text = "1.将干燥箱打开\n2.干燥箱盖高亮显示";
            Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-51, 1, 1.8f), new Vector3(10, -90, 0), RotateMode.Fast);
            m_ObjControl.m_PanelAssessM4.SetActive(false);
        });
        //TestScript();
    }

    //测试代码
    public void TestScript()
    {
        //测试分解动画
        //m_ObjControl.m_UIOnClickPanel.SetActive(false);
        //m_ObjControl.m_UIHelpLeftPanel.SetActive(false);
        //m_ObjControl.m_Player.SetActive(false);
        //m_ObjControl.m_MainCamera.SetActive(true);
        //m_ObjControl.m_PanelM1Show.SetActive(true);
        //m_ObjControl.m_ObjShowM1.SetActive(true);
        //测试组装块
        //m_EnumModel.m_Moudule = Moudule.Moudule2;
        //BtnOnClick_M2_Next();
        //测试合成过程
        m_EnumModel.m_Moudule = Moudule.Moudule4;
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOff();
        }
        //模块1的 高亮对象的 boxcollider 关闭
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<BoxCollider>().enabled = false;
        }
        m_ObjControl.m_Player.SetActive(false);
        m_ObjControl.m_CameraM4.SetActive(true);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.将干燥箱打开\n2.干燥箱盖高亮显示";
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-51, 1, 1.8f), new Vector3(10, -90, 0), RotateMode.Fast);
        m_ObjControl.m_PanelAssessM4.SetActive(false);
    }

    void Update()
    {
        if (m_ObjControl.m_FadeShow)
        {
            UpdateColorAlpha();
        }
        if (m_EnumModel.m_Moudule == Moudule.Moudule1)
        {
            BtnOnClick_M1_UpdateEvent();
        }
        else if (m_EnumModel.m_Moudule == Moudule.Moudule2)
        {
            BtnOnClick_M2_UpdateEvent();
        }
        else if (m_EnumModel.m_Moudule == Moudule.Moudule3)
        {
            BtnOnClick_M3_UpdateEvent();
        }
        else if (m_EnumModel.m_Moudule == Moudule.Moudule4)
        {
            BtnOnClick_M4_UpdateEvent();
        }
    }

    //开场淡入淡出动画
    public void UpdateColorAlpha()
    {
        m_ObjControl.m_Sprite.enabled = true;
        //控制透明值变化
        if (m_ObjControl.m_FadeStatu == false)
        {
            m_ObjControl.m_Alpha += 0.5f * Time.deltaTime;
        }
        else if (m_ObjControl.m_FadeStatu)
        {
            m_ObjControl.m_Alpha -= 0.5f * Time.deltaTime;
        }
        //获取到图片的透明值
        Color ss = m_ObjControl.m_Sprite.color;
        ss.a = m_ObjControl.m_Alpha;
        //将更改过透明值的颜色赋值给图片
        m_ObjControl.m_Sprite.color = ss;
        //透明值等于的1的时候 转换成淡出效果
        if (m_ObjControl.m_Alpha > 1f)
        {
            m_ObjControl.m_Alpha = 1f;
            m_ObjControl.m_FadeStatu = true;
            switch (m_EnumModel.m_Moudule)
            {
                case Moudule.Moudule1:
                    BtnOnClick_M1_Next();
                    break;
                case Moudule.Moudule2:
                    BtnOnClick_M2_Next();
                    break;
                case Moudule.Moudule3:
                    BtnOnClick_M3_Next();
                    break;
                case Moudule.Moudule4:
                    StartCoroutine(BtnOnClick_M4_Next());
                    break;
                default:
                    break;
            }
        }
        //值为0的时候跳转场景
        else if (m_ObjControl.m_Alpha < 0)
        {
            //Debug.Log("结束");
            m_ObjControl.m_FadeShow = false;
            m_ObjControl.m_Sprite.enabled = false;
            m_ObjControl.m_FadeStatu = false;
        }
    }

    //初始化模块
    public void Init_Module()
    {
        //模块1的 高亮对象
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOff();
            m_ObjControl.m_HighlightObj[i].GetComponent<BoxCollider>().enabled = false;
        }
        //模块4的 箭头图片
        for (int i = 0; i < m_ObjControl.m_StudyImageTab.Length; i++)
        {
            m_ObjControl.m_StudyImageTab[i].SetActive(false);
        }
        //模块4的 高亮对象
        for (int i = 0; i < m_ObjControl.m_StudyObj.Length; i++)
        {
            m_ObjControl.m_StudyObj[i].GetComponent<HighlightableObject>().FlashingOff();
            m_ObjControl.m_StudyObj[i].GetComponent<BoxCollider>().enabled = false;
        }
        //模块4的 toggleUI
        m_ObjControl.m_ToggleStudyM4.SetActive(false);
        //模块4的 实心块
        m_ObjControl.m_ArrawZZK.SetActive(false);
        //模块4的 操作的动画对象
        StudyObj_Renew(m_ObjControl.m_StudyObj[0], new Vector3(-43.07077f, 0.4255903f, -0.1683785f), new Vector3(-57.811f, 90, -90));
        StudyObj_Renew(m_ObjControl.m_StudyObj[1], new Vector3(-43.02888f, 2.2375f, -7.3134f), new Vector3(0, 90, 261.71f));
        StudyObj_Renew(m_ObjControl.m_StudyObj[4], new Vector3(-43.96202f, 0.8393324f, -0.0492893f), new Vector3(0, 0, 0));
        StudyObj_Renew(m_ObjControl.m_StudyObj[5], new Vector3(-43.84007f, 0.8433679f, -0.060419f), new Vector3(0, 0, 0));
        StudyObj_Renew(m_ObjControl.m_StudyObj[7], new Vector3(-51.948f, 0.822f, 0.716f), new Vector3(90, 0, -89.29401f));
        StudyObj_Renew(m_ObjControl.m_StudyObj[8], new Vector3(-40.0374f, 0.8522f, -2.1954f), new Vector3(-90, 0, -92.134f));
        m_ObjControl.m_StudyObj[8].SetActive(true);
        StudyObj_Renew(m_ObjControl.m_StudyObj[12], new Vector3(-46.0795f, 1.218f, -6.2892f), new Vector3(-90, 0, -179.581f));
        m_ObjControl.m_StudyObj[12].SetActive(true);
        StudyObj_Renew(m_ObjControl.m_StudyObj[18], new Vector3(-45.41264f, 1.223159f, -6.486f), new Vector3(90, 90, 0));
        m_ObjControl.m_StudyObj[18].SetActive(true);
        StudyObj_Renew(m_ObjControl.m_HammerObjM4[0], new Vector3(2.928711f, -2.3f, 9.4f), new Vector3(90, 0, -90));
        StudyObj_Renew(m_ObjControl.m_HammerObjM4[1], new Vector3(-8.9f, -1.870644f, -2.694794f), new Vector3(0, 0, -180));
        StudyObj_Renew(m_ObjControl.m_HammerObjM4[2], new Vector3(2.341309f, 81.7f, -3.1f), new Vector3(0, 0, 0));
    }

    //模块4的 对象复原
    public void StudyObj_Renew(GameObject _obj,Vector3 _Ve3,Vector3 _Rot)
    {
        _obj.transform.localPosition = _Ve3;
        _obj.transform.localRotation = Quaternion.Euler(_Rot);
    }

    //相机控制
    public void Camera_Control(bool Camera1Bool, bool Camera2Bool, bool Camera3Bool, bool Camera4Bool)
    {
        //相机控制
        //人物相机
        m_ObjControl.m_Player.SetActive(Camera1Bool);
        //合成分解相机
        m_ObjControl.m_CameraM2.SetActive(Camera2Bool);
        //组装块组装相机
        m_ObjControl.m_MainCamera.SetActive(Camera3Bool);
        //漫游相机
        m_ObjControl.m_CameraM4.SetActive(Camera4Bool);
    }

    //模块1 开启过场动画 淡入淡出效果
    public void BtnOnClick_M1()
    {
        //淡入淡出效果
        m_EnumModel.m_Moudule = Moudule.Moudule1;
        m_ObjControl.m_FadeShow = true;
    }

    //模块1 初始化
    public void BtnOnClick_M1_Next()
    {
        //初始化参数
        Init_Module();
        //UI控制
        m_ObjControl.m_UiTextLeftHelp[0].text = "认知环节--导览工厂";
        m_ObjControl.m_UiTextLeftHelp[1].text = "1.请参观工厂的环境\n使用“WSAD”四个键进行移动\n鼠标右键进行视角旋转";
        m_ObjControl.m_UiTextLeftHelp[2].text = "";
        m_ObjControl.m_UiTextLeftHelp[3].text = "";
        //相机控制
        Camera_Control(true, false, false, false);
        //模块1的 高亮对象
        for (int i = 0; i < m_ObjControl.m_HighlightObj.Length; i++)
        {
            m_ObjControl.m_HighlightObj[i].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
            m_ObjControl.m_HighlightObj[i].GetComponent<BoxCollider>().enabled = true;
        }
    }

    //模块1 Update函数
    public void BtnOnClick_M1_UpdateEvent()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_ObjControl.m_CameraRayObj.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                if (gameobj.tag == "collider")
                {
                    switch (gameobj.name)
                    {
                        case "组116":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "六面顶压机";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "铰链式六面顶压机是我国于1965年自主研发的一款新型六面顶压机，该装置通过与油缸连为整体的铰链梁将六个油缸固定在一起。通过该设备能够获得稳定的高温高压条件，并可对腔体中的温度、电阻等参数进行原位测量。";
                            //m_ObjControl.m_BroweImage[0].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[8].GetComponent<HighlightableObject>().FlashingOff();
                            //单独显示
                            m_ObjControl.m_UIOnClickPanel.SetActive(false);
                            m_ObjControl.m_UIHelpLeftPanel.SetActive(false);
                            m_ObjControl.m_PanelM1Show.SetActive(true);
                            //相机控制
                            Camera_Control(false, false, true, false);
                            break;
                        case "组065":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "水泵";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "水泵是输送液体或使液体增压的机械。在六面顶压机合成金刚石实验中主要用于冷却锤面的作用";
                            //m_ObjControl.m_BroweImage[1].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[0].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组105":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "干燥箱";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "干燥箱是用来蒸发零件中的水分。在六面顶压机合成金刚石实验中主要用于蒸发零件中水分的作用";
                            //m_ObjControl.m_BroweImage[2].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[4].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组107":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "干燥箱";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "干燥箱是用来蒸发零件中的水分。在六面顶压机合成金刚石实验中主要用于蒸发零件中水分的作用";
                            //m_ObjControl.m_BroweImage[2].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[5].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组071":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "控制台";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "辅助控制机械设备，安装有六面顶压机监控系统，可以自定义设计温度压力曲线，实时监控六面顶压机的压力、温度、电压、电阻情况等";
                            //m_ObjControl.m_BroweImage[3].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[1].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组074":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "液压控制阀";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "液压阀是一种用压力油操作的自动化元件，它受配压阀压力油的控制，可用于远距离控制油、气、水管路系统的通断。在六面顶压机合成金刚石实验中，主要用于平衡液压作用 ";
                            //m_ObjControl.m_BroweImage[4].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[2].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组080":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "高压泵";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "高压泵是为高压旋喷水泥浆提供高压动力的设备。在六面顶压机合成金刚石实验中，主要用于升压作用";
                            //m_ObjControl.m_BroweImage[5].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[3].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "zhuozi002":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "工具台";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "放置各种操作工具的操作台";
                            //m_ObjControl.m_BroweImage[6].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[7].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组117":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "液压机";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "适用于金属材料的拉深、弯曲、翻边、冷挤、冲裁等工艺。在六面顶压机合成金刚石实验中，主要用于卸顶锤和装顶锤作用";
                            //m_ObjControl.m_BroweImage[6].color = m_ObjControl.m_ColorImage;
                            m_ObjControl.m_HighlightObj[6].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组102":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "电源总开关";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "在六面顶压机合成金刚石实验中，主要用于开启水泵开关作用";
                            m_ObjControl.m_HighlightObj[9].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        case "组103":
                            m_ObjControl.m_UiTextLeftHelp[0].text = "配电箱";
                            m_ObjControl.m_UiTextLeftHelp[1].text = "主要将开关设备、测量仪表、保护电器和辅助设备组装在封闭金属柜子中，是指挥供电线路中各种元器件合理分配电能的控制中心";
                            m_ObjControl.m_HighlightObj[10].GetComponent<HighlightableObject>().FlashingOff();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    //模块1 零件拆解功能
    public void BtnOnClick_M1_SplitEvent()
    {
        if (m_ObjControl.temp.Length == 0)
        {
            m_ObjControl.temp = new Vector3[m_ObjControl.m_SplitObj.Length];
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                m_ObjControl.temp[i] = m_ObjControl.m_SplitObj[i].transform.localPosition;
                //m_ObjControl.m_SplitObj[i].transform.localPosition = m_ObjControl.m_SplitObj[i].transform.Find("NewPosition").transform.localPosition;
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.m_SplitObj[i].transform.Find("NewPosition").transform.localPosition, 3f, false);
            }
        }
        else
        {
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.m_SplitObj[i].transform.Find("NewPosition").transform.localPosition, 3f, false);
            }
        }
    }

    //模块1 零件合成功能
    public void BtnOnClick_M1_MergeEvent()
    {
        if (m_ObjControl.temp.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                //m_ObjControl.m_SplitObj[i].transform.localPosition = temp[i];
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.temp[i], 3f, false);
            }
        }
    }

    //模块  零件分解功能 修改
    public void BtnOnClick_M1_SplitEvent_Modify()
    {
        if (m_ObjControl.m_SplitObjInitVe3.Length == 0)
        {
            m_ObjControl.m_SplitObjInitVe3 = new Vector3[m_ObjControl.m_SplitObj.Length];
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                m_ObjControl.m_SplitObjInitVe3[i] = m_ObjControl.m_SplitObj[i].transform.localPosition;
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.m_SplitObjLastVe3[i].localPosition, 3f, false);
            }
        }
        else
        {
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.m_SplitObjLastVe3[i].localPosition, 3f, false);
            }
        }
    }

    //模块1 零件合成功能 修改
    public void BtnOnClick_M1_MergeEvent_Modify()
    {
        if (m_ObjControl.m_SplitObjInitVe3.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < m_ObjControl.m_SplitObj.Length; i++)
            {
                m_ObjControl.m_SplitObj[i].transform.DOLocalMove(m_ObjControl.m_SplitObjInitVe3[i], 3f, false);
            }
        }
    }

    //模块1 返回主场景
    public void BtnOnClick_M1_BackEvent()
    {
        m_ObjControl.m_UIOnClickPanel.SetActive(true);
        m_ObjControl.m_UIHelpLeftPanel.SetActive(true);
        m_ObjControl.m_Player.SetActive(true);
        m_ObjControl.m_MainCamera.SetActive(false);
        m_ObjControl.m_PanelM1Show.SetActive(false);
    }

    //模块2 开启过场动画 淡入淡出效果
    public void BtnOnClick_M2()
    {
        //淡入淡出效果
        m_EnumModel.m_Moudule = Moudule.Moudule2;
        m_ObjControl.m_FadeShow = true;
    }

    //模块2 初始化
    public void BtnOnClick_M2_Next()
    {
        //初始化模块
        Init_Module();
        //UI控制
        m_ObjControl.m_UiTextLeftHelp[0].text = "请从屏幕上方导航选择进入实验";
        m_ObjControl.m_UiTextLeftHelp[1].text = "根据操作提示进行操作";
        m_ObjControl.m_UiTextLeftHelp[2].text = "";
        m_ObjControl.m_UiTextLeftHelp[3].text = "";
        //相机控制
        Camera_Control(false, false, true, false);
        //开启模块2
        m_ObjControl.m_Player.SetActive(false);
        m_ObjControl.m_MainCamera.SetActive(false);
        m_ObjControl.m_UIOnClickPanel.SetActive(false);
        m_ObjControl.m_UIHelpLeftPanel.SetActive(false);
        m_ObjControl.m_PanelAssembleCube.SetActive(true);
        m_ObjControl.m_CameraM2.SetActive(true);
        m_ObjControl.m_CameraM2.transform.rotation = Quaternion.Euler((new Vector3(40f, -180, 0)));
        m_ObjControl.m_DeskM2.SetActive(true);
        m_ObjControl.m_ObjPartM2.SetActive(true);
        //关闭
        m_ObjControl.m_AniObj.SetActive(false);
        m_ObjControl.m_ObjShowM2.SetActive(false);
        m_ObjControl.m_StepPartM2 = 0;
        //销毁对象
        GameObject[] m_Part = GameObject.FindGameObjectsWithTag("zzkPart");
        for (int i = 0; i < m_Part.Length; i++)
        {
            Destroy(m_Part[i]);
        }
        //零件高亮
        for (int i = 0; i < m_ObjControl.m_PartObjArryM2.Length; i++)
        {
            m_ObjControl.m_PartObjArryM2[i].GetComponent<HighlightableObject>().FlashingOn(1f);
        }
    }

    //模块2 Update函数
    public void BtnOnClick_M2_UpdateEvent()
    {
        if (m_ObjControl.m_IsInitMoveM2)
        {
            if (m_ObjControl.m_PartInitM2 == null)
            {
                return;
            }
            else
            {
                m_ObjControl.m_PartInitM2.transform.position = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 50, Input.mousePosition.y - 100, 1f));
                if (m_ObjControl.m_StepPartM2 == 10)
                {
                    m_ObjControl.m_PartInitM2.transform.position = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 200, Input.mousePosition.y - 200, 1f));
                }
                else if (m_ObjControl.m_StepPartM2 == 4)
                {
                    m_ObjControl.m_PartInitM2.transform.position = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 110, Input.mousePosition.y - 100, 1f));
                }
                else if (m_ObjControl.m_StepPartM2 == 6)
                {
                    m_ObjControl.m_PartInitM2.transform.position = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 150, Input.mousePosition.y - 100, 1f));
                }
                else if (m_ObjControl.m_StepPartM2 == 0 || m_ObjControl.m_StepPartM2 == 1)
                {
                    m_ObjControl.m_PartInitM2.transform.position = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 110, Input.mousePosition.y - 100, 1f));
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //根据点击到UI对象，获取UI对象名字
            //如果获取到对象的话   
            //if (OnePointColliderObject() != null)
            //{
            //    //给图片名字赋值
            //    string onClickName = OnePointColliderObject().name;
            //    //创建对象
            //    BtnNameOnClick_Modify(onClickName);
            //}
            //根据点击到3D对象，获取3D对象名字
            Ray ray = m_ObjControl.m_CameraM2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("collider2")))
            {
                BtnNameOnClick_Modify2(hit.transform.name);
            }
        }
    }

    //模块2 返回主场景功能修改
    public void BtnOnClick_M2_BackEvent_Modify()
    {
        //返回主场景
        m_ObjControl.m_Player.SetActive(true);
        m_ObjControl.m_MainCamera.SetActive(true);
        m_ObjControl.m_UIOnClickPanel.SetActive(true);
        m_ObjControl.m_UIHelpLeftPanel.SetActive(true);
        m_ObjControl.m_PanelAssembleCube.SetActive(false);
        m_ObjControl.m_CameraM2.SetActive(false);
        m_ObjControl.m_DeskM2.SetActive(false);
        m_ObjControl.m_AniObj.SetActive(false);
        m_ObjControl.m_StepPartM2 = 0;
        GameObject[] m_Part = GameObject.FindGameObjectsWithTag("zzkPart");
        for (int i = 0; i < m_Part.Length; i++)
        {
            Destroy(m_Part[i]);
        }
    }

    //模块2 重新开始功能修改
    public void BtnOnClick_M2_RestartEvent_Modify()
    {

        BtnOnClick_M2_Next();
    }

    //模块2 合成动画演示功能
    public void BtnOnClick_M2_AniEvent()
    {
        if (m_ObjControl.m_AssembleAni.isPlaying)
        {
            m_ObjControl.m_AssembleAni.Stop();
        }
        m_ObjControl.m_AniObj.SetActive(true);
        m_ObjControl.m_ObjShowM2.SetActive(false);
        m_ObjControl.m_AssembleAni.Play();
        m_ObjControl.m_CameraM2.transform.rotation = Quaternion.Euler((new Vector3(18.8f, -180, 0)));
        m_ObjControl.m_ObjPartM2.SetActive(false);
        m_ObjControl.m_StepPartM2 = 0;
        GameObject[] m_Part = GameObject.FindGameObjectsWithTag("zzkPart");
        for (int i = 0; i < m_Part.Length; i++)
        {
            Destroy(m_Part[i]);
        }
    }

    //模块2 组装块零件展示说明
    public void BtnOnClick_M2_ShowEvent()
    {
        m_ObjControl.m_AniObj.SetActive(false);
        m_ObjControl.m_ObjShowM2.SetActive(true);
        m_ObjControl.m_CameraM2.transform.rotation = Quaternion.Euler((new Vector3(18.8f, -180, 0)));
        m_ObjControl.m_ObjPartM2.SetActive(false);
        m_ObjControl.m_StepPartM2 = 0;
        GameObject[] m_Part = GameObject.FindGameObjectsWithTag("zzkPart");
        for (int i = 0; i < m_Part.Length; i++)
        {
            Destroy(m_Part[i]);
        }
    }

    //模块2 合成动画演示功能 判断零件是否激活
    public bool DetermineActivationState()
    {
        for (int i = 0; i < m_ObjControl.m_AssembleCube.Length; i++)
        {
            if (!m_ObjControl.m_AssembleCube[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    //模块2 点击不同对象 执行不同代码 
    public void BtnNameOnClick_Modify2(string name)
    {
        m_ObjControl.m_TextStepPartM2.text = "";
        if (m_ObjControl.m_IsInitM2 == false)
        {
            switch (name)
            {
                case "石墨柱":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[0]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.08159941f, 0.08159952f, 0.2656637f);
                    m_ObjControl.m_PartInitM2.name = "石墨柱";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    break;
                case "氧化镁管":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[1]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.05875159f, 0.05875167f, 0.2072768f);
                    m_ObjControl.m_PartInitM2.name = "氧化镁管";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    break;
                case "小氧化镁片":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[2]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.0794956f, 0.07949572f, 0.2588143f);
                    m_ObjControl.m_PartInitM2.name = "小氧化镁片";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberXYHMP += 1;
                    break;
                case "大氧化镁片":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[3]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.10039f, 0.10039f, 0.3268301f);
                    m_ObjControl.m_PartInitM2.name = "大氧化镁片";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberDYHMP += 1;
                    break;
                case "石墨管":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[4]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.08097457f, 0.0809747f, 0.2636294f);
                    m_ObjControl.m_PartInitM2.name = "石墨管";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    break;
                case "石墨片":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[5]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.12175f, 0.12175f, 0.3963902f);
                    m_ObjControl.m_PartInitM2.name = "石墨片";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberSMP += 1;
                    break;
                case "白云石管":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[6]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.103694f, 0.1036942f, 0.3375971f);
                    m_ObjControl.m_PartInitM2.name = "白云石管";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    break;
                case "钛片":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[7]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.1217524f, 0.1217525f, 0.3963896f);
                    m_ObjControl.m_PartInitM2.name = "钛片";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberTP += 1;
                    break;
                case "白云石环":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[8]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.103694f, 0.1036942f, 0.3375971f);
                    m_ObjControl.m_PartInitM2.name = "白云石环";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberBYSH += 1;
                    break;
                case "导电钢圈":
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[9]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.103694f, 0.1036942f, 0.3375971f);
                    m_ObjControl.m_PartInitM2.name = "导电钢圈";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    m_ObjControl.m_NumberDDGQ += 1;
                    break;
                case "白云石柱":
                    m_ObjControl.m_StepPartM2 = 10;
                    m_ObjControl.m_IsInitM2 = true;
                    m_ObjControl.m_PartInitM2 = Instantiate(m_ObjControl.m_PartObjPrefabM2[10]);
                    m_ObjControl.m_PartInitM2.SetActive(true);
                    m_ObjControl.m_PartInitM2.transform.localScale = new Vector3(0.09586866f, 0.08878365f, 0.09094153f);
                    m_ObjControl.m_PartInitM2.name = "白云石柱";
                    m_ObjControl.m_PartInitM2.tag = "zzkPart";
                    m_ObjControl.m_PartInitM2.transform.SetParent(m_ObjControl.m_PartInitParent);
                    m_ObjControl.m_IsInitMoveM2 = true;
                    break;
                default:
                    break;
            }
        }
        if (name == "BoxCollider_M2")
        {
            if (m_ObjControl.m_PartInitM2 == null)
            {
                return;
            }
            else
            {
                switch (m_ObjControl.m_StepPartM2)
                {
                    case 0:
                        if (m_ObjControl.m_PartInitM2.name == "石墨柱")
                        {
                            m_ObjControl.m_IsInitMoveM2 = false;
                            m_ObjControl.m_StepPartM2 = 1;
                            StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[0].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[0].transform.localRotation));
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 1:
                        if (m_ObjControl.m_PartInitM2.name == "氧化镁管")
                        {
                            m_ObjControl.m_IsInitMoveM2 = false;
                            m_ObjControl.m_StepPartM2 = 2;
                            StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[1].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[1].transform.localRotation));
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 2:
                        if (m_ObjControl.m_PartInitM2.name == "小氧化镁片")
                        {
                            if (m_ObjControl.m_NumberXYHMP == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[2].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[2].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberXYHMP == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[3].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[3].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberXYHMP == 3)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[4].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[4].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberXYHMP == 4)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[5].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[5].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 3;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 3:
                        if (m_ObjControl.m_PartInitM2.name == "大氧化镁片")
                        {
                            if (m_ObjControl.m_NumberDYHMP == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[6].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[6].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberDYHMP == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[7].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[7].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 4;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 4:
                        if (m_ObjControl.m_PartInitM2.name == "石墨管")
                        {
                            StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[8].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[8].transform.localRotation));
                            m_ObjControl.m_StepPartM2 = 5;
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 5:
                        if (m_ObjControl.m_PartInitM2.name == "石墨片")
                        {
                            if (m_ObjControl.m_NumberSMP == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[9].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[9].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberSMP == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[10].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[10].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 6;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 6:
                        if (m_ObjControl.m_PartInitM2.name == "白云石管")
                        {
                            m_ObjControl.m_IsInitMoveM2 = false;
                            m_ObjControl.m_StepPartM2 = 7;
                            StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[11].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[11].transform.localRotation));
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 7:
                        if (m_ObjControl.m_PartInitM2.name == "钛片")
                        {
                            if (m_ObjControl.m_NumberTP == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[12].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[12].transform.localRotation));
                            }
                            else if (m_ObjControl.m_NumberTP == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[13].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[13].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 8;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 8:
                        if (m_ObjControl.m_PartInitM2.name == "白云石环")
                        {
                            if (m_ObjControl.m_NumberBYSH == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[14].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[14].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 9;
                            }
                            else if (m_ObjControl.m_NumberBYSH == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[15].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[15].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 9;
                                m_ObjControl.m_NumberDDGQ = 1;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 9:
                        if (m_ObjControl.m_PartInitM2.name == "导电钢圈")
                        {
                            if (m_ObjControl.m_NumberDDGQ == 1)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[16].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[16].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 10;
                            }
                            else if (m_ObjControl.m_NumberDDGQ == 2)
                            {
                                StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[17].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[17].transform.localRotation));
                                m_ObjControl.m_StepPartM2 = 11;
                            }
                            m_ObjControl.m_IsInitMoveM2 = false;
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    case 10:
                        if (m_ObjControl.m_PartInitM2.name == "白云石柱")
                        {
                            m_ObjControl.m_IsInitMoveM2 = false;
                            m_ObjControl.m_StepPartM2 = 8;
                            m_ObjControl.m_NumberBYSH = 1;
                            StartCoroutine(AssembleAni(m_ObjControl.m_PartInitM2, m_ObjControl.m_PartObjTrans2M2[18].transform.localPosition, m_ObjControl.m_PartObjTrans2M2[18].transform.localRotation));
                        }
                        else
                        {
                            m_ObjControl.m_TextStepPartM2.text = "组装顺序不对，请重新选择";
                            m_ObjControl.m_PartInitM2.SetActive(false);
                            m_ObjControl.m_IsInitM2 = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //模块2 移动动画 2次移动功能
    IEnumerator AssembleAni(GameObject obj, Vector3 _moveV3, Quaternion _rotaV3, Vector3 _move2V3, Quaternion _rota2V3)
    {
        //第一次移动
        obj.transform.DOLocalMove(_moveV3, 3f, false);
        obj.transform.DOLocalRotateQuaternion(_rotaV3, 3f);
        yield return new WaitForSeconds(3f);
        //第二次移动
        obj.transform.DOLocalMove(_move2V3, 3f, false);
        obj.transform.DOLocalRotateQuaternion(_rota2V3, 3f);
        m_ObjControl.m_IsInitM2 = true;
    }

    //模块2 移动动画 1次移动功能
    IEnumerator AssembleAni(GameObject obj, Vector3 _moveV3, Quaternion _rotaV3)
    {
        //第一次移动
        obj.transform.DOLocalMove(_moveV3, 3f, false);
        obj.transform.DOLocalRotateQuaternion(_rotaV3, 3f);
        yield return new WaitForSeconds(3f);
        m_ObjControl.m_IsInitM2 = false;
    }

    //模块2 判断碰到的对象
    public GameObject OnePointColliderObject()
    {
        //存有鼠标或者触摸数据的对象
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //当前指针位置
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //射线命中之后的反馈数据
        List<RaycastResult> results = new List<RaycastResult>();
        //投射一条光线并返回所有碰撞
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //返回点击到的物体
        if (results.Count > 0)
            return results[0].gameObject;
        else
            return null;
    }

    //模块4 UI控制
    public void Step_Event()
    {
        m_ObjControl.m_UiTextLeftHelp[0].text = "合成工艺综合分析模块";
        m_ObjControl.m_UiTextLeftHelp[1].text = "1.请按照六面顶压机合成金刚石工艺顺序，进行实验\n2.观察和记录各个阶段的操作步骤";
        m_ObjControl.m_UiTextLeftHelp[2].text = "操作提示";
    }

    //模块3 开场动画
    public void BtnOnClick_M3()
    {
        //淡入淡出效果
        m_EnumModel.m_Moudule = Moudule.Moudule3;
        m_ObjControl.m_FadeShow = true;
    }

    //模块3 初始化
    public void BtnOnClick_M3_Next()
    {
        //初始化模块
        Init_Module();
        //相机控制
        Camera_Control(true, false, false, false);
        //UI控制
        m_ObjControl.m_UiTextLeftHelp[0].text = "请从屏幕上方导航选择进入实验";
        m_ObjControl.m_UiTextLeftHelp[1].text = "根据操作提示进行操作";
        m_ObjControl.m_UiTextLeftHelp[2].text = "";
        m_ObjControl.m_UiTextLeftHelp[3].text = "";
    }

    //模块3 Update函数
    public void BtnOnClick_M3_UpdateEvent()
    {

    }

    //模块4 开场动画
    public void BtnOnClick_M4()
    {
        m_EnumModel.m_Moudule = Moudule.Moudule4;
        m_ObjControl.m_FadeShow = true;
    }

    //模块4 初始化
    IEnumerator BtnOnClick_M4_Next()
    {
        //初始化模块
        Init_Module();
        //UI控制
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.请打开总开关\n2.开关高亮显示";
        Step_Event();
        //相机控制
        Camera_Control(false, false, false, true);
        //开启模块4 高亮对象1
        m_ObjControl.m_StudyObj[0].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        //右边导航栏
        m_ObjControl.m_StudyImageTab[0].SetActive(true);
        m_ObjControl.m_ToggleStudyM4.SetActive(true);
        //开启模块4的 boxcollider
        for (int i = 0; i < m_ObjControl.m_StudyObj.Length; i++)
        {
            m_ObjControl.m_StudyObj[i].GetComponent<BoxCollider>().enabled = true;
        }
        //相机移动动画
        m_ObjControl.m_CameraM4.transform.position = new Vector3(-43.2f, 1.2f, 8.8f);
        m_ObjControl.m_CameraM4.transform.localEulerAngles = new Vector3(0, 180, 0);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.5f, 1.2f, 3.4f), new Vector3(0, 180, 0), RotateMode.Fast);
        yield return new WaitForSeconds(3f);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-42f, 0.8f, 0.4f), new Vector3(7.5f, 267, 0), RotateMode.Fast);
    }

    //模块4 相机移动动画功能
    public void Move_Camera(GameObject _obj, Vector3 _vec, Vector3 _rota, RotateMode _mode)
    {
        _obj.transform.DOLocalMove(_vec, 3f, false);
        _obj.transform.DOLocalRotate(_rota, 3f, _mode);
    }

    //模块4 物体移动动画功能
    public void Move_Obj(GameObject _obj, Vector3 _vec, Vector3 _rota, RotateMode _mode, float _duration)
    {
        _obj.transform.DOLocalMove(_vec, _duration, false);
        _obj.transform.DOLocalRotate(_rota, _duration, _mode);
    }

    //模块4 主机开关动画
    IEnumerator OnOffAni_M4()
    {
        Move_Camera(m_ObjControl.m_StudyObj[0], new Vector3(-43.07077f, 0.4391f, -0.168f), new Vector3(-128.247f, 90, -90), RotateMode.Fast);
        yield return new WaitForSeconds(2f);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.432f, 2.2f, -6f), new Vector3(0, -184, 0), RotateMode.Fast);
    }

    //模块4 水泵开关动画
    IEnumerator OnOff2Ani_M4()
    {
        Move_Camera(m_ObjControl.m_StudyObj[1], new Vector3(-43.02888f, 2.2446573f, -7.325176f), new Vector3(0, 90, 189.977f), RotateMode.Fast);
        yield return new WaitForSeconds(2f);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-46.2f, 1.2f, 1.8f), new Vector3(10.5f, -179, 0), RotateMode.Fast);
    }

    //模块4 泄压开关动画
    IEnumerator XieyaAni_M4()
    {
        m_ObjControl.m_Progress.SetActive(true);
        yield return new WaitForSeconds(4f);
        m_ObjControl.m_StudyObj[6].GetComponent<HighlightableObject>().FlashingOff();
        m_ObjControl.m_Progress.SetActive(false);
        m_ObjControl.m_StudyObj[4].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        m_ObjControl.m_StudyStepManual = 1;
        m_ObjControl.m_StudyObj[4].GetComponent<BoxCollider>().enabled = true;
    }

    //模块4 手动开关动画
    IEnumerator SdAni_M4()
    {
        m_ObjControl.m_StudyObj[4].GetComponent<HighlightableObject>().FlashingOff();
        Move_Camera(m_ObjControl.m_StudyObj[4], new Vector3(-43.96202f, 0.8393324f, -0.0492893f), new Vector3(0, 0, 0), RotateMode.Fast);
        yield return new WaitForSeconds(4f);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.请拿起刀片，然后去检查前锤面是否有裂纹\n2.刀片高亮显示";
        m_ObjControl.m_StudyObj[11].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        m_ObjControl.m_StudyImageTab[3].SetActive(false);
        m_ObjControl.m_StudyImageTab[4].SetActive(true);
        Step_Event();
        m_ObjControl.m_StudyObj[4].GetComponent<BoxCollider>().enabled = true;
        m_ObjControl.m_StudyObj[5].GetComponent<BoxCollider>().enabled = true;
        m_ObjControl.m_StudyObj[9].SetActive(false);
        m_ObjControl.m_ObjMoveIsM4 = true;
        m_ObjControl.m_StudyObj[11].GetComponent<BoxCollider>().enabled = true;
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-40.74f, 1.4f, -2.24f), new Vector3(23, 48, 0), RotateMode.Fast);
    }

    //模块4 Update函数
    public void BtnOnClick_M4_UpdateEvent()
    {
        if (m_ObjControl.m_ObjMoveIsM4)
        {
            m_ObjControl.m_StudyObj[12].transform.position = m_ObjControl.m_CameraM4.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        if (m_ObjControl.m_ObjMoveAssembleIsM4)
        {
            m_ObjControl.m_StudyObj[8].transform.position = m_ObjControl.m_CameraM4.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        if (m_ObjControl.m_ObjMoveGloveM4)
        {
            m_ObjControl.m_StudyObj[18].transform.position = m_ObjControl.m_CameraM4.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_ObjControl.m_CameraM4.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                if (gameobj.tag == "collider4")
                {
                    switch (gameobj.name)
                    {
                        case "m_onoff"://1.开启总开关
                            if (m_ObjControl.m_StudyStepOnff == 0)
                            {
                                //总开关
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.打开水泵，使水泵里面的水从锤体流过，进行减温\n2.水泵开关高亮显示";
                                //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                                m_ObjControl.m_StudyObj[0].GetComponent<HighlightableObject>().FlashingOff();
                                m_ObjControl.m_StudyObj[1].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[0].SetActive(false);
                                m_ObjControl.m_StudyImageTab[1].SetActive(true);
                                //m_ObjControl.m_StudyStepOnff = 1;
                                Step_Event();
                                StartCoroutine(OnOffAni_M4());
                            }
                            else if (m_ObjControl.m_StudyStepOnff == 10)
                            {
                                m_ObjControl.m_UiTextLeftHelp[3].text = "成功完成工艺综合分析模块学习";
                                //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                                m_ObjControl.m_StudyObj[0].GetComponent<HighlightableObject>().FlashingOff();
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[10].SetActive(false);
                                m_ObjControl.m_StudyStepOnff = 2;
                                Step_Event();
                            }
                            break;
                        case "m_onoff3"://2.打开水泵
                                        //水泵
                            m_ObjControl.m_UiTextLeftHelp[3].text = "1.检查泄流阀是否拧紧\n2.泄流阀高亮显示";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[1].GetComponent<HighlightableObject>().FlashingOff();
                            m_ObjControl.m_StudyObj[2].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[1].SetActive(false);
                            m_ObjControl.m_StudyImageTab[2].SetActive(true);
                            Step_Event();
                            StartCoroutine(OnOff2Ani_M4());
                            break;
                        case "m_fm1"://3.检查液压控制阀
                                     //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[2].GetComponent<HighlightableObject>().FlashingOff();
                            m_ObjControl.m_StudyObj[3].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[1].SetActive(false);
                            m_ObjControl.m_StudyImageTab[2].SetActive(true);
                            Step_Event();
                            break;
                        case "m_fm2":
                            //溢流阀
                            m_ObjControl.m_UiTextLeftHelp[3].text = "1.将自动调成手动\n2.自动/手动按钮高亮显示";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[3].GetComponent<HighlightableObject>().FlashingOff();
                            m_ObjControl.m_StudyObj[4].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[2].SetActive(false);
                            m_ObjControl.m_StudyImageTab[3].SetActive(true);
                            Step_Event();
                            Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.99f, 1.2f, 1.44f), new Vector3(12.985f, 180, 0), RotateMode.Fast);
                            break;
                        case "m_sd"://4.合成前流程
                            if (m_ObjControl.m_StudyStepManual == 0)
                            {
                                //手动
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.旋转总停开关\n2.总停开关按钮高亮显示";
                                //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                                m_ObjControl.m_StudyObj[4].GetComponent<HighlightableObject>().FlashingOff();
                                m_ObjControl.m_StudyObj[5].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[2].SetActive(false);
                                m_ObjControl.m_StudyImageTab[3].SetActive(true);
                                Step_Event();
                                m_ObjControl.m_StudyObj[4].GetComponent<BoxCollider>().enabled = false;
                                Move_Camera(m_ObjControl.m_StudyObj[4], new Vector3(-43.96202f, 0.8393324f, -0.0492893f), new Vector3(0, 0, 90), RotateMode.Fast);
                            }
                            else if (m_ObjControl.m_StudyStepManual == 1)
                            {
                                StartCoroutine(SdAni_M4());
                            }
                            break;
                        case "m_jt":
                            if (m_ObjControl.m_StudyStepStop == 0)
                            {
                                //总停
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.点击手动泄压，45秒后自动回程\n2.泄压按钮高亮显示";
                                //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                                m_ObjControl.m_StudyObj[5].GetComponent<HighlightableObject>().FlashingOff();
                                m_ObjControl.m_StudyObj[6].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[2].SetActive(false);
                                m_ObjControl.m_StudyImageTab[3].SetActive(true);
                                m_ObjControl.m_StudyStepStop = 1;
                                Step_Event();
                                m_ObjControl.m_StudyObj[5].GetComponent<BoxCollider>().enabled = false;
                                Move_Camera(m_ObjControl.m_StudyObj[5], new Vector3(-43.84007f, 0.8433679f, -0.060419f), new Vector3(0, 0, 90), RotateMode.Fast);
                            }
                            else if (m_ObjControl.m_StudyStepStop == 1)
                            {
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.关闭总开关\n2.总开关按钮高亮显示";
                                //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                                m_ObjControl.m_StudyObj[5].GetComponent<HighlightableObject>().FlashingOff();
                                m_ObjControl.m_StudyObj[0].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[9].SetActive(false);
                                m_ObjControl.m_StudyImageTab[10].SetActive(true);
                                m_ObjControl.m_StudyStepStop = 2;
                                Step_Event();
                            }
                            break;
                        case "m_xieya":
                            //泄压
                            //m_ObjControl.m_UiTextLeftHelp[3].text = "1.检查前锤面是否有裂纹\n2.前锤面高亮显示";
                            //m_ObjControl.m_UiTextLeftHelp[3].text = "1.请拿起刀片，然后去检查前锤面是否有裂纹\n2.刀片高亮显示";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            //m_ObjControl.m_StudyObj[6].GetComponent<HighlightableObject>().FlashingOff();
                            //m_ObjControl.m_StudyObj[11].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //上一个图片关闭 下一个图片开启
                            //m_ObjControl.m_StudyImageTab[3].SetActive(false);
                            //m_ObjControl.m_StudyImageTab[4].SetActive(true);
                            //Step_Event();
                            //m_ObjControl.m_StudyObj[4].GetComponent<BoxCollider>().enabled = true;
                            //m_ObjControl.m_StudyObj[5].GetComponent<BoxCollider>().enabled = true;
                            //m_ObjControl.m_StudyObj[9].SetActive(false);
                            //m_ObjControl.m_ObjMoveIsM4 = true;
                            //m_ObjControl.m_StudyObj[11].GetComponent<BoxCollider>().enabled = true;
                            StartCoroutine(XieyaAni_M4());
                            //Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-40.8f, 1.2f, -2.3f), new Vector3(23, -312, 0), RotateMode.Fast);
                            break;
                        //case "m_dp":
                        //    Cursor.SetCursor(m_ObjControl.cursorTexture[1], m_ObjControl.hotSpot, m_ObjControl.cursorMode);
                        //    //前锤面
                        //    m_ObjControl.m_UiTextLeftHelp[3].text = "1.请用刀片检查锤面是否有裂纹\n2.锤面高亮显示";
                        //    //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                        //    m_ObjControl.m_StudyObj[7].GetComponent<HighlightableObject>().FlashingOff();
                        //    m_ObjControl.m_StudyObj[8].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                        //    //上一个图片关闭 下一个图片开启
                        //    m_ObjControl.m_StudyImageTab[3].SetActive(false);
                        //    m_ObjControl.m_StudyImageTab[4].SetActive(true);
                        //    m_ObjControl.m_StudyObj[16].SetActive(false);
                        //    Step_Event();
                        //    break;
                        case "m_frontcm"://5.检查锤面
                            //Cursor.SetCursor(null, Vector2.zero, m_ObjControl.cursorMode);
                            //前锤面
                            //m_ObjControl.m_UiTextLeftHelp[3].text = "1.将干燥箱打开\n2.干燥箱盖高亮显示";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[11].GetComponent<HighlightableObject>().FlashingOff();
                            m_ObjControl.m_StudyObj[7].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[4].SetActive(false);
                            m_ObjControl.m_StudyImageTab[5].SetActive(true);
                            Step_Event();
                            m_ObjControl.m_ObjMoveIsM4 = false;
                            m_ObjControl.m_StudyObj[11].GetComponent<BoxCollider>().enabled = false;
                            //锤面检查
                            m_ObjControl.m_PanelAssessM4.SetActive(true);
                            int i = Random.Range(0, 3);
                            switch (i)
                            {
                                case 0:
                                    m_ObjControl.m_TxtAssessM4.text = "有震动感";
                                    break;
                                case 1:
                                    m_ObjControl.m_TxtAssessM4.text = "很流畅";
                                    break;
                                case 2:
                                    m_ObjControl.m_TxtAssessM4.text = "不平滑";
                                    break;
                                case 3:
                                    m_ObjControl.m_TxtAssessM4.text = "没震动感";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "m_gz":
                            //6.取组装块
                            //干燥箱盖子
                            m_ObjControl.m_UiTextLeftHelp[3].text = "1.将组装块取出\n2.组装块高亮显示";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[7].GetComponent<HighlightableObject>().FlashingOff();
                            m_ObjControl.m_StudyObj[8].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            //m_ObjControl.m_StudyObj[7].SetActive(false);
                            Move_Camera(m_ObjControl.m_StudyObj[7], new Vector3(-51.948f, 0.822f, 0.716f), new Vector3(90, 0, -212.93f), RotateMode.FastBeyond360);
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[4].SetActive(false);
                            m_ObjControl.m_StudyImageTab[5].SetActive(true);
                            Step_Event();
                            break;
                        case "m_zzk":
                            //Cursor.SetCursor(m_ObjControl.cursorTexture[3], m_ObjControl.hotSpot, m_ObjControl.cursorMode);
                            //取组装块
                            m_ObjControl.m_UiTextLeftHelp[3].text = "1.将组装块放置六面顶压机中心位置";
                            //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                            m_ObjControl.m_StudyObj[8].GetComponent<HighlightableObject>().FlashingOff();
                            //上一个图片关闭 下一个图片开启
                            m_ObjControl.m_StudyImageTab[5].SetActive(false);
                            m_ObjControl.m_StudyImageTab[6].SetActive(true);
                            Step_Event();
                            m_ObjControl.m_ObjMoveAssembleIsM4 = true;
                            m_ObjControl.m_StudyObj[9].SetActive(true);
                            Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-40.74f, 1.4f, -2.24f), new Vector3(23, 48, 0), RotateMode.Fast);
                            break;
                        case "m_fzzzk"://7.放置组装块
                            if (m_ObjControl.m_StudyStepPlace == 0)
                            {
                                //Cursor.SetCursor(null, Vector2.zero, m_ObjControl.cursorMode);
                                //放置组装块
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.设置温度和压力曲线\n2.保存工艺\n3.下载工艺";
                                //m_ObjControl.m_UICraftSetPanel.SetActive(true);
                                //组装块显示
                                m_ObjControl.m_StudyObj[13].SetActive(true);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[6].SetActive(false);
                                m_ObjControl.m_StudyImageTab[7].SetActive(true);
                                //m_ObjControl.m_StudyStepPlace = 2;
                                Step_Event();
                                m_ObjControl.m_ObjMoveAssembleIsM4 = false;
                                m_ObjControl.m_StudyObj[8].SetActive(false);
                                //Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.5f, 1.05f, 0.9f), new Vector3(10, -180, 0), RotateMode.Fast);
                                //StartCoroutine(SyntheticProcessVideoPlay());
                                StartCoroutine(SyntheticProcessVideoPlay_Modify());
                                //显示UI界面，进行设置工艺曲线，设置完成保存下载之后，模拟合成
                            }
                            else if (m_ObjControl.m_StudyStepPlace == 1)
                            {
                                //Cursor.SetCursor(null, Vector2.zero, m_ObjControl.cursorMode);
                                //放置实心块
                                //实心块显示
                                m_ObjControl.m_UiTextLeftHelp[3].text = "1.压力升到30拍急停\n2.急停按钮高亮显示";
                                m_ObjControl.m_StudyObj[5].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                                //上一个图片关闭 下一个图片开启
                                m_ObjControl.m_StudyImageTab[8].SetActive(false);
                                m_ObjControl.m_StudyImageTab[9].SetActive(true);
                                m_ObjControl.m_StudyStepPlace = 2;
                                Step_Event();
                            }
                            else if (m_ObjControl.m_StudyStepPlace == 2)
                            {
                                StartCoroutine(CraftShowAni());
                            }
                            break;
                        case "m_kj"://9.模拟合成
                            //顶锤空进的过程
                            StartCoroutine(HammerEmptyIntoAni());
                            break;
                        case "m_cy":
                            //充液
                            StartCoroutine(LookPanelLineAni());
                            break;
                        //case "m_sxk"://10.放置实心块
                        //    Cursor.SetCursor(m_ObjControl.cursorTexture[2], m_ObjControl.hotSpot, m_ObjControl.cursorMode);
                        //    //实心块
                        //    m_ObjControl.m_UiTextLeftHelp[3].text = "1.将实心块放置到六面顶压机中心位置\n2.实心块高亮显示";
                        //    //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
                        //    m_ObjControl.m_StudyObj[19].GetComponent<HighlightableObject>().FlashingOff();
                        //    //上一个图片关闭 下一个图片开启
                        //    m_ObjControl.m_StudyImageTab[7].SetActive(false);
                        //    m_ObjControl.m_StudyImageTab[8].SetActive(true);
                        //    Step_Event();
                        //    break;
                        case "m_st":
                            //物体跟随鼠标移动
                            m_ObjControl.m_ObjMoveGloveM4 = true;
                            m_ObjControl.m_StudyObj[18].GetComponent<HighlightableObject>().FlashingOff();
                            //UI控制
                            m_ObjControl.m_UiTextLeftHelp[3].text = "1.请拿起合成之后的组装块\n2.组装块高亮显示";
                            Step_Event();
                            m_ObjControl.m_StudyObj[13].SetActive(true);
                            m_ObjControl.m_StudyObj[13].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
                            m_ObjControl.m_StudyStepPlace = 2;
                            Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-40.74f, 1.4f, -2.24f), new Vector3(23, 48, 0), RotateMode.Fast);
                            break;
                        case "m_zzkhc":
                            StartCoroutine(LookZZKAni());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    //模块4 播放视频功能
    IEnumerator SyntheticProcessVideoPlay()
    {
        m_ObjControl.m_StudyObj[14].SetActive(false);
        m_ObjControl.m_StudyObj[15].SetActive(true);
        yield return new WaitForSeconds(4f);
        m_ObjControl.m_VideoM4.Play();
        //上一个图片关闭 下一个图片开启
        m_ObjControl.m_StudyImageTab[7].SetActive(false);
        m_ObjControl.m_StudyImageTab[8].SetActive(true);
        yield return new WaitForSeconds(10f);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-41.708f, 1.669f, -3.037f), new Vector3(0, 56.495f, 0), RotateMode.Fast);
        //上一个图片关闭 下一个图片开启
        m_ObjControl.m_StudyImageTab[8].SetActive(false);
        m_ObjControl.m_StudyImageTab[9].SetActive(true);
        yield return new WaitForSeconds(4f);
        Move_Camera(m_ObjControl.m_StudyObj[8], new Vector3(-41.015f, 1.504f, -3.002f), new Vector3(-90, 0, 90), RotateMode.Fast);
        yield return new WaitForSeconds(4f);
        m_ObjControl.m_UICraftSetPanel.SetActive(true);
    }

    //模块4 播放视频功能修改
    IEnumerator SyntheticProcessVideoPlay_Modify()
    {
        yield return new WaitForSeconds(3f);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.5f, 1.05f, 0.9f), new Vector3(10, -180, 0), RotateMode.Fast);
        yield return new WaitForSeconds(3f);
        m_ObjControl.m_PanelSetLine.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_ObjControl.m_PanelSetLine.SetActive(false);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.手动点击“空进”\n2.“空进”按钮高亮显示3.等待顶锤空进结束";
        m_ObjControl.m_StudyObj[16].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        m_ObjControl.m_StudyImageTab[7].SetActive(false);
        m_ObjControl.m_StudyImageTab[8].SetActive(true);
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-41.63f, 2.038f, -1.582f), new Vector3(43.805f, 90, 0), RotateMode.Fast);
    }

    //模块4 顶锤空进过程
    IEnumerator HammerEmptyIntoAni()
    {
        //观察顶锤的视角移动
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-40.74f, 1.4f, -2.24f), new Vector3(23, 48, 0), RotateMode.Fast);
        yield return new WaitForSeconds(3f);
        Move_Obj(m_ObjControl.m_HammerObjM4[0], new Vector3(2.9228711f, -2.285461f, 4.015472f), new Vector3(90, 0, -90), RotateMode.Fast, 3f);
        Move_Obj(m_ObjControl.m_HammerObjM4[1], new Vector3(-3.87207f, -1.870544f, -2.694794f), new Vector3(0, 0, -180), RotateMode.Fast, 4f);
        Move_Obj(m_ObjControl.m_HammerObjM4[2], new Vector3(2.341309f, 75.87543f, -3.05928f), new Vector3(0, 0, 0), RotateMode.Fast, 5f);
        yield return new WaitForSeconds(5f);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.点击六面顶压机上面的“充液”按钮\n2.“充液”按钮高亮显示";
        //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
        m_ObjControl.m_StudyObj[16].GetComponent<HighlightableObject>().FlashingOff();
        m_ObjControl.m_StudyObj[17].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
        Step_Event();
        //正对压机按钮控制台
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-41.63f, 2.038f, -1.582f), new Vector3(43.805f, 90, 0), RotateMode.Fast);
    }

    //模块4 充液之后的观察屏幕曲线
    IEnumerator LookPanelLineAni()
    {
        Move_Obj(m_ObjControl.m_HammerObjM4[0], new Vector3(2.928711f, -2.3f, 9.4f), new Vector3(90, 0, -90), RotateMode.Fast, 3f);
        Move_Obj(m_ObjControl.m_HammerObjM4[1], new Vector3(-8.9f, -1.870644f, -2.694794f), new Vector3(0, 0, -180), RotateMode.Fast, 3f);
        Move_Obj(m_ObjControl.m_HammerObjM4[2], new Vector3(2.341309f, 81.7f, -3.1f), new Vector3(0, 0, 0), RotateMode.Fast, 3f);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.观察六面顶压机监控软件的操作界面的曲线\n2.出现异常及时拍急停";
        //上一个高亮对象关闭高亮 下一个高亮对象开启高亮
        m_ObjControl.m_StudyObj[17].GetComponent<HighlightableObject>().FlashingOff();
        Step_Event();
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-43.5f, 1.05f, 0.9f), new Vector3(10, -180, 0), RotateMode.Fast);
        yield return new WaitForSeconds(3f);
        m_ObjControl.m_StudyObj[15].SetActive(true);
        m_ObjControl.m_StudyObj[19].SetActive(false);
        m_ObjControl.m_VideoM4.Play();
        yield return new WaitForSeconds(15f);
        m_ObjControl.m_VideoM4.Stop();
        m_ObjControl.m_StudyObj[15].SetActive(false);
        m_ObjControl.m_StudyObj[19].SetActive(true);
        //上一个图片关闭 下一个图片开启
        m_ObjControl.m_StudyImageTab[8].SetActive(false);
        m_ObjControl.m_StudyImageTab[9].SetActive(true);
        //使用手套拿出实心块
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.请拿起手套\n2.使用手套拿出合成之后的组装块\n3.手套高亮显示";
        Step_Event();
        Move_Camera(m_ObjControl.m_CameraM4, new Vector3(-45.8f, 2.14f, -4.92f), new Vector3(28.765f, 180, 0), RotateMode.Fast);
        m_ObjControl.m_StudyObj[18].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
    }

    //模块4 拿起手套之后取出组装块动画
    IEnumerator CraftShowAni()
    {
        m_ObjControl.m_ObjMoveGloveM4 = false;
        m_ObjControl.m_StudyObj[13].GetComponent<HighlightableObject>().FlashingOff();
        m_ObjControl.m_StudyObj[18].transform.SetParent(m_ObjControl.m_StudyObj[13].transform);
        m_ObjControl.m_StudyObj[18].transform.localPosition = new Vector3(-4.318398f, 0.5570899f, -2.23418f);
        m_ObjControl.m_StudyObj[18].transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 123.658f));
        Move_Obj(m_ObjControl.m_StudyObj[13], new Vector3(-40.45f, 1.136f, -2.615f), new Vector3(-90, 0, 90), RotateMode.Fast, 3f);
        yield return new WaitForSeconds(3f);
        m_ObjControl.m_UiTextLeftHelp[3].text = "1.请用锯条将合成的实心块的正面画出箭头\n2.箭头的面正对着前锤3.实心块高亮显示";
        m_ObjControl.m_StudyObj[13].GetComponent<HighlightableObject>().FlashingOn(Color.green, Color.gray, 1f);
    }

    //模块4 观察组装块
    IEnumerator LookZZKAni()
    {
        m_ObjControl.m_StudyObj[13].GetComponent<HighlightableObject>().FlashingOff();
        m_ObjControl.m_ArrawZZK.SetActive(true);
        m_ObjControl.m_ArrawZZK.transform.localPosition = m_ObjControl.m_StudyObj[13].transform.localPosition;
        m_ObjControl.m_ArrawZZK.transform.localRotation = m_ObjControl.m_StudyObj[13].transform.localRotation;
        m_ObjControl.m_StudyObj[13].SetActive(false);
        yield return new WaitForSeconds(5f);
        m_ObjControl.m_UICraftSetPanel.SetActive(true);
    }

    //实现对象隐藏与关闭功能
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
}
