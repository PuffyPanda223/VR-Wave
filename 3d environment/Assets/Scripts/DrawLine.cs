using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class DrawLine : MonoBehaviour
{


    // LineRenderer is a component that given a list of vector3 coords will draw a line. The list has to be vector3 so we can store the x,y,z coords of each point on the screen
    private LineRenderer line;
    private List<Vector3> positions = new List<Vector3>();

    public Camera camera; 


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

        if (Input.GetButton("Fire1"))
        {
            // Whatever the mouse position project whats called a raycast until it hits an object able to be hit
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            // The coords of the of the of the object able to be hit by the raycast are stored in this hit variable, Hit meaning target location of hit object
            RaycastHit hit; 

            // the first input is the ray used (which in this case is always projected from the mouse position), when it hits something the output will be stored in the hit variable
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("got here ");
                if ( DistanceToLastHit(hit.point) > 1f )
                {
                    positions.Add(hit.point);

                    line.positionCount = positions.Count;
                    line.SetPositions(positions.ToArray()); 
                }
            }
        }
        
    }

    private float DistanceToLastHit(Vector3 hitPoint)
    {
        // the whole point of this is just make sure some distance has been made on the line otherwise their is not point is wasting processing power in order to add and update the line renderer
        // however if this is the first time a point has registered then their is nothing to compare it to. So I use mathf.infinity in order to tell my program that it is fine to add this point to
        // the positions list
        if (!positions.Any())
            return Mathf.Infinity;
        // do a vector3 calculaton to gage the distance from the new coords from the raycast coords last stored in the positions list
        return Vector3.Distance(positions.Last(), hitPoint); 
    }
}
