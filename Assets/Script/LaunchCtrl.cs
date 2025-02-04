﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LaunchCtrl : MonoBehaviour
{
    public enum State {Ready,Aiming,Launch};
    public GameObject Arrow;

    private Vector3 firstPos;
    private float maxRate = 0.0f;
    // Start is called before the first frame update

    private State state;
    void Start()
    {
        state = State.Ready;
        Arrow.SetActive(false);
    }

    public float Zoom {
        get {
            return 0.0f;
        }
    }
    // Update is called once per frame

    void Update()
    {
        
        if (state == State.Ready)
        {

            
            
            if(InputMgr.Instance.ClickDown())
            {
                RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(InputMgr.Instance.curPosition);
                firstPos = InputMgr.Instance.curPosition;
                //firstPos = Camera.main.WorldToScreenPoint(transform.position);
                Physics.Raycast(ray, out hit);
                

                if (hit.collider != null && hit.collider.gameObject.tag == "Player")
                {
                    //Debug.Log("Ball Click");
                    state = State.Aiming;
                    Arrow.SetActive(true);
                }
            }
        }
        else if (state == State.Aiming)
        {

            //float angle = GetAngle(firstPos, Input.mousePosition);
            //float dis = Vector3.Distance(firstPos, Input.mousePosition);
            float angle = GetAngle(firstPos, InputMgr.Instance.curPosition)+180.0f;
            float dis = Vector3.Distance(firstPos, InputMgr.Instance.curPosition);            
            float rate = dis / Mathf.Min(Screen.height,Screen.width);
            float temp_mr = Mathf.Max(maxRate, rate);
            if (rate > Mathf.Max(0.2f,maxRate))
                rate = Mathf.Max(0.2f, maxRate);

            rate /= Mathf.Max(0.2f, maxRate);
            rate /= 0.9f;
            rate = Mathf.Min(rate, 1.0f);
            maxRate = temp_mr;
            Vector3 eurAngle = transform.eulerAngles;
            eurAngle.y = -angle;
            transform.eulerAngles = eurAngle;
            


            Vector3 arrowScale = Arrow.transform.localScale;
            arrowScale.x = rate;
            Arrow.transform.localScale = arrowScale;
            Vector3 arrowPositon = Arrow.transform.localPosition;
            arrowPositon.x = rate * 5;
            Arrow.transform.localPosition = arrowPositon;
           
            
            //if (!Input.GetMouseButton(0))
            if(!InputMgr.Instance.Click())
            {
                
                GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.right) * 15000 * rate);
                state = State.Launch;
                //Debug.Log("rate = " + rate);
                Arrow.SetActive(false);
            }
            else
            {
                
                //Debug.Log("dis = " + dis);
            }
        }
    }
    private float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x)*Mathf.Rad2Deg;
    }

    public State GetState() { return state; }
    public void SetAim()
    {
        firstPos = InputMgr.Instance.curPosition;
        state = State.Aiming;
        Arrow.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
    }
}
