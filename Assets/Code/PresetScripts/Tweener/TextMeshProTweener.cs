using System;
using TMPro;
using UnityEngine;

namespace DhafinFawwaz.Tweener
{
    public class TextMeshProTweener : Tweener
    {
        [Header("Values")]
        [SerializeField] TMP_Text _target;
        [SerializeField] string _text;
        [SerializeField] long _start;
        [SerializeField] long _end;
        [SerializeField] string _numericFormat = "N0";
        [SerializeField] string _textFormat = "{0}";
        Coroutine[] _coroutines = new Coroutine[1];

        public override void Stop()
        {
            foreach (var c in _coroutines) StopCoroutineIfNull(c);
        }

        TextMeshProTweener SetStart(long start)
        {
            _start = start;
            return this;
        }
        TextMeshProTweener SetEnd(long end)
        {
            _end = end;
            return this;
        }
        TextMeshProTweener SetFormat(string format)
        {
            _numericFormat = format;
            return this;
        }

        [ContextMenu("Play Numeric")]
        public void Numeric()
        {
            _target.maxVisibleCharacters = int.MaxValue;
            StopCoroutineIfNull(_coroutines[0]);
            _coroutines[0] = StartCoroutine(Tween<long>(
                x => _target.text = String.Format(_textFormat, x.ToString(_numericFormat)),
                _start,
                _end,
                _duration,
                LerpUnclamped
            ));
        }

        long LerpUnclamped(long a, long b, float t)
        {
            return a + (long)((b - a) * t);
        }

        [ContextMenu("Play Text")]
        public void Text()
        {
            StopCoroutineIfNull(_coroutines[0]);
            _target.text = _text;
            _coroutines[0] = StartCoroutine(Tween<int>(
                x => _target.maxVisibleCharacters = x,
                0,
                _target.text.Length,
                _duration,
                LerpUnclamped
            ));
        }

        int LerpUnclamped(int a, int b, float t)
        {
            return a + (int)((b - a) * t);
        }
    }

}
