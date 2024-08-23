using UnityEngine;

namespace Services.WindowService
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform _safeAreaTransform;

        private void Awake()
        {
            _safeAreaTransform = GetComponent<RectTransform>();
        }

        public void Fit()
        {
            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position / new Vector2(Screen.width, Screen.height);
            var anchorMax = (safeArea.position + safeArea.size) / new Vector2(Screen.width, Screen.height);

            _safeAreaTransform.anchorMin = anchorMin;
            _safeAreaTransform.anchorMax = anchorMax;
        }
    }
}