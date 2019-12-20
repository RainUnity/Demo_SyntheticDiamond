using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class M2ScenesObj_Control : MonoBehaviour
{
    //按钮 模块1
    public Button m_BtnMoudule1;
    //按钮 模块2
    public Button m_BtnMoudule2;
    //按钮 模块3
    public Button m_BtnMoudule3;
    //按钮 模块4
    public Button m_BtnMoudule4;
    //按钮 工艺开始按钮
    public Button m_BtnCraftStart;
    //主摄像机
    public Camera m_CameraRayObj;
    //模块1 点击模型出现的UI
    public GameObject m_UIOnClickPanel;
    //左边帮助Panel
    public GameObject m_UIHelpLeftPanel;
    //右边UI 颜色变化值
    public Color m_ColorImage;
    //设置的图片
    public Image m_Sprite;
    //透明值
    [HideInInspector]
    public float m_Alpha;
    //透明值变化标识
    [HideInInspector]
    public bool m_FadeStatu = false;
    //执行淡入淡出效果
    [HideInInspector]
    public bool m_FadeShow = false;
    //步数计数
    [HideInInspector]
    public int m_StudyStepOnff = 0;
    [HideInInspector]
    public int m_StudyStepPlace = 0;
    [HideInInspector]
    public int m_StudyStepStop = 0;
    [HideInInspector]
    public int m_StudyStepManual = 0;
    //参数设置面板
    public GameObject m_UICraftSetPanel;
    //拆分按钮
    public Button m_BtnSplit;
    //合并按钮
    public Button m_BtnMerge;
    //返回按钮
    public Button m_BtnBack;
    //组装块 返回按钮
    public Button m_BtnBackM2;
    //组装块 重新开始按钮
    public Button m_BtnRestart;
    //组装块 组装演示按钮
    public Button m_BtnAniM2;
    //组装块 零件说明按钮
    public Button m_BtnShowM2;
    //组装块 零件说明
    public GameObject m_ObjShowM2;
    //组装块 组装零件
    public GameObject m_ObjPartM2;
    //组装机 父对象
    public Transform m_PartInitParent;

    //分解动画演示
    public GameObject m_Player;
    public GameObject m_MainCamera;
    public GameObject m_PanelM1Show;
    //组装块组装
    public GameObject m_PanelAssembleCube;
    public GameObject m_CameraM2;
    public GameObject m_DeskM2;
    public Animation m_AssembleAni;
    public GameObject m_AniObj;
    public GameObject m_TxtHelpM2;
    //要操作的组装块 零件
    [HideInInspector]
    public GameObject m_PartInitM2;
    [HideInInspector]
    public bool m_IsInitM2 = true;
    [HideInInspector]
    public bool m_IsInitMoveM2 = false;
    [HideInInspector]
    public int m_StepPartM2 = 0;
    public Text m_TextStepPartM2;
    //组装块 零件 数量标识
    [HideInInspector]
    public int m_NumberXYHMP = 0;
    [HideInInspector]
    public int m_NumberDYHMP = 0;
    [HideInInspector]
    public int m_NumberTP = 0;
    [HideInInspector]
    public int m_NumberSMP = 0;
    [HideInInspector]
    public int m_NumberBYSH = 0;
    [HideInInspector]
    public int m_NumberDDGQ = 0;
    //模式4
    public GameObject m_CameraM4;
    [HideInInspector]
    public bool m_ObjMoveIsM4 = false;
    [HideInInspector]
    public bool m_ObjMoveAssembleIsM4 = false;
    [HideInInspector]
    public bool m_ObjMoveGloveM4 = false;
    public VideoPlayer m_VideoM4;
    //结果分析确定按钮
    public Button m_BtnAnalyzeOkM4;
    //检查锤面
    public GameObject m_PanelAssessM4;
    public Button m_BtnAssessOKM4;
    public Text m_TxtAssessM4;
    //泄压进度条
    public GameObject m_Progress;
    //设置温度压力曲线UI界面
    public GameObject m_PanelSetLine;
    //模式4的 toggleUI
    public GameObject m_ToggleStudyM4;
    //模式4的 画箭头的实心块
    public GameObject m_ArrawZZK;

    //模块1 帮助UI文字
    public Text[] m_UITextHelp;
    //模块1 左下角帮助UI文字
    public Text[] m_UiTextLeftHelp;
    //导航UI
    public Image[] m_BroweImage;
    //高亮物体
    public GameObject[] m_HighlightObj;
    //工艺模块操作对象
    public GameObject[] m_StudyObj;
    //工艺模块箭头图片
    public GameObject[] m_StudyImageTab;
    //拆分对象
    public GameObject[] m_SplitObj;
    //模块1 拆分对象 初始坐标 以及 拆分坐标
    [HideInInspector]
    public Vector3[] m_SplitObjInitVe3;
    public Transform[] m_SplitObjLastVe3;
    //组装块对象
    public GameObject[] m_AssembleCube;
    //按钮对象 加号
    public Button[] m_BtnAddClassify;
    //按钮对象 减号
    public Button[] m_BtnSubClassify;
    //分解动画需要用到的位置数组
    [HideInInspector]
    public Vector3[] temp;
    //组装块 提示文本
    public Text[] m_txtObjArray;
    //组装块 零件
    public GameObject[] m_PartObjArryM2;
    //组装块 零件 预制体
    public GameObject[] m_PartObjPrefabM2;
    //组装块 零件 终点位置
    public GameObject[] m_PartObjTrans1M2;
    public GameObject[] m_PartObjTrans2M2;
    //模块4 空进的三个顶锤
    public GameObject[] m_HammerObjM4;
    
    //鼠标图标切换
    public Texture2D[] cursorTexture;
    [HideInInspector]
    public CursorMode cursorMode = CursorMode.Auto;
    [HideInInspector]
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        m_Sprite.enabled = false;
    }
}