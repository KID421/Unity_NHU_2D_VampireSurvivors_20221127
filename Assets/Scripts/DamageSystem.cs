using UnityEngine;

namespace KID
{
    /// <summary>
    /// 受傷系統
    /// </summary>
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField, Header("血量"), Range(0, 5000)]
        private float hp;
        [SerializeField, Header("受傷半徑"), Range(0, 50)]
        private float radiusDamage;
        [SerializeField, Header("受傷位移")]
        private Vector2 offsetDamage;
        [SerializeField, Header("傷害值物件")]
        private GameObject prefabDamage;
        [SerializeField, Header("傷害值物件位移")]
        private Vector2 offsetDamagePrefab;
        [SerializeField, Header("受傷圖層")]
        private LayerMask layerDamage;
        [SerializeField, Header("受傷無敵時間"), Range(0, 1)]
        private float timeInvisiable = 0.2f;

        private float timer;
        private bool isDamage;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.3f, 0.5f, 0.7f);
            Gizmos.DrawSphere(
                transform.position + transform.TransformDirection(offsetDamage), 
                radiusDamage);
        }

        private void Update()
        {
            GetDamage();
            InvisiableTimer();
        }

        /// <summary>
        /// 造成傷害判定
        /// </summary>
        private void GetDamage()
        {
            // 碰撞 = 2D 物理.覆蓋圓形(中心點，半徑，圖層)
            Collider2D hit = Physics2D.OverlapCircle(
                transform.position + transform.TransformDirection(offsetDamage),
                radiusDamage, layerDamage);

            // 如果 有碰撞物件
            if (hit)
            {
                // 如果 尚未受傷 再生成 傷害值物件
                if (!isDamage)
                {
                    // 設定為已受傷
                    isDamage = true;
                    // 生成傷害值物件
                    Instantiate(
                        prefabDamage, 
                        transform.position + transform.TransformDirection(offsetDamagePrefab),  
                        Quaternion.identity);
                }
            }
        }

        /// <summary>
        /// 無敵時間計時器
        /// </summary>
        private void InvisiableTimer()
        {
            // 如果 已經受傷
            if (isDamage)
            {
                // 如果 計時器 小於 無敵時間 就 累加
                if (timer < timeInvisiable) timer += Time.deltaTime;
                // 否則 計時器 歸零 並且設定為尚未受傷
                else
                {
                    timer = 0;
                    isDamage = false;
                }
            }
        }
    }
}
