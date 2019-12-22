using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootRay();
    }

    void ShootRay()
    {
        //left click to fire
        //cast a ray from the center of the screen of the screen
        //debug the name of the object you hit
        if (Input.GetMouseButtonDown(0))
        {
            //starting point
            //hitInfo
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);
            }
        }
    }
}
