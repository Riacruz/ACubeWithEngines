using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDisolve : MonoBehaviour
{
    public SpawnEffect goAsociado;
    private SpawnEffect mySpawn;
    // Start is called before the first frame update
    void Start()
    {
        mySpawn = GetComponent<SpawnEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(goAsociado.isActiveAndEnabled)
        {
            mySpawn.enabled = true;
        }
    }
}
