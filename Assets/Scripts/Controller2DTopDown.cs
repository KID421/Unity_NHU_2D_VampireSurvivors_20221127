using UnityEngine;

namespace KID
{
    /// <summary>
    /// 2D Top Down 類型控制器
    /// </summary>
    public class Controller2DTopDown : MonoBehaviour
    {
        [SerializeField, Header("移動速度"), Range(0, 100)]
        private float speed = 3.5f;

        private Animator ani;
        private Rigidbody2D rig;
        private string parWalk = "開關走路";

        // 喚醒事件：播放遊戲時執行一次，處理初始
        private void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
        }

        // 更新事件：約 60FPS
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            // print($"玩家的水平軸向值：{h}");

            rig.velocity = new Vector2(h , v) * speed;

            UpdateAnimation(h, v);
        }

        private void UpdateAnimation(float h, float v)
        {
            ani.SetBool(parWalk, h != 0 || v != 0);
        }
    }
}
