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

/*
 * Student: CHHIPERI Alin-Ioan
 * Calculatoare
 * grupa: 3131a
*/


namespace CHIPERI_Lab_EGC
{
    class SimpleWindow3D : GameWindow
    {

        const float rotation_speed = 180.0f;
        float angle;
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
            else if (keyboard[OpenTK.Input.Key.ControlLeft] && keyboard[OpenTK.Input.Key.C] )
            {

                if (showCube == true)
                {
                    showCube = false;
                    
                }
                else
                {
                    showCube = true;
                }
                lastKeyPress = keyboard;
            }
            

           }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            //angle += rotation_speed * (float)e.Time;
            //GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            // Exportăm controlul randării obiectelor către o metodă externă (modularizare).
            if (showCube == true)
            {
                DrawCube();
            }
            

            SwapBuffers();
            //Thread.Sleep(1);
        }

        /* private void DrawAxes_OLD()
         {
             GL.Begin(PrimitiveType.Lines);

             // X
             GL.Color3(Color.Red);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(20, 0, 0);

             // Y
             GL.Color3(Color.Blue);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(0, 20, 0);

             // Z
             GL.Color3(Color.Yellow);
             GL.Vertex3(0, 0, 0);
             GL.Vertex3(0, 0, 20);


             GL.End();
         }*/


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
        /*private void DrawPiramide()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(0.0f, -1.0f, -1.0f);

            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(0.0f, -1.0f, -1.0f);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(0.0f, -1.0f, -1.0f);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }*/

        [STAThread]
        static void Main(string[] args)
        {


            using (SimpleWindow3D example = new SimpleWindow3D())
            {

                // Verificați semnătura funcției în documentația inline oferită de IntelliSense!
                example.Run(30.0, 0.0);
            }
        }
    }

}

