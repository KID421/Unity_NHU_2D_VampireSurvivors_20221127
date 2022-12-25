using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    /// <summary>
    /// 等級管理器
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("吸取經驗值半徑")]
        private float getExpRadius = 3.5f;
        [SerializeField, Header("吸取經驗值速度")]
        private float getExpSpeed = 5.5f;
        [SerializeField, Header("經驗值圖層")]
        private LayerMask layerExp;

        private int lv = 1;
        private float expCurrent;
        private Image imgExp;
        private TextMeshProUGUI textExp;

        [SerializeField]
        private float[] expNeeds;
        #endregion

        #region 事件
        private void Awake()
        {
            imgExp = GameObject.Find("圖片經驗值").GetComponent<Image>();
            textExp = GameObject.Find("文字等級").GetComponent<TextMeshProUGUI>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.1f, 0.8f, 0.7f, 0.3f);
            Gizmos.DrawSphere(transform.position, getExpRadius);
        }

        private void Update()
        {
            GetExpObject();
        }
        #endregion

        #region 方法
        [ContextMenu("更新經驗值需求表")]
        private void UpdateExpNeeds()
        {
            expNeeds = new float[99];

            for (int i = 0; i < expNeeds.Length; i++)
            {
                expNeeds[i] = (i + 1) * 100 + ((i + 1) * 10);
            }
        } 

        /// <summary>
        /// 吸取經驗值物件
        /// </summary>
        private void GetExpObject()
        {
            // 碰撞物件 = 2D 物理 覆蓋圓形範圍(本物件座標，取得經驗值半徑，取得經驗值圖層)
            Collider2D hit = Physics2D.OverlapCircle(transform.position, getExpRadius, layerExp);

            // 如果 碰撞物件存在
            if (hit)
            {
                // 座標 = 二維.向前移動(碰撞物件的座標，本物件座標，速度 * 一幀的時間)
                Vector2 pos = Vector2.MoveTowards(
                    hit.transform.position, transform.position, getExpSpeed * Time.deltaTime);

                // 碰撞物件 的 座標 = 座標
                hit.transform.position = pos;

                UpdateExp(hit);
            }
        }

        /// <summary>
        /// 更新經驗值
        /// </summary>
        /// <param name="hit">經驗值碰撞器</param>
        private void UpdateExp(Collider2D hit)
        {
            float dis = Vector2.Distance(hit.transform.position, transform.position);

            if (dis <= 0.5f)
            {
                expCurrent += hit.GetComponent<ExpManager>().exp;

                float expNeed = expNeeds[lv - 1];
                imgExp.fillAmount = expCurrent / expNeed;

                Destroy(hit.gameObject);
            }
        }
        #endregion
    }
}
