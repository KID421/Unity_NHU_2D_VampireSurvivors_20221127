using UnityEngine;

namespace KID
{
    /// <summary>
    /// �Z���t��
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("�Z�����")]
        private WeaponData weaponData;
        [SerializeField]
        private int level;

        private WeaponLevelData weaponLevel => weaponData.weaponLevelDatas[level];

        private void Awake()
        {
            // SpawnWeapon();
            // ���ƩI�s(��k�W�١A����ɶ��A�����W�v)
            InvokeRepeating("SpawnWeapon", 0, weaponLevel.intervalSpawn);
        }

        /// <summary>
        /// �ͦ��Z��
        /// </summary>
        private void SpawnWeapon()
        {
            WeaponObject[] weaponObject = weaponLevel.weaponObjects;

            for (int i = 0; i < weaponObject.Length; i++)
            {
                // �ͦ����� = �ͦ�(����A�y�СA����)
                // transform.position �����󪺮y��
                // Quaternion.identity �s����
                GameObject tempWeapon = Instantiate(
                    weaponData.prefabWeapon,
                    transform.position + weaponObject[i].pointSpawn,
                    Quaternion.identity);

                // �ͦ�����.���o����<2D ����>().�K�[���O(�Z����ƪ��Z���t��)
                tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponObject[i].speed);
            }
        }
    }
}
