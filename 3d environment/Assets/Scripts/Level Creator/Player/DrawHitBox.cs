using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// get the distance between two points drawn by the user and create a hitbox. Show 
public class DrawHitBox : MonoBehaviour
{
    public Camera camera;
    private LayerMask layer_mask; 
    private Vector3 startPos;
    private Vector3 endPos;
    
    // we want the entire plane object to be on the same z axis so we get it from the starting pos z and use that when generating our hitbox
    private float zAxis; 
    void Start()
    {
        // in the game world their is an invisble sphere closer to the player. This will create hit boxes a decent way away from the player but the entire distance. Makes generating hitboxes easier
        layer_mask = LayerMask.GetMask("DrawLayer"); 
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // clear the startPos, as we are creating a new hitbox
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // get the pos of the start point and log them into the startPos variable. This will allow us calculate the distance we need to render the hitbox
            if (Physics.Raycast(ray, out hit))
            {
                // Make the start position = inital set of Vector3 coords that the user clicks on
                startPos = hit.point;
                zAxis = startPos.z - 3f;
                Debug.Log("start position is " + startPos);
            }
        }

        // continiously update the end position variable 
        if (Input.GetButton("Fire1"))
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // get the pos of the start point and log them into the startPos variable. This will allow us calculate the distance we need to render the hitbox
            if (Physics.Raycast(ray, out hit))
            {
                // Make the start position = inital set of Vector3 coords that the user clicks on
                endPos = hit.point;
                Debug.Log("end position is " + endPos);
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            // after the user has released the key create the hitbox
            generatePlane(); 
        }
    }


    public void generatePlane()
    {

        GameObject go = new GameObject("plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();
       
        
        // the height and width is set to whatever the last position is
        float height = endPos.y;
        float width = endPos.x;
        
        Debug.Log("Width and height is " + width + " "+ height);
        float startX = startPos.x;
        float startY = startPos.y; 
        //  in order to draw a plane we need 4 vertices. with 4 verticies we can make two right handed triangles and it is with those two right handed triangles we can draw a plane
        m.vertices = new Vector3[]
        {
            // starting position of the vertices in world space
            new Vector3(startX,startY, zAxis), 
            new Vector3(width,startY, zAxis),
            new Vector3(width,height,zAxis), 
            new Vector3(startX,height,zAxis)
        };

        m.uv = new Vector2[]
        {
            new Vector2(startX,startY),
            new Vector2(width, startY), 
            new Vector2(width,height ), 
            new Vector2(startX, height)
        };

      
        m.triangles = new int[] { 0 ,1 ,2 ,0 ,2 ,3};


        mf.mesh = m;
        m.RecalculateNormals();
        m.RecalculateBounds();

        
       

    }



}
