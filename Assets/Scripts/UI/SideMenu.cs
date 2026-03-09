using System.Collections;
using UnityEngine;

namespace UI
{
    public class SideMenu : MonoBehaviour
    {
        [SerializeField] private GameObject openButton;
    
        private RectTransform panel;

        private float width;
        private const float Speed = 10f;
        private bool visible = false;

        private void Start()
        {
            panel = GetComponent<RectTransform>();
            width = panel.rect.width;
        }

        public void Toggle()
        {
            openButton.SetActive(visible);
            visible = !visible;
            StopAllCoroutines();
            StartCoroutine(Animate());
        }

        public void InstantClose()
        {
            openButton.SetActive(true);
            visible = false;
            StopAllCoroutines();
            panel.anchoredPosition = new Vector2(-width, panel.anchoredPosition.y);
        }

        private IEnumerator Animate()
        {
            Vector2 target = visible
                ? new Vector2(0, panel.anchoredPosition.y)
                : new Vector2(-width, panel.anchoredPosition.y);

            while (Vector2.Distance(panel.anchoredPosition, target) > 0.5f)
            {
                panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * Speed);
                yield return null;
            }
        
            panel.anchoredPosition = target;
        }
    }
}
