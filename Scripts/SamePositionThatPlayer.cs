using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamePositionThatPlayer : MonoBehaviour
{
    public float ajusteX = 0;
    public float ajusteY = 0;
    private Vector3 pos;
    // Start is called before the first frame update
    private Transform player;
    void Start()
    {
        
        player = GameObject.FindWithTag ("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(player.transform.position.x + ajusteX, player.transform.position.y + ajusteY, transform.position.z);
        transform.position = pos;
    }
}
