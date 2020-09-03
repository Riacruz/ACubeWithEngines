using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    GameObject textUnderCenter;
    public bool isReloaded;
    private Propulsor prop;
    // Start is called before the first frame update
    void Start()
    {
        /*
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        */
        textUnderCenter = GameObject.Find("Text_UnderCenterScreen");
        isReloaded = false;
        prop = gameObject.GetComponent<Propulsor>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<ButtonOptions>().OnClickSettings();
        }

        if (SimpleInput.GetAxis("Horizontal")> 0.8f) //Hacia la derecha
        {
            prop.leftIsActive = true;
            // gameObject.GetComponent<Propulsor>().humoLeft.Play();
            CompruebaGas();
        } else if (SimpleInput.GetAxis("Horizontal") < 0.08f)
        {
            prop.leftIsActive = false;
        }

        if (SimpleInput.GetAxis("Horizontal") < -0.8f) //Hacia la izquierda
        {
            prop.rightIsActive = true;
            // gameObject.GetComponent<Propulsor>().humoRight.Play();
            CompruebaGas();
        } else if (SimpleInput.GetAxis("Horizontal") > -0.08f)
        {
            prop.rightIsActive = false;
        }

        if (SimpleInput.GetAxis("Vertical")>0.8f)
        {
            prop.upIsActive = true;
            // gameObject.GetComponent<Propulsor>().humoDown.Play();
            CompruebaGas();



        } else if(SimpleInput.GetAxis("Vertical") < 0.8f)
        {
            prop.upIsActive  = false;
        }
        
    }

    public void CompruebaGas()
    {
        if (prop.gas <= 0)
        {
            if (!isReloaded )
            {
                
                prop.RestartReload();
               // reloadsCount += 1;
            }
            else
            {
                if (isReloaded) { prop.RestartExit();  }
            }
        }
        else
        {
            if (!prop.levelComplete )
            {
                prop.text_Restart.gameObject.SetActive(false);
                prop.text_Reload.gameObject.SetActive(false);
                //prop.Text_Exit.gameObject.SetActive(false);
            }
        }
    }
}
