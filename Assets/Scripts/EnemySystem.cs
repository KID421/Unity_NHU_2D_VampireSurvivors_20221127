using UnityEngine;

namespace KID
{
    /// <summary>
    /// 敵人系統：追蹤玩家
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float speed = 3.5f;
        [SerializeField, Header("停止距離"), Range(0, 10)]
        private float stopDistance = 1.5f;

        private string nameTarget = "射手";
        private Transform traTarget;

        // 繪製圖示：僅在 Unity 編輯器內顯示，輔助用
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.1f, 0.1f, 0.3f);       // 圖示顏色
            Gizmos.DrawSphere(transform.position, stopDistance);    // 繪製球體圖示
        }

        private void Awake()
        {
            traTarget = GameObject.Find(nameTarget).transform;
        }

        private void Update()
        {
            Track();
        }

        /// <summary>
        /// 追蹤
        /// </summary>
        private void Track()
        {
            Vector3 posTarget = traTarget.position;
            Vector3 posCurrent = transform.position;
            
            Flip(posCurrent.x, posTarget.x);

            // 距離小於停止距離 就 跳出不追蹤
            if (Vector3.Distance(posCurrent, posTarget) <= stopDistance) return;

            posCurrent = Vector3.MoveTowards(posCurrent, posTarget, speed * Time.deltaTime);
            transform.position = posCurrent;
        }

        /// <summary>
        /// 翻面
        /// </summary>
        /// <param name="xCurrent">此物件的 X</param>
        /// <param name="xTarget">目標物件的 X</param>
        private void Flip(float xCurrent, float xTarget)
        {
            float angle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}
