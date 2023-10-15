using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private bool lastFrameAttack;

    public Vector2 MovementAxis { get; private set; }
    public int MovementVertical { get; private set; }
    public Vector2 LookAxis { get; private set; }

    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackStop;
    public UnityEvent OnPausePressed;

    private void Update()
    {
        CheckAttack();
        CheckMovement();
        CheckLook();
        CheckPause();
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
        int up = Input.GetKey(KeyCode.Space) ? 1 : 0;
        int down = Input.GetKey(KeyCode.LeftShift) ? -1 : 0;

        MovementAxis = new Vector2(x, y);
        MovementVertical = up + down;
    }

    private void CheckLook()
    {
        LookAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePressed?.Invoke();
        }
    }
}
