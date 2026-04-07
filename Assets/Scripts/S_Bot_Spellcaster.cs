using UnityEngine;
using UnityEngine.InputSystem;

public class S_Bot_Spellcaster : MonoBehaviour
{
    public Key spellCastKey;
    private bool _wasActivated = false;
    public Transform botPivot;
    public GameObject spellBulletPrefab;
    private GameObject _currentBullet;

    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Keyboard.current[spellCastKey].IsPressed() && !_wasActivated)
        {
            Debug.Log("Shoot");
            _wasActivated = true;
            CastSpell();
        }
    }

    public void CastSpell()
    {

        _currentBullet = Instantiate(spellBulletPrefab, botPivot);
        _animator.SetTrigger("Spell");


    }
    public void ResetCast()
    {
        _animator.ResetTrigger("Spell");
        _wasActivated = false;
    }
    public void ShootSpell()
    {

        _currentBullet.transform.rotation = transform.rotation;
        _currentBullet.GetComponent<S_SpellBullet>().ShootBullet();
        _currentBullet = null;
    }
}
