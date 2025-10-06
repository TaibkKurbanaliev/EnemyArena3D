using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;


public class Gun : MonoBehaviour
{
    private const string FIRE = "Fire";

    [SerializeField] private float _shootDelay;
    [SerializeField] private float _shootRange;
    [SerializeField] private float _damage;

    [SerializeField] private LayerMask _targets;

    [Header("Effects")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _muzzleEffect;


    private bool _canShoot = true;

    public void Fire()
    {

        if (_canShoot)
        {
            var _screenCenterPosition = new Vector2(Screen.width / 2, Screen.height / 2);
            var ray = Camera.main.ScreenPointToRay(_screenCenterPosition);
            _canShoot = false;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _shootRange, _targets))
            {
                if (hitInfo.collider.TryGetComponent<IDamagable>(out IDamagable target))
                {
                    target.TakeDamage(_damage);
                }
            }

            _audioSource.PlayOneShot(_fireSound);
            _muzzleEffect.Play();
            
            PlayAnim();
            StartCoroutine("ShootDelayTimer");
        }
    }

    public void PlayAnim()
    {
        _animator.Play(FIRE);
    }

    private IEnumerator ShootDelayTimer()
    {
        yield return new WaitForSeconds(_shootDelay);

        _canShoot = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var _screenCenterPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = Camera.main.ScreenPointToRay(_screenCenterPosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _shootRange, _targets))
            Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
        else
            Debug.DrawRay(ray.origin, ray.direction * _shootRange, Color.red);
    }

#endif
}
