﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prvncher.UX_Sketchbook.MultiTouch.Input
{
    using Input = UnityEngine.Input;

    public class InputSource : MonoBehaviour
    {
        float m_Width;
        float m_Height;

        List<Vector3> m_FingerPositions = new List<Vector3>();

        public IReadOnlyList<Vector3> FingerPositions => m_FingerPositions;

        void OnGUI()
        {
            // Compute a fontSize based on the size of the screen m_Width.
            GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

            for (int i = 0; i < m_FingerPositions.Count; i++)
            {
                GUI.Label(new Rect(20, 20 * (i + 1) + i * 20, m_Width, m_Height * 0.25f),
                    $"F {i} [ x = {m_FingerPositions[i].x:f2} | y = {m_FingerPositions[i].y:f2} ]");
            }
        }

        void Update()
        {
            // Update screen proportions
            m_Width = (float)Screen.width / 2.0f;
            m_Height = (float)Screen.height / 2.0f;

            m_FingerPositions.Clear();
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                Vector2 pos = touch.position;
                pos.x = (pos.x - m_Width) / m_Width;
                pos.y = (pos.y - m_Height) / m_Height;

                m_FingerPositions.Add(new Vector3(-pos.x, pos.y, 0.0f));
            }
        }
    }
}