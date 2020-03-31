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

       // different tiers of wave difficulties
    private Color safeHitBox = Color.green;
    private Color mediumHitBox = Color.yellow;
    private Color notSafeHitbox = Color.red;

    // floating text prefab which we will instaniate on whenever the user clicks on a wave
    public GameObject floatingText;

    // interger score values for the various waves. They're given values in the start function

    private int safeWave;
    private int mediumWave;
    private int hardWave; 

    // the static makes it able to be accessed by other components of the script  
     public static bool isGamePaused; 


    // Start is called before the first frame update
    private void Start()
    {
        layer_mask = LayerMask.GetMask("hitBox");
        Distance = 49;
        isGamePaused = false;
        safeWave = 5;
        mediumWave = 3;
        hardWave = 1; 
    }
    void Awake ()
    {
        // make line of type LineRenderer the actual line renderer that will be used later on
        line = GetComponent<LineRenderer>();
        
        // gets the global point system stored in the game master game object
        getPointSystem(); 

       
    }

    void getPointSystem()
    {

        // These two lines of code get the game amster object in our game which contains a lot of scripts that keep track of the overall game and loads specifically 
        // the point scoring script to give access to the function that increments the player score so whenever a hitbox is successfully hit we can increase the player score 
        GameObject gameMaster = GameObject.Find("Game Master");
        pointScript = (PointSystem)gameMaster.GetComponent(typeof(PointSystem));

    }

    // Update is called once per frame
    void Update()
    {
        // check to see if the game is paused. If not then proceed 
        if (!isGamePaused)
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
                if (Physics.Raycast(ray, out hit, Distance, layer_mask))
                {

                    if (DistanceToLastHit(hit.point) > 1f)
                    {

                        positions.Add(hit.point);

                        //line.positionCount = positions.Count;
                        //line.SetPositions(positions.ToArray());

                        // The color tells us which level of difficult of wave was selected
                        Color hitColor = hit.transform.gameObject.GetComponent<Renderer>().material.color;
                        // check to see which level of difficulty of wave the player hit
                        if (hitColor == Color.green)
                        {
                            pointScript.addScore(safeWave);
                            showFloatingText(safeWave, hit.transform.gameObject, floatingText, camera.transform.position);
                            //Debug.Log("safe object hit");
                        }
                        else if (hitColor == Color.yellow)
                        {
                            //Debug.Log("Medium hit box hit");
                            pointScript.addScore(mediumWave);
                            showFloatingText(mediumWave, hit.transform.gameObject, floatingText, camera.transform.position); 
                        }
                        else
                        {
                            //Debug.Log("Not safe hitbox hit");
                            pointScript.addScore(hardWave);
                            showFloatingText(hardWave, hit.transform.gameObject, floatingText, camera.transform.position);
                        }
                        Destroy(hit.transform.gameObject);

                    }
                }
            }
        }
    }

    public static void showFloatingText(int score, GameObject enemy, GameObject floatingText, Vector3 camera)
    {
        
       // genenerate some text at the location the hitbox was hit
        var text = Instantiate(floatingText, enemy.transform.position, Quaternion.LookRotation(camera) );
        text.GetComponent<TextMesh>().text = "+" + score.ToString(); 
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
