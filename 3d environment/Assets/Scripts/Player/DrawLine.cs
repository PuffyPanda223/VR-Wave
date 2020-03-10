using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class DrawLine : MonoBehaviour
{


    // LineRenderer is a component that given a list of vector3 coords will draw a line. The list has to be vector3 so we can store the x,y,z coords of each point on the screen
    private LineRenderer line;
    private List<Vector3> positions = new List<Vector3>();
    int layer_mask;
    int Distance; 
    public Camera camera;
    // load the functions needed from point script to add to the player script 
    private PointSystem pointScript; 

    // Start is called before the first frame update
    private void Start()
    {
        layer_mask = LayerMask.GetMask("hitBox");
        Distance = 49;
    }
    void Awake ()
    {
        // make line of type LineRenderer the actual line renderer that will be used later on
        line = GetComponent<LineRenderer>();

        // These two lines of code get the game amster object in our game which contains a lot of scripts that keep track of the overall game and loads specifically 
        // the point scoring script to give access to the function that increments the player score so whenever a hitbox is successfully hit we can increase the player score 
        GameObject gameMaster = GameObject.Find("Game Master");
        pointScript = (PointSystem) gameMaster.GetComponent(typeof(PointSystem));
      

       
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
            
            // The coords of the of the of the object able to be hit by the raycast are stored in this hit variable, Hit meaning target location of hit object
            RaycastHit hit; 

            // the first input is the ray used (which in this case is always projected from the mouse position), when it hits something the output will be stored in the hit variable
            // The distance is set to just under the radius of the sphere so the video won't be deleted
            if (Physics.Raycast(ray, out hit, Distance , layer_mask))
            {
             
                if ( DistanceToLastHit(hit.point) > 1f )
                {
                  
                    positions.Add(hit.point);
                   
                    //line.positionCount = positions.Count;
                    //line.SetPositions(positions.ToArray());
                  
                    Destroy(hit.transform.gameObject);
                    pointScript.addScore(5);
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
