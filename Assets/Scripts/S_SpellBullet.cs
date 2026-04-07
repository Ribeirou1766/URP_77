using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody))]
public class S_SpellBullet : MonoBehaviour
{
    public float bulletSpeed = 2;
    bool wasInitialized = false;

    [Header("Visual Effects")]
    public GameObject onHitVFXPrefab;
    public float onHitVFXDuration = 1;


    //Refs
    Collider _collider;
    void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }
    public void ShootBullet()
    {
        wasInitialized = true;
        transform.SetParent(null);
    }
    void FixedUpdate()
    {
        if (!wasInitialized) return;

        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (!wasInitialized) return;

        if (onHitVFXPrefab)
        {
            var vfx = Instantiate(onHitVFXPrefab, transform.position, transform.rotation);
            vfx.GetComponentInChildren<VisualEffect>().Play();
            Destroy(vfx, onHitVFXDuration);
        }
        Destroy(this.gameObject);
    }
}
