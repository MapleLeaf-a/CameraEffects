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

    V2Delegate v2_strategy; //使用生成v2的策略

    //随机数晃动摄像机
    public bool randomShake = false;

    //定向脉冲
    public bool verticalShake = false;
    public bool horizontalShake = false;

    //指数衰减
    public bool exponentialDecay = false;
    private float decay = 1f;

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
            randomShake = false;
            v2_strategy = RandomV2;
            StartCoroutine(Shake(v2_strategy));
        }
        else if (verticalShake)
        {
            verticalShake = false;
            v2_strategy = VerticalV2;
            StartCoroutine(Shake(v2_strategy));
        }
        else if (horizontalShake)
        {
            horizontalShake = false;
            v2_strategy = HorizontalV2;
            StartCoroutine(Shake(v2_strategy));
        }
    }

    public delegate Vector2 V2Delegate();

    public Vector2 RandomV2()
    {
        return new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
    }

    public Vector2 VerticalV2()
    {
        return new Vector2(0, Random.Range(-0.01f, 0.01f));
    }

    public Vector2 HorizontalV2()
    {
        return new Vector2(Random.Range(-0.01f, 0.01f), 0);
    }

    IEnumerator Shake(V2Delegate random_strategy)
    {
        prePos = transform.position;

        while (currentTime >= 0)
        {
            if (exponentialDecay) //如果启用指数级衰减
            {
                decay = Mathf.Exp(-(duration - currentTime));
            }
            else
            {
                decay = 1;
            }

            prePos = CharacterController.Instance.transform.position;
            Vector2 v2 = random_strategy() * intensity * decay;
            transform.position += new Vector3(v2.x, v2.y, 0);

            currentTime -= deltaTime;

            yield return new WaitForSeconds(deltaTime); //持续duration/deltaTime秒
        }

        currentTime = duration;

        Vector3 targetposition = new Vector3(prePos.x, prePos.y, transform.position.z);
        Smooth_Camera.SmoothMove(targetposition);
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

            yield return new WaitForSeconds(deltaTime); //持续duration/deltaTime秒
        }

        currentTime = duration;

        Vector3 targetposition = new Vector3(prePos.x, prePos.y, transform.position.z);
        Smooth_Camera.SmoothMove(targetposition);
    }
}
