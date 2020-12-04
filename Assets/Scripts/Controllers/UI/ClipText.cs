using UnityEngine;

namespace Controllers.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClipText : MonoBehaviour
    {
        [SerializeField] private Transform follow;
        [SerializeField] private Camera cam;

        // Update is called once per frame
        private void Update()
        {
            var namePos = cam.WorldToScreenPoint(follow.position);
            transform.position = namePos;
        }
    }
}