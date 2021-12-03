using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace CHIPERI_Alin_Lab_EGC
{
    class Triangle
    {
        private const int MIN_VAL= -25;
        private const int MAX_VAL= 25;

        private VertexPoint A, B, C;
        private Color color;
        private Randomizer rando= new Randomizer();
        private Random r = new Random();
        
        private float linewidth;
        private readonly int OFFSET = 1;
        public bool IsDrawable { get; set; }

        public bool canFall()
        {
            if (A.coordY != 0)
                return true;
            else
                return false;
        }

        public void Hide()
        {
            IsDrawable = false;
        }

        public void Show()
        {
            IsDrawable = true;
        }

        public void ToggleVisibility()
        {
            if (IsDrawable == true)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public Triangle()
        {
            IsDrawable = true;
            color = rando.RandomColor();

            // coordonate hardcoded - se poate înlocui cu încărcare din fișier text specificat;
            A = new VertexPoint(5, 12, 0, color);
            B = new VertexPoint(15, 30, 0,color);
            C = new VertexPoint(10, 30, 0,color);
        }
       
        public void ManualMoveMe(bool _relativeForward, bool _relativeBackward, bool _relativeLeft, bool _relativeRight, bool _relativeUp, bool _relativeDown)
        {
            if (IsDrawable == false)
            {
                return;
            }

            if (_relativeForward == true)
            {
                A.coordZ -= OFFSET;
                B.coordZ -= OFFSET;
                C.coordZ -= OFFSET;
            }

            if (_relativeBackward == true)
            {
                A.coordZ += OFFSET;
                B.coordZ += OFFSET;
                C.coordZ += OFFSET;
            }

            if (_relativeLeft == true)
            {
                A.coordX -= OFFSET;
                B.coordX -= OFFSET;
                C.coordX -= OFFSET;
            }

            if (_relativeRight == true)
            {
                A.coordX += OFFSET;
                B.coordX += OFFSET;
                C.coordX += OFFSET;
            }

            if (_relativeUp == true)
            {
                A.coordY += OFFSET;
                B.coordY += OFFSET;
                C.coordY += OFFSET;
            }

            if (_relativeDown == true)
            {
                A.coordY -= OFFSET;
                B.coordY -= OFFSET;
                C.coordY -= OFFSET;
            }

        }

        public void Draw()
        {
            if (IsDrawable == false)
            {
                return;
            }

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(color);
            GL.Vertex3(A.coordX, A.coordY, A.coordZ);
            GL.Vertex3(B.coordX, B.coordY, B.coordZ);
            GL.Vertex3(C.coordX, C.coordY, C.coordZ);

            GL.End();
        }

        /*
         * laborator 4- exercitiul 2
         * am creat o functie care deneseaza un triunghi si genereaza random culorile lui
         */
        public void DrawRand()
        {
            Randomizer rand = new Randomizer();
            Color color = rand.RandomColor();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);
            GL.Vertex3(A.coordX, A.coordY, A.coordZ);

            GL.Vertex3(B.coordX, B.coordY, B.coordZ);
            GL.Vertex3(C.coordX, C.coordY, C.coordZ);
            Console.WriteLine(color);
            GL.End();

        }
        
        public void Translate(int x)
        {
            A.coordX += x;
            B.coordX += x;
            C.coordX += x;
            A.coordY += x;
            B.coordY += x;
            C.coordY += x;
            A.coordZ += x;
            B.coordZ += x;
            C.coordZ += x;

        }


        public void Fall()
        {
            while (canFall())
            {
                ManualMoveMe(false, false, false, false, false, true);

            }
        }

        public void DiscoMode()
        {
            color = rando.RandomColor();
        }
        /// <summary>
        /// 2 PUNCTE RAMAN FIZE SI AL 3 LEA SE MORFEAZA
        /// </summary>
        public void Morph()
        {
            int select = rando.GeneratePozitiveInt(3);
            VertexPoint tmp = rando.GenerateVertexPoint();
            if(select==0)
            {
                A = tmp;
            }else if(select ==1)
            {
                B = tmp;
            }else
            {
                C = tmp;
            }

        }

     
    }
}
