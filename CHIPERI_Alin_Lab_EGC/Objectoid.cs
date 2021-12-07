﻿using OpenTK;
using System.Drawing;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace CHIPERI_Alin_Lab_EGC
{
    class Objectoid
    {
        private bool visibility;
        private bool isGravityBound;
        private Color color;
        private List<Vector3> coordList;
        private Randomizer rando;

        private const int GRAVITY_OFFSET = 1;

        public Objectoid()
        {
            rando = new Randomizer();
            visibility = true;
            isGravityBound = true;
            color = rando.RandomColor();
            coordList = new List<Vector3>();
            int size_offset = rando.RandomInt(3,7); // permite crearea de obiecte cu un mic offset de dimensiune(variabila ca dimensiune
            int height_offset = rando.RandomInt(40, 60); // permite creade de obiecte plasate la un mic offset de inaltime 
            int radial_offset = rando.RandomInt(5, 15);  // permite creare de obiecte cu un mic ofsser pe directia ox-oz pozitive

         
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 0 * size_offset + height_offset, 1 * size_offset+ radial_offset));
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 0 * size_offset + height_offset, 0 * size_offset+ radial_offset));
            coordList.Add(new Vector3(1 * size_offset+radial_offset, 0 * size_offset + height_offset, 1 * size_offset+ radial_offset));
            coordList.Add(new Vector3(1 * size_offset+radial_offset, 0 * size_offset + height_offset, 0 * size_offset+ radial_offset));
            coordList.Add(new Vector3(1 * size_offset+radial_offset, 1 * size_offset + height_offset, 1 * size_offset+ radial_offset));
            coordList.Add(new Vector3(1 * size_offset+radial_offset, 1 * size_offset + height_offset, 0 * size_offset+ radial_offset));
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 1 * size_offset + height_offset, 1 * size_offset+ radial_offset));
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 1 * size_offset + height_offset, 0 * size_offset+ radial_offset));
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 0 * size_offset + height_offset, 1 * size_offset+ radial_offset));
            coordList.Add(new Vector3(0 * size_offset+radial_offset, 0 * size_offset + height_offset, 0 * size_offset+ radial_offset));
        }
        public void Draw()
        {
            if (visibility)
            {

                GL.Color3(color);
                GL.Begin(PrimitiveType.QuadStrip);
                foreach (Vector3 v in coordList)
                {
                    GL.Vertex3(v);
                }
                GL.End();
            }
        }
        public void UpdatePosition()
        {
            if (visibility && isGravityBound && !GroundCollisionDetected() )
            {
                for(int i=0; i<coordList.Count; i++)
                {
                    coordList[i] = new Vector3(coordList[i].X, coordList[i].Y-GRAVITY_OFFSET, coordList[i].Z);
                }
            }
        }

        public bool GroundCollisionDetected()
        {
            foreach (Vector3 v in coordList)
            {
                if (v.Y <= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void ToggleVisibility()
        {
            visibility = !visibility;
        }
        public void ToogleGravity()
        {
            isGravityBound = !isGravityBound;
        }
        public void SetGravity()
        {
            isGravityBound = true;
        }
        public void UnsetGravity()
        {
            isGravityBound = false;
        }
    }
}
