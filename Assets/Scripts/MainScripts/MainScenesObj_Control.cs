using UnityEngine;
using UnityEngine.UI;

public class MainScenesObj_Control : MonoBehaviour
{
    public GameObject[] m_HighlightObj;
    public GameObject[] m_UIPanel;
    public GameObject[] m_UIToggle;
    public Text[] m_UITextHelpLeft;
    public Text[] m_UITextHelpRight;
    public Button[] m_UIButton;
    public Camera m_Camera;
    public Image[] m_BroweImage;
    public Color m_ColorImage;
    [HideInInspector]
    public int m_StudyStep = 0;
    public GameObject[] m_StudyObj;
    public GameObject[] m_StudyImageTab;
    public Image[] m_StudyImageBg;

    void Awake()
    {
        for (int i = 0; i < m_UIPanel.Length; i++)
        {
            m_UIPanel[i].SetActive(false);
        }
        for (int i = 0; i < m_UIToggle.Length; i++)
        {
            m_UIToggle[i].SetActive(false);
        }
        for (int i = 0; i < m_StudyImageTab.Length; i++)
        {
            m_StudyImageTab[i].SetActive(false);
        }
    }
}
