using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource themePlayer;
    [SerializeField] private AudioDistortionFilter themeDistortionFilter;

    [SerializeField] private AudioSource bulletShotSoundsPlayer;
    [SerializeField] private AudioSource bulletColisionSoundsPlayer;
    [SerializeField] private AudioClip[] bulletShotSounds;
    [SerializeField] private AudioClip[] bulletColisionSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        bulletShotSounds = Resources.LoadAll<AudioClip>(@"Sounds\ManVoices");
        bulletColisionSounds = Resources.LoadAll<AudioClip>(@"Sounds\WomanVoices");
    }

    public void ChangeDistortion()
    {
        StartCoroutine(ChangeDistortionRoutine());
    }

    public IEnumerator ChangeDistortionRoutine()
    {
        themeDistortionFilter.distortionLevel = 1f;
        yield return new WaitForSeconds(.5f);
        themeDistortionFilter.distortionLevel = .5f;
    }

    public void BulletShot() => bulletShotSoundsPlayer.PlayOneShot(bulletShotSounds[Random.Range(0, bulletShotSounds.Length)]);

    public void BulletShotSide(float side) => bulletShotSoundsPlayer.panStereo = side;

    public void BulletColision() => bulletColisionSoundsPlayer.PlayOneShot(bulletColisionSounds[Random.Range(0, bulletColisionSounds.Length)]);
}
