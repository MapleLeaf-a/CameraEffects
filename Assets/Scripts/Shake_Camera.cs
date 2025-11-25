using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Camera : MonoBehaviour
{
    public Camera Camera;
    public float intensity = 0.1f;
    public float duration = 0.05f;

    private float currentTime;
    private float deltaTime = 0.01f;
    private Vector3 prePos;

    //随机数晃动摄像机
    public bool randomShake = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (randomShake)
        {
            StartCoroutine(RandomShake());
        }
        //else if ()
    }

    IEnumerator RandomShake()
    {
        prePos = transform.position;
        randomShake = false;

        while (currentTime >= 0)
        {
            prePos = CharacterController.Instance.transform.position;
            Vector2 v2 = new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)) * intensity;
            transform.position += new Vector3(v2.x, v2.y, 0);

            currentTime -= deltaTime;

            yield return new WaitForSeconds(deltaTime);
        }

        currentTime = duration;

        Vector3 targetposition = new Vector3(prePos.x, prePos.y, transform.position.z);
        Smooth_Camera.SmoothMove(targetposition);
    }
}
