using UnityEngine;

namespace DhafinFawwaz.Tweener
{
    public class CanvasGroupTweener : Tweener
    {
        [Header("Values")]
        [SerializeField] CanvasGroup _target;
        [SerializeField] float _end;
        Coroutine[] _coroutines = new Coroutine[1];

        public override void Stop()
        {
            foreach (var c in _coroutines) StopCoroutineIfNull(c);
        }

        [ContextMenu("Play Alpha")]
        public void Alpha()
        {
            StopCoroutineIfNull(_coroutines[0]);
            _coroutines[0] = StartCoroutine(Tween<float>(
                x => _target.alpha = x,
                _target.alpha,
                _end,
                _duration,
                Mathf.LerpUnclamped
            ));
        }
    }

}
