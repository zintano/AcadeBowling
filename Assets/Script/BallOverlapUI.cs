﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallOverlapUI : MonoBehaviour
{

    public GameObject ball;

    private LaunchCtrl lc;
    private int screenHeight;
    private int screenWidth;
    private float radius;
    private Image img;
    private bool overlabOn;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        lc = ball.GetComponent<LaunchCtrl>();
        img = GetComponent<Image>();
        overlabOn = true;

        radius = transform.gameObject.GetComponent<RectTransform>().rect.height/2;
        Debug.Log("radius="+ radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (lc.GetState() != LaunchCtrl.State.Ready)
        {
            gameObject.SetActive(false);
        }
        else if (overlabOn)
        {
            img.enabled = true;

            Vector3 overlapPos = Camera.main.WorldToScreenPoint(ball.transform.position);

            overlapPos.z = 0.0f;
            if (InputMgr.Instance.ClickDown() && Vector3.Distance(InputMgr.Instance.curPosition, overlapPos) < radius)
            {


                lc.SetAim();

            }

            overlapPos.x -= screenWidth / 2;
            overlapPos.y -= screenHeight / 2;

            transform.localPosition = overlapPos;

            if (Vector3.Distance(Camera.main.transform.position, ball.transform.position) < 10.0f)
            {
                overlabOn = false;
            }
        }
        else
        {
            img.enabled = false;
            if (Vector3.Distance(Camera.main.transform.position, ball.transform.position) > 50.0f)
            {
                overlabOn = true;
            }
        }
    }
}
