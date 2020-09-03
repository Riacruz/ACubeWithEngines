using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ShowFPS : MonoBehaviour
{
    private Text fpsText;
    public float deltaTime;
    public bool isDebug;

    private void Awake()
    {
        fpsText = GetComponentInChildren<Text>();
        
        if (isDebug) DontDestroyOnLoad(this.gameObject);
        else fpsText.text = "";

       
    }
   
    void Update()
    {
        if (isDebug)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
        }

        
    }
}
