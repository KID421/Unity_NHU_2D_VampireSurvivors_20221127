using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KID
{
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// 技能系統
    /// </summary>
    public class SkillSystem : MonoBehaviour
    {
        private Button[] btnSkills = new Button[3];
        private TextMeshProUGUI[] textTitles = new TextMeshProUGUI[3];
        private Image[] imgSkills = new Image[3];
        private TextMeshProUGUI[] textDescriptions = new TextMeshProUGUI[3];
        private TextMeshProUGUI[] textSkillsLv = new TextMeshProUGUI[3];

        private void Awake()
        {
            LevelManager.instance.onLevelup += ShowSkillUI;

            for (int i = 1; i < 4; i++)
            {
                btnSkills[i - 1] = GameObject.Find("按鈕技能選取 " + i).GetComponent<Button>();
                textTitles[i - 1] = GameObject.Find("文字技能標題 " + i).GetComponent<TextMeshProUGUI>();
                imgSkills[i - 1] = GameObject.Find("圖片技能圖示 " + i).GetComponent<Image>();
                textDescriptions[i - 1] = GameObject.Find("文字技能說明 " + i).GetComponent<TextMeshProUGUI>();
                textSkillsLv[i - 1] = GameObject.Find("文字技能等級狀態 " + i).GetComponent<TextMeshProUGUI>();
            }
        }

        /// <summary>
        /// 顯示技能介面
        /// </summary>
        private void ShowSkillUI()
        {
            
        }
    }
}
