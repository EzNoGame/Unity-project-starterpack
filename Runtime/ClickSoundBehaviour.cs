using UnityEngine;
using UnityEngine.UI;
public class ClickSoundBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickSound;
    protected void PlayClickSound()
    {
        AbstractAudioSystem.Instance.PlaySFX(clickSound);
    }

    public virtual void OnClick()
    {
        PlayClickSound();
    }
}