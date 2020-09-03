using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPrivacyPolicy : MonoBehaviour
{
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        print(rect.anchorMin);
        /*
        if (rect.offsetMin.y >= 2050)
        {
            rect.offsetMin = new Vector3(0, 0, 2050);
        }
        */
    }
}
