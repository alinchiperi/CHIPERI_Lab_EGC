using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using System.Collections;
using System.IO;
/*
 * Student: CHIPERI Alin-Ioan
 * Calculatoare
 * grupa: 3131a
*/


namespace CHIPERI_Lab_EGC
{
    class SimpleWindow3D : GameWindow
    {

        const float rotation_speed = 180.0f;
        float anglex;
        
        float lookatX=30;
        float lookatY=30;
        float lookatZ=15;
        Color color=Color.Red;
        bool showCube = true;
        KeyboardState lastKeyPress;
        
            

        // Constructor.
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
            
            
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            //GL.Enable(EnableCap.DepthTest);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, mai ales pentru gaming - gamepads, joysticks, etc.).
            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[OpenTK.Input.Key.ControlLeft] && keyboard[OpenTK.Input.Key.C] && keyboard.Equals(lastKeyPress))
            {

                if (showCube == true)
                {
                    showCube = false;

                }
                else
                {
                    showCube = true;
                }

            }
            lastKeyPress = keyboard;

            if (mouse[OpenTK.Input.MouseButton.Left])
            {
              
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            

            if(keyboard[OpenTK.Input.Key.Up])
            {
                lookatY++;
            }
            if (keyboard[OpenTK.Input.Key.Down])
            {
                lookatY--;
            }
            if (keyboard[OpenTK.Input.Key.Right])
            {
                lookatX++;
            }
            if (keyboard[OpenTK.Input.Key.Left])
            {
                lookatX--;
            }

        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetCursorState();
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(lookatX, lookatY, lookatZ, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            /*angle += rotation_speed * (float)e.Time;*/

            //---------------------------------------------------------------
            //Laborator 2 punctul 2
            // controlul folosind 2 taste
            if (keyboard[OpenTK.Input.Key.A])
            {
                anglex++;
                GL.Rotate(anglex, 0.0f, 1.0f, 0.0f);
            }
            if (keyboard[OpenTK.Input.Key.D])
            {
                anglex--;
                GL.Rotate(anglex, 0.0f, 1.0f, 0.0f);
            }

            /*
             * Laborator 2
             * Punctul 2-prin mișcarea mouse-ului.
             */
            GL.Rotate(mouse.X, 0.0f, 1.0f, 0.0f);


            // Exportăm controlul randării obiectelor către o metodă externă (modularizare).
            if (showCube == true)
            {
                DrawCube();
            }

            /*
             * Laborator 3- cerinta 8 schimbarea culorilor folosind tastele
             */
            if(keyboard[OpenTK.Input.Key.Number1])
            {
                color = Color.Red;
            }
            if(keyboard[OpenTK.Input.Key.Number2])
            {
                color = Color.White;
            }


            DrawAxes_OLD();
            DrawTriangle();

            /*laborator 3 exercitiul 4 */
            DrawLineLoop();
            DrawLineStrip();
            DrawTriangleFan();
            DrawTriangleStrip();

            SwapBuffers();
            //Thread.Sleep(1);
        }

        private void DrawAxes_OLD()
         {
             GL.Begin(PrimitiveType.Lines);     // desenarea axelor folosind doar un GL.Begin si un GL.End

             // X
             GL.Color3(Color.Red);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(20, 0, 0);

             // Y
             GL.Color3(Color.Green);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(0, 20, 0);

             // Z
             GL.Color3(Color.Yellow);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(0, 0, 20);


             GL.End();
         }

        /*
         * Laborator 3-exercitiul4
         */
        private void DrawLineLoop()
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(2, 4, 0);
            GL.Vertex3(1, 8, 0);
            GL.Vertex3(-7, -3, 0);
            GL.End();
        }
        private void DrawLineStrip()
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(Color.AntiqueWhite);
            GL.Vertex3(-2, -4, 0);
            
            GL.Vertex3(5, -9, 0);
            GL.Vertex3(3, 4, 0);
           
            GL.End();
        }
        private void DrawTriangleFan()
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(Color.Chocolate);
            GL.Vertex3(-3, -3, 0);
            GL.Color3(Color.RosyBrown);
            GL.Vertex3(-10, -15, 0);
            GL.Color3(Color.SaddleBrown);
            GL.Vertex3(-8, -8, 0);
            GL.Color3(Color.SandyBrown);
            GL.Vertex3(-16, -6, 0);

            GL.End();
        }

        private void DrawTriangleStrip()
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Vertex3(6, 7, 0);
            GL.Color3(254,254,254);
            GL.Vertex3(10, 18, 0);
            GL.Color3(44, 78, 90);
            GL.Vertex3(17, 7, 0);
            GL.Color3(99, 56, 70);
            
            GL.End();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }
        /*
         * Laborator 3- desenare triunghi
         */

        private void DrawTriangle()
        {
           
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);            
            List<float> coordonate = date(@"D:\Facultate\Anul III\EGC\CHIPERI_Lab_EGC\Triunghi.txt");

            // Am adugat fiecarui vertex coordonata din lista de float 
            GL.Vertex3(coordonate[0], coordonate[1], coordonate[2]);
            GL.Vertex3(coordonate[3], coordonate[4], coordonate[5]);
            GL.Vertex3(coordonate[6], coordonate[7], coordonate[8]);

            /*
            GL.Vertex3(4, 15, 0);
            GL.Vertex3(8, 4, 0);
            GL.Vertex3(4, 4, 0);*/

            GL.End();

        }
        
        /*
         * Laborator 3- citirea datelor din fisier 
         * Am creat o lista float in care am adaugat datele din fisier text
         */
        public List<float> date(string NumeFisier)
        {
            List<float> coordonate = new List<float>();
            string[] lines = System.IO.File.ReadAllLines(NumeFisier);
            for (int i = 0; i <= 2; i++)
            {
                string[] coord_split = lines[i].Split(' ');
                for (int j = 0; j <= 2; j++)
                    coordonate.Add(float.Parse(coord_split[j]));

            }
                return coordonate;
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {

                
                example.Run(30.0, 0.0);
            }
        }
    }

}

