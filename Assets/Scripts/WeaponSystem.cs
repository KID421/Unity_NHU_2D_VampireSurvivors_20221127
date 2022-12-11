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

        private void Awake()
        {
            SpawnWeapon();
        }

        /// <summary>
        /// �ͦ��Z��
        /// </summary>
        private void SpawnWeapon()
        {
            // �ͦ����� = �ͦ�(����A�y�СA����)
            // transform.position �����󪺮y��
            // Quaternion.identity �s����
            GameObject tempWeapon = Instantiate(
                weaponData.prefabWeapon,
                transform.position + weaponData.weaponObjects[0].pointSpawn,
                Quaternion.identity);

            // �ͦ�����.���o����<2D ����>().�K�[���O(�Z����ƪ��Z���t��)
            tempWeapon.GetComponent<Rigidbody2D>().AddForce(weaponData.weaponObjects[0].speed);
        }
    }
}
