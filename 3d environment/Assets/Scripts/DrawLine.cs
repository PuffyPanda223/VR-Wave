using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class DrawLine : MonoBehaviour
{


    // LineRenderer is a component that given a list of vector3 coords will draw a line. The list has to be vector3 so we can store the x,y,z coords of each point on the screen
    private LineRenderer line;
    private List<Vector3> positions; 



    // Start is called before the first frame update
    void Awake ()
    {
        // make line of type LineRenderer the actual line renderer that will be used later on
        line = GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {   
        // when the user first holds down the left click it means they are inputting new lines to be drawn so clear out the previous coordinates in the positions list 
        if (Input.GetButtonDown("Fire1"))
            positions.Clear(); 

        if (Input.GetButtonDown("Fire1"))
        {

        }
        
    }
}
