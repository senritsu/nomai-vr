﻿using UnityEngine;

namespace NomaiVR {
    public class HoldPrompts: MonoBehaviour {
        Transform _holdTransform;

        void Awake () {
            var canvas = GameObject.Find("ScreenPromptCanvas").GetComponent<Canvas>();
            canvas.transform.localScale = Vector3.one * 0.0015f;
            canvas.transform.localPosition = Vector3.zero;
            canvas.transform.localRotation = Quaternion.identity;

            canvas.renderMode = RenderMode.WorldSpace;
            canvas.transform.localPosition = Vector3.zero;
            canvas.transform.localRotation = Quaternion.identity;

            _holdTransform = Hands.HoldObject(canvas.transform, Hands.RightHand, new Vector3(-0.09f, -0.11f, 0.13f));

            foreach (Transform child in canvas.transform) {
                child.localPosition = Vector3.zero;
            }

            var conditionalRenderer = canvas.gameObject.AddComponent<ConditionalRenderer>();
            conditionalRenderer.getShouldRender += ShouldRender;
        }

        bool ShouldRender () {
            return Common.ToolSwapper.IsInToolMode(ToolMode.None) || Common.ToolSwapper.IsInToolMode(ToolMode.Item);
        }

        void Update () {
            _holdTransform.LookAt(2 * _holdTransform.position - Camera.main.transform.position, Common.PlayerHead.up);
        }
    }
}