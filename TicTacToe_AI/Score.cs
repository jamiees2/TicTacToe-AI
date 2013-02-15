using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe_AI
{
    class Score
    {
        private int xScore = 0;//Staða X
        private int oScore = 0;//Staða O
        private int drawScore = 0;//Hversu mörg jafntefli

        public int X//Breytanleg staða X
        {
            get//Skilar X
            {
                return xScore;
            }
            set //Setur X
            {
                xScore = value;
                Logic.window.updateScore();//Uppfærir stöðu
            }
        }
        public int O//Breytanleg staða O
        {
            get//Skilar O
            {
                return oScore;
            }
            set//Setur O
            {
                oScore = value;
                Logic.window.updateScore();//Uppfærir stöðu
            }
        }

        public int Draw
        {
            get 
            {
                return drawScore;
            }
            set
            {
                drawScore = value;
                Logic.window.updateScore();
            }
        }

        public void Clear() //Hreinsar stödu
        {
            X = 0;
            O = 0;
            Draw = 0;
        }

        public void Update(int winner) //Uppfærir stöðu
        {
            if (winner == Logic.xID)
            {
                X++;
            }
            else if (winner == Logic.oID)
            {
                O++;
            }
            else if (winner == 0)
            {
                Draw++;
            }
        }
    }
}
