using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownTimer : MonoBehaviour
{
    // Static makes it so the variable becomes a member of the class and not an instance of the class, meaning we can access the variable from the class
    static public float timer = 0;
    public GameObject score; 

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
    }
}
