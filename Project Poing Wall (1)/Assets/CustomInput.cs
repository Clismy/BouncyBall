using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput : MonoBehaviour
{
    public delegate void OnInput();
    public static OnInput OnInputBegan;
    public static OnInput OnInputEnded;

    ReturnedInput returnedInput;


    private Vector2 inputBeganPos;
    private Vector2 inputDownWorldPos;
    private Vector2 updatedInputDirection;
    private InputPhase inputPhase;
    private Vector3 startPos;
    bool hasStartedInput = false;
    [SerializeField] bool debugMode = false;

    enum InputPhase
    {
        None,
        Started,
        Released,
        Holding
    }

    struct ReturnedInput
    {
        public Vector2 startPos;
        public Vector2 worldStartPos;
        public Vector2 dir;
        public Vector2 currentWorldPos;
        public InputPhase typeOfInput;
    }

    //public void CheckInput()
    //{
    //    returnedInput = GetInput(returnedInput);
    //}

    //public Vector2 GetNewVelocity(Vector2 oldVel)
    //{
    //    if(returnedInput.typeOfInput == InputPhase.Released)
    //    {
    //        if (returnedInput.dir.sqrMagnitude != 0) //&& Swipes.instance.CurrentSwipes > 0
    //        {
    //            Swipes.instance.DecreaseSwipe();
    //            returnedInput.typeOfInput = InputPhase.None;
    //            hasStartedInput = false;
    //            return returnedInput.dir * oldVel.magnitude;
    //        }
    //    }
    //    return oldVel;
    //}

    public Vector2 GetNewVelocity(Vector2 oldVel)
    {
        if (inputPhase == InputPhase.Released)
        {
            // Invoke other funcs

            Swipes.instance.DecreaseSwipe();
            inputPhase = InputPhase.None;
            hasStartedInput = false;
            return updatedInputDirection * oldVel.magnitude;
        }
        return oldVel;
    }

    public void GetInput()
    {
        if (Input.touchCount > 0 && !debugMode || debugMode)
        {
            Touch touch = debugMode == false ? Input.GetTouch(0) : new Touch();

            if (touch.phase == TouchPhase.Began && !debugMode || Input.GetMouseButtonDown(0) && debugMode)
            {
                hasStartedInput = true;
                inputBeganPos = debugMode == false ? touch.position : (Vector2)Input.mousePosition;
                inputPhase = InputPhase.Started;
            }
            else if (touch.phase == TouchPhase.Ended && !debugMode || Input.GetMouseButtonUp(0) && debugMode)
            {
                hasStartedInput = false;
                var endPos = debugMode == false ? touch.position : (Vector2)Input.mousePosition;
                updatedInputDirection = ((Vector2)endPos - inputBeganPos).normalized;
                inputPhase = InputPhase.Released;
            }

            if (hasStartedInput)
            {
                Vector2 inputTemp = debugMode == false ? touch.position : (Vector2)Input.mousePosition;
                inputDownWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(inputTemp.x, inputTemp.y, 10));
                inputPhase = InputPhase.Holding;
            }
        }
    }
}