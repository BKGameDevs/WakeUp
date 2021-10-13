using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WakeUp.Constants
{

    public static class PlayerInput
    {
        public static bool IsPressingEnter
        {
            get
            {
                return Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return);
            }
        }
        public static bool IsPressingLeft
        {
            get
            {
                return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.LeftArrow);
            }
        }

        public static bool IsPressingRight
        {
            get
            {
                return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Semicolon) || Input.GetKey(KeyCode.RightArrow);
            }
        }

        public static bool IsPressingUp
        {
            get
            {
                return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.UpArrow);
            }
        }

        public static bool IsPressingDown
        {
            get
            {
                return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.DownArrow);
            }
        }

        public static bool IsPressingSpace
        {
            get
            {
                return Input.GetKey(KeyCode.Space);
            }
        }

        public static bool IsReleasingSpace
        {
            get
            {
                return Input.GetKeyUp(KeyCode.Space);
            }
        }

        public static bool IsPressingEscape
        {
            get
            {
                return Input.GetKey(KeyCode.Escape);
            }
        }

        public static bool IsPressingInteract
        {
            get
            {
                return Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.F);
            }
        }

    }
}
