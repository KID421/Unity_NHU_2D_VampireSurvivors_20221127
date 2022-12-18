using UnityEngine;

namespace KID
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private WeaponData weaponData;
        [SerializeField]
        private int level;

        private WeaponLevelData weaponLevel => weaponData.weaponLevelDatas[level];

        private void Awake()
        {
            // SpawnWeapon();
            // 重複呼叫(方法名稱，延遲時間，重複頻率)
            InvokeRepeating("SpawnWeapon", 0, weaponLevel.intervalSpawn);
        }

        /// <summary>
        /// 生成武器
        /// </summary>
        private void SpawnWeapon()
        {
            WeaponObject[] weaponObject = weaponLevel.weaponObjects;

            for (int i = 0; i < weaponObject.Length; i++)
            {
                // 生成物件 = 生成(物件，座標，角度)
                // transform.position 此物件的座標
                // Quaternion.identity 零角度
                GameObject tempWeapon = Instantiate(
                    weaponData.prefabWeapon,
                    transform.position + transform.TransformDirection(weaponObject[i].pointSpawn),
                    Quaternion.identity);

                Vector2 speedMove;

                if (weaponData.withCharacterDirection) speedMove = transform.TransformDirection(weaponObject[i].speed);
                else speedMove = weaponObject[i].speed;

                // 生成物件.取得元件<2D 剛體>().添加推力(武器資料的武器速度)
                tempWeapon.GetComponent<Rigidbody2D>().AddForce(speedMove);
            }
        }
    }
}
