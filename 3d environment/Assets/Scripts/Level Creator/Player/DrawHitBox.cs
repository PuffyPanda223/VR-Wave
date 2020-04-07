using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// get the distance between two points drawn by the user and create a hitbox. Show 
public class DrawHitBox : MonoBehaviour
{
    public Camera camera;
    public GameObject player;
    private LayerMask layer_mask;
    private Vector3 startPos;
    private Vector3 endPos;
    public Material safe;

    // Shadow box is a temporory plane object whose dimensions will continiously change as the user draws a hitbox. It will show the user how big and where the hitbox will be drawn
    private GameObject shadowBox;
    private MeshFilter shadowFilter;
    private MeshRenderer shadowRenderer;
    private Mesh shadowMesh;

  
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
                //Debug.Log("start pos is " + startPos);

                // have the hitbox spawn closer to the player so it doesn't clip through the world
                
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
                //Debug.Log("end position is " + endPos); 
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


        shadowMesh = calculateMesh(startPos, endPos, shadowMesh);
        shadowFilter.mesh = shadowMesh;
        shadowRenderer.material = safe;
    }





    public void generatePlane()
    {
      

        GameObject go = new GameObject("plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        Mesh m = new Mesh();
        MeshCollider mc = new MeshCollider();
        
        m = calculateMesh(startPos, endPos, m);
        
        mf.mesh = m;

        // the material is how we both visually and programmatically differentiate between the level of difficult the wave is
        mr.material = safe;

        go.AddComponent<HitBox>();
        HitBox script = go.GetComponent<HitBox>();
        script.startTime = GlobalTimer.timer;
        //hitbox will remain spawned for 5 seconds
        script.endTime = GlobalTimer.timer + 2f;


        // give hitbox a layer. this will enable us to find and save them later on
        go.gameObject.tag = "hitBox";

    }

    // calculate the mesh  and the necessary components of a mesh given a startpoint and endpoint. Set the mesh of the gameobject you want a mesh for to the output of this function
    public static Mesh calculateMesh(Vector3 startPos, Vector3 endPos, Mesh hitBoxMesh )
    {
        // the height and width is set to whatever the last position is
        float height = endPos.y;
        float width = endPos.x;
        float endZ = endPos.z;

        float startX = startPos.x;
        float startY = startPos.y;
        float startZ = startPos.z;
        //  in order to draw a plane we need 4 vertices. with 4 verticies we can make two right handed triangles and it is with those two right handed triangles we can draw a plane
        int[] triangles;
        Vector3[] vertices;
        // uvs are coordinates that allow the renderer to apply textures to an object. Since UVs follow the same logic of backwards culling as triangles we have to manually calculate the uvs depending on the criteria of plane being drawn
        Vector2[] uv;



        // Because of a process called backwards culling we need to make sure we feed in the vertices in a clockwise motion. So I get the velocity and height of the hitbox being drawn 
        // and determine which way the numbers should be feed in 

        // image four points each representing the positional data that act as the four points of a rectangle, we need to test in what direction on the x and y axis the user is drawing on 
        // and where on the z axis the object is heading towards. Based on the direction we need to manipulate the direction that we draw our triangles in order for it be draw in on the map in the right
        // direction

        if (endPos.z > 0 && startPos.z > 0) // left 
            if (startX < width)
            {
                if (height > startY)
                {
                    triangles = new int[] { 0, 3, 2, 0, 2, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1 1 = 1,0, 2 = 1,1
                        new Vector2(0,0),
                        new Vector2(0,1), 
                        new Vector2(1,1),
                        new Vector2(1,0)
                    };
                }
                else
                {
                    triangles = new int[] { 3, 0, 1, 3, 1, 2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1 1 = 1,0, 2 = 1,1
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0),
                        new Vector2(1,1)
                    };
                }
            }
            else
            {
                if (height > startY)
                {
                    triangles = new int[] { 1, 2, 3, 1, 3, 0 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };
                }
                else
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }

            }
        else if (endZ > 0) // right hand side, backwards culling
        {
            if (startX < 0) // determines which direction the user is drawning, right to left or left to right. Depending on which we feed different triangle data, Less than zero is left hand side
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 3, 0, 2, 0, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }
                else
                {
                    triangles = new int[] { 0,3,2,0,2,1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1),
                        new Vector2(1,0)
                    };
                }
            } else
            {
                if (startY > height)
                {
                    triangles = new int[] { 2,1,0,2,0,3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };

                }
                else
                {
                    triangles = new int[] { 1,0,3,1,3,2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1)
                    };
                }
            }
        }
        else if (startZ > 0 ) // left hand side clockwise culling
        {
            if (width < 0)
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 1, 0, 2, 0, 3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }
                else
                {
                    triangles = new int[] { 1, 2, 3, 1 ,3 , 0  };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };

                }
            } else
            {
                if (startY > height)
                {
                    triangles = new int[] { 2, 3, 0, 2, 0, 1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }
                else
                {
                    triangles = new int[] { 1, 0 , 3, 1, 3, 2 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1),
                        new Vector2(1,1)
                    };

                }
            }
        }
        else // backwards 
        {
            if (startX > width) // 
            {
               
                    if (startY < height)
                    {
                        triangles = new int[] { 0,3,2,0,2,1};
                        uv = new Vector2[] {
                            // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                            new Vector2(0,0),
                            new Vector2(0,1),
                            new Vector2(1,1),
                            new Vector2(1,0)
                        };
                      }
                    else
                    {
                    triangles = new int[] {  2,3,0,2,0,1 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0),
                        new Vector2(1,0)
                    };

                }

            }
            else
            {

                if (startY < height)
                {
                    triangles = new int[] { 1, 2, 3, 1, 3, 0 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,0),
                        new Vector2(1,1),
                        new Vector2(0,1),
                        new Vector2(0,0)
                    };
                }
                else
                {
                    triangles = new int[] { 2,1,0,2,0,3 };
                    uv = new Vector2[] {
                        // x,y 3 = 0,1   1 = 1,0,    2 = 1,1
                        new Vector2(1,1),
                        new Vector2(1,0),
                        new Vector2(0,0),
                        new Vector2(0,1)
                    };
                }
            }
        }

       vertices = new Vector3[]
      {
                // starting position of the vertices in world space
                new Vector3(startX,startY, startZ),
                new Vector3(width,startY, endZ),
                new Vector3(width,height,endZ),
                new Vector3(startX,height,startZ)
      };
        // calculate the uvs at some point
        





        hitBoxMesh.vertices = vertices;
        hitBoxMesh.triangles = triangles;
        hitBoxMesh.uv = uv; 
        hitBoxMesh.RecalculateNormals();
        hitBoxMesh.RecalculateBounds();

        return hitBoxMesh; 

    }



}

