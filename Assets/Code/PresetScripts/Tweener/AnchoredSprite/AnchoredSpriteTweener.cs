using System.Collections;
using System.Collections.Generic;
using DhafinFawwaz.Tweener;
using UnityEngine;
namespace DhafinFawwaz.Tweener
{
    public class AnchoredSpriteTweener : Tweener
    {
        [Header("Values")]
        [SerializeField] AchoredSpriteRenderer _target;
        [SerializeField] Vector2 _end;
        Coroutine[] _coroutines = new Coroutine[1];

        public override void Stop()
        {
            foreach (var c in _coroutines) StopCoroutineIfNull(c);
        }

        [ContextMenu("Play AnchoredScreenPosition")]
        public void AnchoredScreenPosition()
        {
            StopCoroutineIfNull(_coroutines[0]);
            _coroutines[0] = StartCoroutine(Tween<Vector2>(
                x => _target.AnchoredScreenPosition = x,
                _target.AnchoredScreenPosition,
                _end,
                _duration,
                Vector2.LerpUnclamped
            ));
        }
    }

}
