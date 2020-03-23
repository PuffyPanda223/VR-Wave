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


    // Shadow box is a temporory plane object whose dimensions will continiously change as the user draws a hitbox. It will show the user how big and where the hitbox will be drawn
    private GameObject shadowBox;
    private MeshFilter shadowFilter;
    private MeshRenderer shadowRenderer;
    private Mesh shadowMesh;
    
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

            // Create the shadowbox object which will show the user how the box is being drawn. It will continiously be updated while the user is holding down the fire button
            // this object will be destroyed when the user releases the fire button
            shadowBox = new GameObject("plane");
            shadowFilter = shadowBox.AddComponent(typeof(MeshFilter)) as MeshFilter;
            shadowRenderer = shadowBox.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            shadowMesh = new Mesh(); 



            // get the pos of the start point and log them into the startPos variable. This will allow us calculate the distance we need to render the hitbox
            if (Physics.Raycast(ray, out hit))
            {
                // Make the start position = inital set of Vector3 coords that the user clicks on
                startPos = hit.point;
                zAxis = startPos.z - 3f;
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
                // show the user how the hitbox is being drawn in real time 
                DrawShadowBox(); 
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            // after the user has released the key create the hitbox
            generatePlane();
            destroyShadow(); 
        }
    }

    private void destroyShadow()
    {
       Destroy(shadowBox);

        //implement at a later date. Some floating text that says the hitbox has been created
    }

    private void DrawShadowBox()
    {
        // the height and width is set to whatever the last position is
        float height = endPos.y;
        float width = endPos.x;

       
        float startX = startPos.x;
        float startY = startPos.y;
        //  in order to draw a plane we need 4 vertices. with 4 verticies we can make two right handed triangles and it is with those two right handed triangles we can draw a plane



        Vector3[] vertices;
        int[] triangles;
        // Because of a process called backwards culling we need to make sure we feed in the vertices in a clockwise motion
        if (startY < height)
        {


            if (startX < width)
            {
                vertices = new Vector3[]
               {
                                // starting position of the vertices in world space
                    new Vector3(startX,startY, zAxis),
                    new Vector3(startX,height, zAxis),
                    new Vector3(width,height,zAxis),
                    new Vector3(width,startY,zAxis)
               };


            }
            else
            {
                vertices = new Vector3[]
                {

                    new Vector3(startX,startY, zAxis),
                    new Vector3(width,startY, zAxis),
                    new Vector3(width,height,zAxis),
                    new Vector3(startX,height,zAxis)

                };


            }


        }
        else
        {
            if (width > startX)
            {
                vertices = new Vector3[]
                {
                    new Vector3(startX,startY,zAxis),
                    new Vector3(width,startY,zAxis),
                    new Vector3(width,height, zAxis),
                    new Vector3(startX,height,zAxis)

                };


            }
            else
            {
                vertices = new Vector3[]
                 {
                    new Vector3(startX,startY,zAxis),
                    new Vector3(startX,height,zAxis),
                    new Vector3(width,height, zAxis),
                    new Vector3(width,startY,zAxis)

                 };


            }
        }

        triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        // calculate the uvs at some point
        shadowMesh.vertices = vertices;
        shadowMesh.triangles = triangles;


        shadowFilter.mesh = shadowMesh;
        shadowMesh.RecalculateNormals();
        shadowMesh.RecalculateBounds();

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
        
   
        float startX = startPos.x;
        float startY = startPos.y;
        //  in order to draw a plane we need 4 vertices. with 4 verticies we can make two right handed triangles and it is with those two right handed triangles we can draw a plane


       
        Vector3[] vertices; 
        int[] triangles; 
        // Because of a process called backwards culling we need to make sure we feed in the vertices in a clockwise motion. So I get the velocity and height of the hitbox being drawn 
        // and determine which way the numbers should be feed in 
        if (startY < height)
        {
           

            if (startX < width)
            {
                vertices = new Vector3[]
               {
                                // starting position of the vertices in world space
                    new Vector3(startX,startY, zAxis),
                    new Vector3(startX,height, zAxis),
                    new Vector3(width,height,zAxis),
                    new Vector3(width,startY,zAxis)
               };
                

            } else
            {
                vertices = new Vector3[]
                {
                                
                    new Vector3(startX,startY, zAxis),
                    new Vector3(width,startY, zAxis),
                    new Vector3(width,height,zAxis),
                    new Vector3(startX,height,zAxis)

                };

             
            }

            
        } else
            {
            if (width > startX)
            {
                vertices = new Vector3[]
                {
                    new Vector3(startX,startY,zAxis),
                    new Vector3(width,startY,zAxis),
                    new Vector3(width,height, zAxis),
                    new Vector3(startX,height,zAxis)

                };


            } else
            {
               vertices = new Vector3[]
                {
                    new Vector3(startX,startY,zAxis),
                    new Vector3(startX,height,zAxis),
                    new Vector3(width,height, zAxis),
                    new Vector3(width,startY,zAxis)

                };
               

            }
        }

        triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        // calculate the uvs at some point
        m.vertices = vertices;
        m.triangles = triangles;
        

        mf.mesh = m;
        m.RecalculateNormals();
        m.RecalculateBounds();

        
       

    }



}
