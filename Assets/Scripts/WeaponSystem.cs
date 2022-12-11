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

        private void Awake()
        {
            SpawnWeapon();
        }

        /// <summary>
        /// 生成武器
        /// </summary>
        private void SpawnWeapon()
        {
            // 生成物件 = 生成(物件，座標，角度)
            // transform.position 此物件的座標
            // Quaternion.identity 零角度
            GameObject tempWeapon = Instantiate(
                weaponData.prefabWeapon,
                transform.position + weaponData.weaponObjects[0].pointSpawn,
                Quaternion.identity);

            // 生成物件.取得元件<2D 剛體>().添加推力(武器資料的武器速度)
            tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponData.weaponObjects[0].speed);
        }
    }
}
