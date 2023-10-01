using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private bool lastFrameAttack;

    public Vector2 MovementAxis { get; private set; }
    public Vector2 LookAxis { get; private set; }

    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackStop;

    private void Update()
    {
        CheckAttack();
        CheckMovement();
        CheckLook();
    }

    private void CheckAttack()
    {
        bool attackInput = Input.GetMouseButton(0);
        //may eventually need to check if over ui
        //bool overUI = EventSystem.current.IsPointerOverGameObject();

        if (attackInput && !lastFrameAttack) OnAttackStart?.Invoke();
        if (!attackInput && lastFrameAttack) OnAttackStop?.Invoke();

        lastFrameAttack = attackInput;
    }

    private void CheckMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        MovementAxis = new Vector2(x, y);
    }

    private void CheckLook()
    {
        LookAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}
