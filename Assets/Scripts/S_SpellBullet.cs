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
    [Tooltip("Vai seguir a rotação daquilo que ele acertou, olhando para a direção da normal")]
    public bool followCollisionRotation = false;

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

            var vfx = Instantiate(onHitVFXPrefab, transform.position, GetRotation(other));
            vfx.GetComponentInChildren<VisualEffect>().Play();
            Destroy(vfx, onHitVFXDuration);
        }
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision other)
    { 
    }
    Quaternion GetRotation(Collider target)
    {
        if (followCollisionRotation)
        {
            Quaternion rot;
            float angle = Quaternion.Angle(transform.rotation, target.transform.rotation); 
            if(angle > 90 || angle < -90)
            {
                rot = Quaternion.Euler(0,angle,0);
            }
            else
            {
                rot = Quaternion.Euler(0, 180+angle, 0);
            }
            return rot;
        }
        else
        {
            return transform.rotation;
        }
    }
}
