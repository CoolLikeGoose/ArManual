using System.Collections;
using UnityEngine;

namespace UI
{
    public class SideMenu : MonoBehaviour
    {
        [SerializeField] private GameObject openButton;
    
        private RectTransform _panel;

        private float _width;
        private const float Speed = 10f;
        private bool _visible = false;

        private void Start()
        {
            _panel = GetComponent<RectTransform>();
            _width = _panel.rect.width;
        }

        public void Toggle()
        {
            openButton.SetActive(_visible);
            _visible = !_visible;
            StopAllCoroutines();
            StartCoroutine(Animate());
        }

        public void InstantClose()
        {
            openButton.SetActive(true);
            _visible = false;
            StopAllCoroutines();
            _panel.anchoredPosition = new Vector2(-_width, _panel.anchoredPosition.y);
        }

        private IEnumerator Animate()
        {
            Vector2 target = _visible
                ? new Vector2(0, _panel.anchoredPosition.y)
                : new Vector2(-_width, _panel.anchoredPosition.y);

            while (Vector2.Distance(_panel.anchoredPosition, target) > 0.5f)
            {
                _panel.anchoredPosition = Vector2.Lerp(_panel.anchoredPosition, target, Time.deltaTime * Speed);
                yield return null;
            }
        
            _panel.anchoredPosition = target;
        }
    }
}
