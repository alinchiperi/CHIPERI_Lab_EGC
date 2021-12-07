using OpenTK;
using System;
using System.Drawing;

namespace CHIPERI_Alin_Lab_EGC
{
    /// <summary>
    /// This class generates various random values for different kind of parameters (<see cref="Random()"/>).
    /// </summary>
    class Randomizer
    {
        private Random r;

        private const int LOW_INT_VAL = -25;
        private const int HIGH_INT_VAL = 25;
        private const int LOW_COORD_VAL = -50;
        private const int HIGH_COORD_VAL = 50;

        /// <summary>
        /// Standard constructor. Initialised with the system clock for seed.
        /// </summary>
        public Randomizer()
        {
            r = new Random();
        }

        /// <summary>
        /// This method returns a random Color when requested.
        /// </summary>
        /// <returns>the Color, randomly generated!</returns>
        public Color RandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);

            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        /// <summary>
        /// This method returns a random 3D coordinate. Values are ranged (0-centered).
        /// </summary>
        /// <returns>the 3D point's coordinates, randomly generated!</returns>
        public Vector3 Random3DPoint()
        {
            int genA = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);
            int genB = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);
            int genC = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);

            Vector3 vec = new Vector3(genA, genB, genC);

            return vec;
        }

        /// <summary>
        /// This method returns a random int when required. The value is ranged between predefined values (symmetrical over zero).
        /// </summary>
        /// <returns>random int;</returns>
        public int RandomInt()
        {
            int i = r.Next(LOW_INT_VAL, HIGH_INT_VAL);

            return i;
        }
        /// <summary>
        /// Functie care genereaza un vertexpoint avand coordonatele intre LOW_INT_VAL si HIGH_INT_VAL
        /// </summary>
        /// <returns></returns>
        public VertexPoint GenerateVertexPoint()
        {
            int a = r.Next(LOW_INT_VAL,HIGH_INT_VAL);
            int b = r.Next(LOW_INT_VAL,HIGH_INT_VAL);
            int c = r.Next(LOW_INT_VAL,HIGH_INT_VAL);

            VertexPoint vertx = new VertexPoint(a, b, c);
            return vertx;
        }

        public int GeneratePozitiveInt(int limit)
        {
            int a = r.Next(0, limit);
            return a;
        }
        /// <summary>
        /// Metoda care returneaza un int random . Valoarea returnata este intre valorile date
        /// </summary>
        /// <param name="minVal">Valoarea minima</param>
        /// <param name="maxVal">valoarea maxima</param>
        /// <returns>valoarea random</returns>
        public int RandomInt(int minVal, int maxVal)
        {
            int i = r.Next(minVal,maxVal);

            return i;
        }
    }
}
