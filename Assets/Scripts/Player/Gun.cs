using Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField][Range(0, 100)] private float _attackRange;
    [SerializeField] private int _shootDamage;
    [SerializeField] private float _shootDelay;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private GameObject _shootEffectPrefab;
    [SerializeField] private GameObject _fireEffectPrefab;
    [SerializeField] private Transform _muzzlePoint;

    private CinemachineImpulseSource _impulse;

    private Camera _camera;

    private bool _canShoot { get => _currentCount <= 0; }
    private float _currentCount;

    private void Awake() => Init();

    private void Update() => HandleCanShoot();

    private void Init()
    {
        _camera = Camera.main;
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    public bool Shoot()
    {
        if (!_canShoot) return false;

        PlayShootSound();
        PlayCameraEffect();
        PlayShootEffect();
        _currentCount = _shootDelay;

        PlayFireEffect(_muzzlePoint);

        RaycastHit hit;
        IDamagable target = RayShoot(out hit);

        if (!hit.Equals(default)) PlayShootEffect(hit.point, Quaternion.LookRotation(hit.normal));


        if (target == null) return true;

        target.TakeDamage(_shootDamage);

        return true;
    }

    private void HandleCanShoot()
    {
        if (_canShoot) return;

        _currentCount -= Time.deltaTime;
    }

    private IDamagable RayShoot(out RaycastHit hitTarget)
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _attackRange, _targetLayer))
        {
            hitTarget = hit;
            return ReferenceRegistry.GetProvider(hit.collider.gameObject)?.GetAs<NormalMonster>();
        }
        else 
        {
            hitTarget = default;
            return null;
        }
    }

    private void PlayShootEffect(Vector3 position, Quaternion rotation)
    {
        Instantiate(_shootEffectPrefab, position, rotation);
    }

    private void PlayFireEffect(Transform transform)
    {
        GameObject instacne = Instantiate(_fireEffectPrefab, transform.position, Quaternion.identity);
        instacne.transform.rotation = transform.rotation;
        Destroy(instacne, 0.15f);
    }

    private void PlayShootSound()
    {
        SFXController sfx = GameManager.Instance.Audio.GetSFX();
        sfx.Play(_shootSFX);
    }

    private void PlayCameraEffect()
    {
        _impulse.GenerateImpulse();
    }

    private void PlayShootEffect()
    {

    }
}
