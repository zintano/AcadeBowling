﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialEx;
    public int Xpos;
    public int Ypos;
    public Transform spotLightTarget;
    public int spXpos;
    public int spYpos;

    private int screenHeight;
    private int screenWidth;
    void Start()
    {
        Vector3 spotLightPos;
        string sceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.GetInt(sceneName) != 0)
            Destroy(gameObject);
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        GameObject ui=GameObject.Find("UI");
        transform.SetParent(ui.transform);

        if (spXpos==0 && spYpos==0)
        {
            spotLightPos = Camera.main.WorldToScreenPoint(spotLightTarget.position);
            //spotLightPos = spotLightTarget.localPosition;
            //Debug.Log("스포트라이트 작동"+spotLightPos);
            spotLightPos.z = 0;
            spotLightPos.x -= screenWidth / 2;
            spotLightPos.y -= screenHeight / 2;
            transform.localPosition = spotLightPos;
        }
        else
        {
            transform.localPosition = new Vector3(spXpos, spYpos);
        }

        if (tutorialEx != null)
        {
            tutorialEx.transform.SetParent(transform);
            tutorialEx.transform.localPosition = new Vector3(Xpos, Ypos)/1080* screenHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InputMgr.Instance.ClickDown())
        {
            Destroy(gameObject);
        }
    }
}
