using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器資料
    /// </summary>
    [CreateAssetMenu(menuName = "KID/Weapon Data", fileName = "New Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("武器物件")]
        public GameObject prefabWeapon;
        [Header("武器等級資料")]
        public WeaponLevelData[] weaponLevelDatas;
    }

    [System.Serializable]
    public class WeaponLevelData
    {
        [Header("武器生成間隔"), Range(0, 10)]
        public float intervalSpawn = 3;
        [Header("武器攻擊力"), Range(0, 10000)]
        public float attack;
        [Header("武器物件數量、速度與位置")]
        public WeaponObject[] weaponObjects;
    }

    [System.Serializable]
    public class WeaponObject
    {
        [Header("武器速度")]
        public Vector2 speed;
        [Header("武器生成位置")]
        public Vector3 pointSpawn;
    }
}
