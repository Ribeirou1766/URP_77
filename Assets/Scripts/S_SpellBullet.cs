using UnityEngine;
using UnityEngine.VFX;

public class S_SpellBullet : MonoBehaviour
{
    bool wasInitialized = false;
    public float bulletSpeed = 2;
    public Vector3 currentDirection;

    [Header("Visual Effects")]
    public GameObject onHitVFXPrefab;
    public float onHitVFXDuration = 1;

    void OnEnable()
    {
        
    }
    public void ShootBullet(Vector3 direction)
    {
        wasInitialized = true;
        currentDirection = direction;
        transform.SetParent(null);
    }
    void FixedUpdate()
    {
        if (!wasInitialized) return;

        transform.position += currentDirection * bulletSpeed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!wasInitialized) return;

        if (onHitVFXPrefab)
        {
            var vfx = Instantiate(onHitVFXPrefab, transform.position, transform.rotation);
            vfx.GetComponentInChildren<VisualEffect>().Play();
            Destroy(vfx,onHitVFXDuration);
        }
        Destroy(this.gameObject);
    }
}
