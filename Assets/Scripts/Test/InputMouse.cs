using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    public Camera m_Camera;
    public GameObject m_UiPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                GameObject gameobj = hit.collider.gameObject;
                if (gameobj.tag == "collider")
                {
                    m_UiPanel.transform.position = new Vector3(Input.mousePosition.x + 100, Input.mousePosition.y + 100, Input.mousePosition.z);
                    m_UiPanel.SetActive(true);
                }
            }
        }
    }
}
