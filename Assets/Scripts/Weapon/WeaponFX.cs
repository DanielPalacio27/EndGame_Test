using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [SerializeField] ParticleSystem shootParticles = null;

    public void PlayFX(AudioClip _audioClip)
    {
        shootParticles.Play();
        AudioController.Instance.PlaySingle(_audioClip, false);
    }
}
