using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoaderSpinner : MonoBehaviour
    {
        private Image image;
    
        void Start()
        {
            image = GetComponent<Image>();
        }
    
        void Update()
        {
            image.transform.Rotate(0, 0, -200 * Time.deltaTime);    
        }
    }
}
