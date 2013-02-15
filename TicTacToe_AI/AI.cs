using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TicTacToe_AI
{
    class AI
    {
        private static int ID = 0;//ID okkar
        private static int OID = 0;//ID óvinarins

        public AI(int id, int opponent)
        {
            ID = id;
            OID = opponent;
        }

        int blocked = 0;

        public int calculateBestPosition()
        {
            if (Logic.CPUbegins && Logic.player == 0) return 4;//Ef við byrjum, tökum miðjuna

            blocked = Logic.getMarkValue(1, 2);//Magn raða sem við höfum komið í veg fyrir

            //Afrita röðina
            int[] tictacBAK = new int[9];
            Array.Copy(Logic.tictac,tictacBAK,Logic.tictac.Length);

            //Besta value
            int best_value = -30;
            //Besta mögulega staða
            int best_position = 0;

            for (int i = 0; i < Logic.tictac.Length; i++)
            {
                if (Logic.tictac[i] != ID && Logic.tictac[i] != OID)  //Ef einhver er ekki búinn að gera
                {
                    Logic.tictac[i] = ID;//prófum að setja x
                    int returned_value = EvaluatePosition();//Var þetta gott move?
                    //MessageBox.Show((returned_value >= best_value) + "");
                    if (returned_value >= best_value)
                    {
                        best_value = returned_value;
                        best_position = i;
                        
                    }
                    Logic.tictac[i] = 0;
                }
                //Array.Copy(tictacBAK,Logic.tictac,tictacBAK.Length);//Restorum afritið


            }
            //Array.Copy(tictacBAK, Logic.tictac, tictacBAK.Length);//Restorum afritið
            return best_position;//Skilum bestu mögulegu stöðunni

        }

        public int EvaluatePosition()
        {
            if (ID == Logic.xID && Logic.getMarkValue(3, 0) > 0)//Er hægt að vinna?
            {
                return 10;
            }
            else if (ID == Logic.oID && Logic.getMarkValue(0, 3) > 0)
            {
                return 10;
            }
            else if (checkFor2ndPitfall() || checkFor3rdPitfall() || checkFor4thPitfall())
            {
                return -10;
            }
            else if (blocked < Logic.getMarkValue(1, 2))//Er eitthvað fleira blockað?
            {
                return 8;
            }
            else
            {
                int X2 = Logic.getMarkValue(2, 0);  // determine x2 number
                int X1 = Logic.getMarkValue(1, 0);  // determine x1 number
                int O2 = Logic.getMarkValue(0, 2);  // determine O2 number
                int O1 = Logic.getMarkValue(0, 1);  // determine O1 number
                return 3 * X2 + X1 - (3 * O2 + O1);
            }

        }
        /*
         * Gegn
         * |x |  | x|
         * |  |o |  |
         * | x|  |x |
         *
         */
        public bool checkForPitfall()
        {
            if (Logic.tictac[4] == OID && ((Logic.tictac[0] == ID && Logic.tictac[8] == ID) || (Logic.tictac[2] == ID && Logic.tictac[6] == ID)) && !Logic.edgesChecked(0))
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[8] == OID && Logic.tictac[2] == ID && Logic.tictac[3] == ID)
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[6] == OID && Logic.tictac[0] == ID && Logic.tictac[5] == ID)
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[6] == OID && Logic.tictac[1] == ID && Logic.tictac[8] == ID)
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[8] == OID && Logic.tictac[1] == ID && Logic.tictac[6] == ID)
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[8] == OID && Logic.tictac[1] == ID && Logic.tictac[3] == ID)
                return true;
            else if (Logic.tictac[4] == OID && Logic.tictac[0] == ID && Logic.tictac[6] == OID && Logic.tictac[7] == OID && Logic.tictac[1] == ID && Logic.tictac[2] == ID && Logic.tictac[3] == ID && Logic.tictac[8] == ID)
                return true;
            else
                return false;
        }

        /*
         * Gegn
         * |o |  |o |
         * |  |x |  |
         * |o |  |x |
         */
        public bool checkFor2ndPitfall()
        {

            if (Logic.tictac[0] == OID && Logic.tictac[8] == OID && !Logic.edgesChecked(ID))
                return true;
            else if (Logic.tictac[2] == OID && Logic.tictac[6] == OID && !Logic.edgesChecked(ID))
                return true;
            else if (Logic.tictac[6] == OID && Logic.tictac[2] == OID && !Logic.edgesChecked(ID))
                return true;
            else if (Logic.tictac[8] == OID && Logic.tictac[0] == OID && !Logic.edgesChecked(ID))
                return true;
            else
                return false;
        }
        /*
         * Gegn
         * |  |o |  |
         * |o |x |  |
         * |  |  |x |
         */
        public bool checkFor3rdPitfall()
        {
            if (Logic.tictac[1] == OID && Logic.tictac[3] == OID && Logic.tictac[0] == 0 && (Logic.tictac[2] != ID && Logic.tictac[6] != ID))
                return true;
            else if (Logic.tictac[1] == OID && Logic.tictac[5] == OID && Logic.tictac[2] == 0 && (Logic.tictac[0] != ID && Logic.tictac[8] != ID))
                return true;
            else if (Logic.tictac[5] == OID && Logic.tictac[7] == OID && Logic.tictac[8] == 0 && (Logic.tictac[2] != ID && Logic.tictac[6] != ID))
                return true;
            else if (Logic.tictac[3] == OID && Logic.tictac[7] == OID && Logic.tictac[6] == 0 && (Logic.tictac[0] != ID && Logic.tictac[8] != ID))
                return true;
            else
                return false;
        }

        public bool checkFor4thPitfall()
        {
            if(Logic.tictac[0] == OID && Logic.tictac[4] == ID && Logic.tictac[5] == OID && (Logic.tictac[2] != ID && Logic.tictac[8]  != ID))
                return true;
            else if (Logic.tictac[2] == OID && Logic.tictac[4] == ID && Logic.tictac[3] == OID && (Logic.tictac[0] != ID && Logic.tictac[6] != ID))
                return true;
            else if (Logic.tictac[1] == OID && Logic.tictac[4] == ID && Logic.tictac[6] == OID && (Logic.tictac[0] != ID && Logic.tictac[2] != ID))
                return true;
            else if (Logic.tictac[1] == OID && Logic.tictac[4] == ID && Logic.tictac[8] == OID && (Logic.tictac[0] != ID && Logic.tictac[2] != ID))
                return true;
            return false;
        }
    }

}
