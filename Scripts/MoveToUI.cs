using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToUI : MonoBehaviour
{

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }




    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            {

            // assign new position to where finger was pressed
            //transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            // transform.position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
           
            transform.position = Input.mousePosition;

        }
        for (var i = 0; i < Input.touchCount; i++)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // assign new position to where finger was pressed
                transform.position = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, transform.position.z);

            }

            

        }

    }
    */





}
