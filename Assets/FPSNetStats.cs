using UnityEngine;
using System;
using System.Collections;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Diagnostics;

public class FPSNetStats : MonoBehaviour
{
    public KeyCode openFPS = KeyCode.P;
    public bool state;

    private float deltaTime = 0.0f;
    private float kbsTimer = 1.0f; //X Seconds
    private float currTimer;
    private float kbInLast = 0.0f;
    private float kbOutLast = 0.0f;
    private float kbInCurr = 0.0f;
    private float kbOutCurr = 0.0f;
    private float kbsIN1 = 0.0f;
    private float kbsOUT1 = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        //Update FPS
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        //Update KBs Timer
        currTimer -= Time.deltaTime;
        if (currTimer <= 0.0f)
        {
            kbsIN1 = 0.0f;
            kbsOUT1 = 0.0f;
            kbInCurr = 0.0f;
            kbOutCurr = 0.0f;
            kbInCurr = (NetworkManager.Instance.Networker.BandwidthIn / 1024.0f);
            kbOutCurr = (NetworkManager.Instance.Networker.BandwidthOut / 1024.0f);
            if (kbInCurr - kbInLast > 0.0f)
                kbsIN1 = (kbInCurr - kbInLast);
            if (kbOutCurr - kbOutLast > 0.0f)
                kbsOUT1 = (kbOutCurr - kbOutLast);

            kbInLast = 0.0f;
            kbOutLast = 0.0f;
            kbInLast = (NetworkManager.Instance.Networker.BandwidthIn / 1024.0f);
            kbOutLast = (NetworkManager.Instance.Networker.BandwidthOut / 1024.0f);

            currTimer = kbsTimer; //Reset Timer
        }

        if (Input.GetKeyDown(openFPS))
        {
            if (state == false) //Toggle
            {
                state = true;
            }
            else
            {
                state = false;
            }
        }
    }

    private void OnGUI()
    {
        if (state)
        {
            //CALC KBS IN & OUT
            string kbsIN = string.Format("{0:0} KBs", kbsIN1);
            string kbsOUT = string.Format("{0:0} KBs", kbsOUT1);

            GUILayout.Space(10);
            GUILayout.Label("The current server time is: " + string.Format("{0:0}", NetworkManager.Instance.Networker.Time));
            GUILayout.Label("Kilobytes In: " + kbsIN);
            GUILayout.Label("Kilobytes Out: " + kbsOUT);

            //CALC FPS & UPDATE TIME
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUILayout.Label("FPS: " + text);
        }
    }

}