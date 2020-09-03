using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedMovil : MonoBehaviour
{
    public bool paredActive;
    public int velocidad = 10;
    private GameObject pared;
    private float paredGoSize, paredGoFirstPosition;
    private MeshRenderer meshButton;
    private AudioController myAudio;

    // Script que añadido a un GO button mueve una pared según su tamaño.
    

    void Start()
    {
        myAudio = GameObject.Find("SFX").GetComponent<AudioController>();
        meshButton = gameObject.GetComponent<MeshRenderer>();
        string[] t = gameObject.name.Split('.'); //Separamos el numero del Texto hacinedo split en el punto .
        pared = GameObject.Find("ParedMovil." + t[1]);
        print(pared.name);
        paredGoSize = pared.transform.localScale.x;
        paredGoFirstPosition = pared.transform.position.x;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if(paredActive && (pared.transform.position.x >= -(paredGoSize - paredGoFirstPosition)))
        {
            pared.transform.Translate(Vector3.left  * velocidad * Time.deltaTime);
        } else if(!paredActive && (pared.transform.position.x <= (paredGoFirstPosition)))
        {
            pared.transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            paredActive = paredActive ? paredActive = false : paredActive = true;
            meshButton.enabled = false;
            myAudio.audioFx.pitch = 3;
            myAudio.audioFx.clip = myAudio.button;
            myAudio.audioFx.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            meshButton.enabled = true;
        }
    }


}
