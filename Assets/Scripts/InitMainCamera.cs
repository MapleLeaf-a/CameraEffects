using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMainCamera : MonoBehaviour
{
    public static InitMainCamera MainCameraInstance;
    public Camera Camera;

    void Awake()
    {
        if (MainCameraInstance == null)
        {
            MainCameraInstance = this;
            MainCameraInstance.Camera = Camera;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
