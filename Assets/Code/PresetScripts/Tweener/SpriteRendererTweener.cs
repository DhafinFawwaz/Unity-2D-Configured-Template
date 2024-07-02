using UnityEngine;
using UnityEngine.UI;

namespace DhafinFawwaz.Tweener
{
    public class SpriteRendererTweener : Tweener
    {
        [Header("Values")]
        [SerializeField] SpriteRenderer _target;
        [SerializeField] Color _end;
        Coroutine[] _coroutines = new Coroutine[0];

        public SpriteRendererTweener SetEnd(Color end)
        {
            _end = end;
            return this;
        }

        public override void Stop()
        {
            foreach (var c in _coroutines) StopCoroutineIfNull(c);
        }

        [ContextMenu("Play Color")]
        public void Color()
        {
            StopCoroutineIfNull(_coroutines[0]);
            _coroutines[0] = StartCoroutine(Tween<Color>(
                x => _target.color = x,
                _target.color,
                _end,
                _duration,
                UnityEngine.Color.LerpUnclamped
            ));
        }
    }

}
