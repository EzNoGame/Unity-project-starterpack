using UnityEngine;

/// <summary>
/// GameObject that act like a buttton,
/// highly customizable.
/// </summary>
/// 

public enum ButtonState
{
    ZoomIn,
    Idle,
    Onclick,
    ZoomOut,
    Selected,
}

public class MyButton : ClickSoundBehaviour
{

    protected bool _interactable = false, _zoomable = true;
    protected ButtonState state = ButtonState.Idle;
    protected Animator _animator;
    protected Color _initColor;

    protected virtual void OnMouseEnter()
    {
        state = (_interactable && _zoomable) ? ButtonState.ZoomIn : ButtonState.Idle;
    }
    protected virtual void OnMouseExit()
    {
        state = (_interactable && _zoomable) ? ButtonState.ZoomOut : ButtonState.Idle;
    }
    protected virtual void OnMouseDown()
    {
        state = _interactable ? ButtonState.Onclick : ButtonState.Idle;
    }

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _initColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    public void SetInteractable(bool value)
    {
        _interactable = value;
        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = _interactable ? Color.white : Color.gray;
        }
    }

    public virtual void Update() {
        switch (state) {
            case ButtonState.ZoomIn:
                ZoomIn();
                break;
            case ButtonState.Onclick:
                OnClick();
                break;
            case ButtonState.Selected:
                break;
            case ButtonState.ZoomOut:
                ZoomOut();
                break;
        }
    }


    public virtual void ZoomIn()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), Time.deltaTime * 5);
    }

    public virtual void ZoomOut()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 5);
    }

    public override void OnClick()
    {
        PlayClickSound();
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), Time.deltaTime * 5);
    }
}