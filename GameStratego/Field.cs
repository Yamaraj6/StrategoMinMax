using System;
using System.Collections.Generic;
using System.Text;
using GameSolvingAlgorithms.GameInterfaces;

namespace GameStratego
{
    public class Field:IField
    {
        private int x;
        private int y;

        public Field(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override int GetHashCode()
        {
            return x*10000+y;
        }

        public override bool Equals(object obj)
        {
            return x==((Field)obj).x && y == ((Field)obj).y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}
