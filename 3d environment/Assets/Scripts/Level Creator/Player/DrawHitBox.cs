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
    public GameObject prefab;


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
    }

    // as the user is drawing the hitbox show to the user how it is going to look. It has no components is merely a shell of what it will look like
    private void DrawShadowBox()
    {


        shadowMesh = CreateHitBox.calculateMesh(startPos, endPos, shadowMesh);
        shadowFilter.mesh = shadowMesh;
        shadowRenderer.material = safe;
    }




    // creates a plane object given points from the user and adds the hitbox to a global list for saving
    public void generatePlane()
    {
      

        GameObject go = new GameObject("plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        Mesh m = new Mesh();
        MeshCollider mc = new MeshCollider();
        
        m = CreateHitBox.calculateMesh(startPos, endPos, m);
        
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


        // now that all the details of the object have been finialised we populate this custom data class with the necessary information so that the mesh renderer and filter can generate the hitbox when loading from a save file
        HitboxData actor = new HitboxData();

        actor.GetMeshDetails(mf.mesh);
        actor.startTime = script.startTime;
        actor.endTime = script.endTime;
        actor.difficulty = "safe";

        //we have a static global list that is the same type as the above custom class. that makes sure there is one certialized location where all the list data of our hitboxes are being kept. this makes saving and loading a lot easier
        SaveData.AddToList(actor);
    }

    

}

