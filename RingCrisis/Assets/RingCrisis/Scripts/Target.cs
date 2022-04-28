using UnityEngine;

namespace RingCrisis
{
    /// <summary>
    /// ターゲットそのものの処理を担うコンポーネント
    /// </summary>
    [DisallowMultipleComponent]
    public class Target : MonoBehaviour
    {
        private static readonly float LifeTime = 8.0f;

        [SerializeField]
        private int _score = 0;

        public int Score => _score;

        private void Update()
        {
        }
    }
}
