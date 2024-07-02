using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

enum SpriteFitMode
{
    None,
    FitWidth,
    FitHeight,
}

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class AchoredSpriteRenderer : MonoBehaviour
{
    [SerializeField] bool _enable = true;
    public bool Enable { get => _enable; set => _enable = value; }

    [SerializeField] SpriteFitMode _fitMode = SpriteFitMode.None;


    [SerializeField] Vector2 _screenPosition;
    public Vector2 ScreenPosition { get => _screenPosition; set => _screenPosition = value; }

    [SerializeField] Vector2 _anchorPoint = new Vector2(0.5f, 0.5f);
    public Vector2 AnchorPoint { get => _anchorPoint; set => _anchorPoint = value; }

    SpriteRenderer _spriteRenderer;
    public Bounds Bounds { get => _spriteRenderer.bounds; }
    Camera _mainCam;


    public Vector2 AnchoredScreenPosition
    {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main;
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

            return _anchorPoint;
        }
        set
        {
            _screenPosition = value;
            _anchorPoint = value;
            Set(_screenPosition, _anchorPoint);
        }
    }
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCam = Camera.main;
    }

    public void Set(Vector2 screenPosition, Vector2 anchorPoint)
    {
        _screenPosition = screenPosition;
        _anchorPoint = anchorPoint;
        Vector2 worldPos = GetWorldPosition();
        Vector2 offset = GetOffset();
        transform.position = worldPos - offset;
    }

    public void SetAnchoredPosition(Vector2 anchoredPosition) => Set(anchoredPosition, anchoredPosition);
    public void SetAnchoredPositionX(float anchoredPositionX) => Set(new Vector2(anchoredPositionX, _screenPosition.y), new Vector2(anchoredPositionX, _anchorPoint.y));
    public void SetAnchoredPositionY(float anchoredPositionY) => Set(new Vector2(_screenPosition.x, anchoredPositionY), new Vector2(_anchorPoint.x, anchoredPositionY));

    public Vector2 GetScreenPosition()
    {
        if (_mainCam == null) _mainCam = Camera.main;
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        return new Vector2(_mainCam.pixelWidth * _screenPosition.x, _mainCam.pixelHeight * _screenPosition.y);
    }

    public Vector2 GetAnchorWorldPosition()
    {
        if (_mainCam == null) _mainCam = Camera.main;
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        return _mainCam.ScreenToWorldPoint(GetScreenPosition() + new Vector2(_spriteRenderer.bounds.size.x * (_anchorPoint.x - 0.5f), _spriteRenderer.bounds.size.y * (_anchorPoint.y - 0.5f)));
    }

    public Vector2 GetWorldPosition()
    {
        if (_mainCam == null) _mainCam = Camera.main;
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        return _mainCam.ScreenToWorldPoint(GetScreenPosition());
    }

    public Vector2 GetOffset()
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        return new Vector2(_spriteRenderer.bounds.size.x * (_anchorPoint.x-0.5f), _spriteRenderer.bounds.size.y * (_anchorPoint.y-0.5f));
    }

    public void UpdateFit()
    {
        if(_fitMode == SpriteFitMode.FitWidth)
        {
            transform.localScale = Vector2.one;
            float scale = _mainCam.orthographicSize * 2 * _mainCam.aspect / _spriteRenderer.bounds.size.x;
            transform.localScale = new Vector3(scale, scale, 1);
        }
        else if(_fitMode == SpriteFitMode.FitHeight)
        {
            transform.localScale = Vector2.one;
            float scale = _mainCam.orthographicSize * 2 / _spriteRenderer.bounds.size.y;
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if(!_enable) return;
        if (_mainCam == null) _mainCam = Camera.main;
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        Set(_screenPosition, _anchorPoint);

        Handles.color = Color.red;
        Vector2 pos = GetAnchorWorldPosition();
        Handles.DrawSolidDisc(pos, Vector3.forward, 0.2f);
        Handles.DrawWireDisc (pos, Vector3.forward, 0.3f);

        UpdateFit();
    }
#endif
}