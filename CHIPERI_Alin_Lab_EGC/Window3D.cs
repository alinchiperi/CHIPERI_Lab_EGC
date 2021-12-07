using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace CHIPERI_Alin_Lab_EGC
{
    /// <summary>
    /// The graphic window. Contains the canvas (viewport to be draw).
    /// </summary>
    class Window3D : GameWindow
    {

        Axes xyz;
        Camera3DIsometric cam;
        Triangle trg;
        Cube cube;
        Cube cube2;

        private List<Objectoid> rainOfObjects;
        
        KeyboardState lastKeyPress;
        MouseState lastClick;
        private const int XYZ_SIZE = 75;
        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);
        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            cam = new Camera3DIsometric();
            xyz = new Axes();
            trg = new Triangle();
            cube = new Cube();
            cube2 = new Cube(@"D:\Facultate\Anul III\EGC\CHIPERI_Lab_EGC\Cube.txt");
            rainOfObjects = new List<Objectoid>();

            DisplayMenu();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set background
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            cam.SetCamera();

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
           

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

                // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, inclusiv
                // pentru gaminig - gamepads, joysticks, etc.).
            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            if (keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Ascundere comandată, prin apăsarea unei taste - cu verificare de remanență! Timpul de reacție
                // uman << calculator.
                xyz.ToggleVisibility();
            }

            #region _TriangleMOVE_ 
            if (keyboard[Key.Z] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(true, false, false, false, false, false);
            }
            if (keyboard[Key.X] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(false, true, false, false, false, false);
            }
            if (keyboard[Key.C] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(false, false, true, false, false, false);
            }
            if (keyboard[Key.V] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(false, false, false, true, false, false);
            }
            if (keyboard[Key.B] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(false, false, false, false, true, false);
            }
            if (keyboard[Key.N] && !keyboard.Equals(lastKeyPress))
            {
                trg.ManualMoveMe(false, false, false, false, false, true);
            }
            #endregion

            if (keyboard[Key.Y] && !keyboard.Equals(lastKeyPress))
            {
                trg.Morph(); //triangle morph
                trg.DiscoMode(); // triangle discomode

            }
            //la apasarea tastei r se vor schimba fetele cubului 
            if (keyboard[Key.R] && !keyboard.Equals(lastKeyPress))
            {
                
                cube2.ChangeColorFace6();
                cube2.ChangeColorFace5();
                cube2.ChangeColorFace4();

            }
            if (keyboard[Key.U] && !keyboard.Equals(lastKeyPress))
            {
                cube2.Scale(3);
                

            }
            if (keyboard[Key.T] && !keyboard.Equals(lastKeyPress))
            {
                cube2.ToggleVisibility();
                trg.ToggleVisibility();
            }

            if (keyboard[Key.H] && !keyboard.Equals(lastKeyPress))
            {
                DisplayMenu();
            }

            // ploaie de obiecte
            if(mouse[MouseButton.Left] && !lastClick[MouseButton.Left])
            {
                rainOfObjects.Add(new Objectoid());
            }
            // curata obiectele cazatoare

            if(mouse[MouseButton.Right]&& !lastClick[MouseButton.Right])
            {
                rainOfObjects.Clear();
            }
            // update cadere
            foreach(Objectoid obj in rainOfObjects)
            {
                obj.UpdatePosition();
            }
                #region CameraMove 
                if (keyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (keyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (keyboard[Key.A])
            {
                cam.MoveLeft();
            }
            if (keyboard[Key.D])
            {
                cam.MoveRight();
            }
            if (keyboard[Key.Q])
            {
                cam.MoveUp();
            }
            if (keyboard[Key.E])
            {
                cam.MoveDown();

            }
            if(keyboard[Key.J] && !keyboard.Equals(lastKeyPress))
            {
                cam.FarCam();
            }
            if (keyboard[Key.K] && !keyboard.Equals(lastKeyPress))
            {
                cam.NearCam();
            }
            #endregion

            lastKeyPress = keyboard;
            lastClick = mouse;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);        

            xyz.DrawMe();          

            trg.Draw();
            cube2.Draw();
           
            foreach(Objectoid obj in rainOfObjects)
            {
                obj.Draw();
            }

            SwapBuffers();
        }

        public void DisplayMenu()
        {
            Console.WriteLine("-----------------------MENU---------------------");
            Console.WriteLine("Left Click - Cadere obiect de la o inaltime random ");
            Console.WriteLine("Right Click - Curata lista de Obiecte ");
            Console.WriteLine("ESC - terminare program");
            Console.WriteLine("P - show axes");
            Console.WriteLine("H - show help");
            Console.WriteLine("W A S D E Q - camera move");
            Console.WriteLine("J - Far Cam");
            Console.WriteLine("K - Near Cam");
            Console.WriteLine("Z X C V B N - Triangle Move");
            Console.WriteLine("Y -  Triangle Morph and Disco Mode");
            Console.WriteLine("U - cube Scale");
            Console.WriteLine("R - change cube color");


        }
    }
}
