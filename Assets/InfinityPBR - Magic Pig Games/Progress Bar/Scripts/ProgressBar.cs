using System;
using System.Collections;
using UnityEngine;

namespace MagicPigGames
{
    [Serializable]
    public class ProgressBar : MonoBehaviour
    {
        [Header("Overlay Bar")]
        [Tooltip("This is the bar that moves to show progress, covering the fill bar.")]
        public RectTransform overlayBar;
        [Tooltip("Width as a percent of the Progress Bar Rect Transform.")]
        [Range(0f, 1f)] public float sizeMin = 0f;
        [Tooltip("Width as a percent of the Progress Bar Rect Transform.")]
        [Range(0f, 1f)] public float sizeMax = 1f;

        [Header("Options")]
        [Tooltip("When true, the progress bar width will grow as the progress increases. When false, the progress bar width will shrink as the progress increases.")]
        public bool invertProgress = true;
        [Tooltip("When 0, the progress bar will update immediately. When greater than 0, the progress bar will take this many seconds to update.")]
        [Min(0f)]
        public float transitionTime = 0f;

        [Header("Plumbing")]
        public RectTransform rectTransform;

        public Transform player; // Assign this in the inspector
        public Transform targetObject; // Assign this in the inspector
        public float maxDistance = 10.0f; // The maximum distance for full speed depletion

        private float _progress = 0f; // This is the runtime value for progress!
        protected float _elapsedTime = 0f;
        protected float _lastProgress = 0f;
        protected Vector2 _lastParentSize;
        protected Coroutine _transitionCoroutine;
        public Light spotlight;
        public FlaskScript flask; // Reference to the current flask script

        private float countdownTimer = 20.0f; // 20 seconds duration
        private float maxTimer = 20.0f; // Maximum value of the timer

        protected virtual float SizeAtCurrentProgress 
            => Mathf.Lerp(SizeMin, SizeMax, _progress);

        public virtual float Progress => _progress; // The 0-1 value of the progress bar.
        public virtual float ProgressPercent => _progress * 100f; // The 0-100 value of the progress bar.

        protected virtual float SizeMin => rectTransform.sizeDelta.x * sizeMin; // Minimum size of the overlay bar.
        protected virtual float SizeMax => rectTransform.sizeDelta.x * sizeMax; // Maximum size of the overlay bar.
        protected virtual float CurrentOverlaySize => overlayBar.sizeDelta.x; // Current size of the overlay bar.

        protected virtual void Start()
        {
            SetProgress(1f); // Start with full health
        }

        protected virtual void Update()
        {
                if (Input.GetKeyDown(KeyCode.E) && flask.IsPlayerNearby())
        {
                countdownTimer = maxTimer;
                SetProgress(1f); // Refill the progress bar
                flask.ConsumeFlask(); // Despawn the flask
                return;
        }
            

            // Base depletion rate
            float baseDepletionRate = 0.5f; // Adjust this value as needed

            // Adjust additional depletion rate based on distance to the object
            float distance = Vector3.Distance(player.position, targetObject.position);
            distance = Mathf.Clamp(distance, 0, maxDistance);

            // Calculate additional depletion rate based on proximity
            float proximityDepletionRate = (1 - (distance / maxDistance)) * baseDepletionRate;

            // Total depletion rate
            float totalDepletionRate = baseDepletionRate + proximityDepletionRate;

            // Decrease the timer and update the progress
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime * totalDepletionRate;
                float newProgress = countdownTimer / maxTimer; // Map the timer value to a 0-1 range
                SetProgress(newProgress);
            }
            else
            {
                // Ensure the progress doesn't go below zero
                SetProgress(0f);
             // Disable the spotlight when progress reaches 0
            if (spotlight != null)
            {
                spotlight.enabled = false;
            }
            }

            HandleParentSizeChange();
        }


        public virtual void SetProgress(float progress)
        {
            if (progress is > 1 or < 0)
            {
                Debug.LogError($"Progress value must be between 0 and 1. Value was {progress}. Will clamp.");
                progress = Mathf.Clamp(progress, 0, 1);
            }

            _progress = ValueBasedOnInvert(progress);
            _lastProgress = _progress;

            if (transitionTime <= 0f)
            {
                SetBarValue(SizeAtCurrentProgress);
                return;
            }

            StartTheCoroutine();
        }

        protected float ValueBasedOnInvert(float value) => invertProgress ? 1 - value : value;

        private void StartTheCoroutine()
        {
            if (_transitionCoroutine != null)
            {
                _elapsedTime = transitionTime - _elapsedTime;
                StopCoroutine(_transitionCoroutine);
            }
            else
                _elapsedTime = 0f;

            _transitionCoroutine = StartCoroutine(TransitionProgress(CurrentOverlaySize));
        }

        protected virtual void CheckOverlayBarRectTransform()
        {
            if (overlayBar == null) return;
            if (overlayBar.anchorMin == new Vector2(1, 0)
                && overlayBar.anchorMax == new Vector2(1, 1)) return;

            overlayBar.anchorMin = new Vector2(1, 0); // Anchor to top right corner
            overlayBar.anchorMax = new Vector2(1, 1); // Stretch vertically
        }

        protected virtual void HandleParentSizeChange()
        {
            if (_lastParentSize == rectTransform.sizeDelta)
                return;

            _lastParentSize = rectTransform.sizeDelta;

            var progress = ValueBasedOnInvert(_progress);
            SetProgress(progress);
        }

        protected virtual IEnumerator TransitionProgress(float startWidth)
        {
            while (_elapsedTime < transitionTime)
            {
                var newWidth = Mathf.Lerp(startWidth, SizeAtCurrentProgress, _elapsedTime / transitionTime);
                SetBarValue(newWidth);

                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            _transitionCoroutine = null;
        }

        protected virtual void SetBarValue(float value)
        {
            var sizeDelta = overlayBar.sizeDelta;
            sizeDelta = new Vector2(value, sizeDelta.y);
            overlayBar.sizeDelta = sizeDelta;
        }

        protected virtual void OnValidate()
        {
            sizeMin = Mathf.Clamp(sizeMin, 0, sizeMax - 0.01f);
            sizeMax = Mathf.Clamp(sizeMax, sizeMin + 0.01f, 1);

            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            if (overlayBar == null)
                Debug.Log("Please assign the Overlay Bar RectTransform.");

            CheckOverlayBarRectTransform();

            _lastParentSize = rectTransform.sizeDelta;
        }
    }
}
