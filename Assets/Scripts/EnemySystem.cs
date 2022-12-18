using UnityEngine;

namespace KID
{
    /// <summary>
    /// �ĤH�t�ΡG�l�ܪ��a
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("���ʳt��"), Range(0, 10)]
        private float speed = 3.5f;
        [SerializeField, Header("����Z��"), Range(0, 10)]
        private float stopDistance = 1.5f;

        private string nameTarget = "�g��";
        private Transform traTarget;

        // ø�s�ϥܡG�Ȧb Unity �s�边����ܡA���U��
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.8f, 0.1f, 0.1f, 0.3f);       // �ϥ��C��
            Gizmos.DrawSphere(transform.position, stopDistance);    // ø�s�y��ϥ�
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
        /// �l��
        /// </summary>
        private void Track()
        {
            Vector3 posTarget = traTarget.position;
            Vector3 posCurrent = transform.position;
            
            Flip(posCurrent.x, posTarget.x);

            // �Z���p�󰱤�Z�� �N ���X���l��
            if (Vector3.Distance(posCurrent, posTarget) <= stopDistance) return;

            posCurrent = Vector3.MoveTowards(posCurrent, posTarget, speed * Time.deltaTime);
            transform.position = posCurrent;
        }

        /// <summary>
        /// ½��
        /// </summary>
        /// <param name="xCurrent">������ X</param>
        /// <param name="xTarget">�ؼЪ��� X</param>
        private void Flip(float xCurrent, float xTarget)
        {
            float angle = xCurrent > xTarget ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}
