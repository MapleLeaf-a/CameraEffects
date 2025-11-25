using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_Camera : MonoBehaviour
{
    public static Camera Camera;
    private Vector2 characterPos;
    private Vector2 submission; //摄像机与玩家之间相减的向量
    
    //平滑相机移动
    public bool smooth_move_enable = true;
    public static float smooth_move_speed = 5f;
    public float later_smooth_move = 1f; //停止时的再向前趋势

    //死区设置
    public bool follow_drag_enable = false;
    public float follow_drag_margin_left = 0.2f;
    public float follow_drag_margin_right = 0.2f;
    public float follow_drag_margin_up = 0.2f;
    public float follow_drag_margin_down = 0.2f;
    bool isOutOfMargin;

    // Start is called before the first frame update
    void Start()
    {
        isOutOfMargin = false;
        Camera = InitMainCamera.MainCameraInstance.Camera;
    }

    // Update is called once per frame
    void Update()
    {
        characterPos = CharacterController.GetCurrentPosInV2();
        if (follow_drag_enable)
        {
            if (characterPos.x >= transform.position.x + follow_drag_margin_right || characterPos.x <= transform.position.x - follow_drag_margin_left ||
                characterPos.y >= transform.position.y + follow_drag_margin_up || characterPos.y <= transform.position.y - follow_drag_margin_down)
            {
                isOutOfMargin = true;
            }
            else
            {
                isOutOfMargin = false;
            }
            if (isOutOfMargin)
            {
                if (smooth_move_enable == true)
                {
                    SmoothMove(characterPos);
                }
                else
                {
                    transform.position = characterPos;
                }
            }
        }
        else
        {
            if (smooth_move_enable == true)
            {
                SmoothMove(characterPos);
            }
            else
            {
                transform.position = characterPos;
            }
        }
    }

    void SmoothMove()
    {
        submission = characterPos - new Vector2(transform.position.x, transform.position.y);
        Vector2 v2 = submission * Time.deltaTime * smooth_move_speed;
        transform.position += new Vector3(v2.x, v2.y, 0);
    }

    public static void SmoothMove(Vector2 targetPos)
    {
        Vector2 v2 = (targetPos - new Vector2(Camera.transform.position.x, Camera.transform.position.y)) * Time.deltaTime * smooth_move_speed;
        Camera.transform.position += new Vector3(v2.x, v2.y, 0);
    }
}
