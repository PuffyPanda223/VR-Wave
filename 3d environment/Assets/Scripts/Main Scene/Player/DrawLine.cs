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
    public GameObject player;
    // load the functions needed from point script to add to the player script 
    private PointSystem pointScript;

    // different tiers of wave difficulties
    private Color safeHitBox = Color.green;
    private Color mediumHitBox = Color.yellow;
    private Color notSafeHitbox = Color.red;

    // floating text prefab which we will instaniate on whenever the user clicks on a wave
    private FloatingText FloatingText;

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
        Distance = 240;
        isGamePaused = false;
        safeWave = 6;
        mediumWave = 3;
        hardWave = 1;
    }
    void Awake()
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
        FloatingText = gameMaster.GetComponent<FloatingText>();

    }

    // Update is called once per frame
    void Update()
    {
        // check to see if the game is paused. If not then proceed 
        if (!isGamePaused)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                // Whatever the mouse position project whats called a raycast until it hits an object able to be hit
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                // The coords of the of the of the object able to be hit by the raycast are stored in this hit variable, Hit meaning target location of hit object
                RaycastHit hit;

                // the first input is the ray used (which in this case is always projected from the mouse position), when it hits something the output will be stored in the hit variable
                // The distance is set to just under the radius of the sphere so the video won't be deleted
                if (Physics.Raycast(ray, out hit, Distance, layer_mask))
                {

                    // The color tells us which level of difficult of wave was selected
                    string waveDifficulty = hit.transform.GetComponent<MeshRenderer>().material.name.Substring(0, 4);

                    // check to see which level of difficulty of wave the player hit
                    switch (waveDifficulty)
                    {
                        case "safe":
                            PointSystem.addScore(safeWave);
                            FloatingText.showFloatingText(safeWave, hit.transform.gameObject);
                            break;
                        case "medi":
                            PointSystem.addScore(mediumWave);
                            break;
                        case "hard":
                            PointSystem.addScore(hardWave);
                            break;
                        default:
                            PointSystem.addScore(hardWave);
                            break;
                    }

                    Destroy(hit.transform.gameObject);


                }
            }
        }
    }
}