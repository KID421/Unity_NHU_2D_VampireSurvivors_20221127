using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    [DefaultExecutionOrder(100)]
    /// <summary>
    /// 等級管理器
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region 資料
        public static LevelManager instance;

        public delegate void LevelUp();     // 委派
        public event LevelUp onLevelup;     // 事件

        [SerializeField, Header("吸取經驗值半徑")]
        private float getExpRadius = 3.5f;
        [SerializeField, Header("吸取經驗值速度")]
        private float getExpSpeed = 5.5f;
        [SerializeField, Header("經驗值圖層")]
        private LayerMask layerExp;

        private int lv = 1;
        private float expCurrent;
        private Image imgExp;
        private TextMeshProUGUI textLv;

        [SerializeField]
        private float[] expNeeds;

        /// <summary>
        /// 升級技能選取介面
        /// </summary>
        private Animator aniUpdateLevelAndChooseSkill;
        #endregion

        #region 事件
        private void Awake()
        {
            instance = this;

            imgExp = GameObject.Find("圖片經驗值").GetComponent<Image>();
            textLv = GameObject.Find("文字等級").GetComponent<TextMeshProUGUI>();
            aniUpdateLevelAndChooseSkill = GameObject.Find("升級技能選取介面").GetComponent<Animator>();
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
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getExpRadius, layerExp);

            for (int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];

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
                expCurrent += hit.GetComponent<ExpManager>().exp;   // 累加經驗

                float expNeed = expNeeds[lv - 1];                   // 取得當前等級的經驗需求

                if (expCurrent >= expNeed)                          // 如果 當前經驗值 >= 經驗值需求 (代表升級)
                {
                    expCurrent -= expNeed;                          // 將多餘的經驗還給玩家
                    UpdateLevel();
                }

                imgExp.fillAmount = expCurrent / expNeed;           // 圖片填滿長度 = 當前經驗 / 經驗需求

                Destroy(hit.gameObject);                            // 刪除 經驗值物件
            }
        }

        /// <summary>
        /// 升級
        /// </summary>
        private void UpdateLevel()
        {
            lv++;                                           // 升級
            textLv.text = "Lv " + lv;                       // 更新等級介面
            aniUpdateLevelAndChooseSkill.enabled = true;    // 啟動升級介面動畫
            onLevelup();                                    // 觸發事件
        }
        #endregion
    }
}
