using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCast : MonoBehaviour
{

    public Key SpellCastKey;
    private Animator Animator;
    private bool SpellCastWasPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[SpellCastKey].isPressed & SpellCastWasPressed == false)
        {
            Animator.SetTrigger("Spell");
            SpellCastWasPressed=true;
        }
        else
        {
            SpellCastWasPressed = false;
        }
    }
}
