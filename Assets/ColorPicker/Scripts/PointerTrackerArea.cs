#region Includes
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace TS.ColorPicker
{
    [RequireComponent(typeof(RectTransform))]
    public class PointerTrackerArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables

        private const string TRACK_COROUTINE = "TrackCoroutine";

        public delegate void OnDrag(PointerTrackerArea sender, Vector2 position);
        public OnDrag Drag;

        private RectTransform _transform;
        private Canvas _parentCanvas;

        #endregion

        private void Awake()
        {
            _transform = transform as RectTransform;
            _parentCanvas = GetComponentInParent<Canvas>();
        }

        public Vector2 Normalize(Vector2 position)
        {
            return new Vector2(position.x / _transform.rect.width, position.y / _transform.rect.height);
        }
        public Vector2 DeNormalize(Vector2 position)
        {
            return new Vector2(position.x * _transform.rect.width, position.y * _transform.rect.height);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(TRACK_COROUTINE);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine(TRACK_COROUTINE);
        }

        private IEnumerator TrackCoroutine()
        {
            while (true)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, Input.mousePosition, _parentCanvas.worldCamera, out Vector2 position);

                var rect = _transform.rect;
                position.x = Mathf.Clamp(position.x, rect.min.x, rect.max.x);
                position.y = Mathf.Clamp(position.y, rect.min.y, rect.max.y);

                Drag?.Invoke(this, position);

                yield return 0;
            }
        }
    }
}