using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe_AI;

namespace TicTacToe_AI
{
    class Logic
    {
        public static int player = 0;//Núverandi spilari
        public static int[] tictac = new int[9];//Fylki sem heldur um stöðu

        public static int xID = 2;//Það sem fer í fylkið þegar x gerir
        public static int oID = 1;//Það sem fer í fylkið þegar o gerir


        private static bool CPUstarts = false;
        public static bool CPUbegins//Byrjar CPU?
        {
            get//Skilar cpu
            {
                return CPUstarts;
            }
            set//Setur cpu
            {
                CPUstarts = value;
                if (CPUstarts)
                {
                    CPU = new AI(oID, xID);//Endurstillir AIinn fyrir cpu-byrjun
                }
                else
                {
                    CPU = new AI(xID, oID);//Endurstillir AIinn fyrir mann-byrjun
                }
            }
        }

        public static AI CPU = new AI(xID,oID);
        public static MainWindow window = null;
        public static Score score = new Score();

        

        public static string Player()
        {
            return (player % 2 == 1) ? "X" : "O";//Skilar x ef núverandi spilarinn er deilanlegur með 2, O annars
        }

        public static int PlayerID() 
        {
            return (player % 2 == 1) ? xID : oID;//Skilar id af x ef núverandi spilari er deilanlegur með 2
        }

        public static string Player(int id)
        {
            return (id == xID) ? "X" : ((id == oID) ? "O" : "Enginn"); 
        }

        public static int checkWins()
        {
            if (getMarkValue(3,0) > 0)//Ef talan er jöfn og talan fyrir x sinnum 3, semsagt -1*3 = -3
            {
                //winner = "X";//Leikur búinn
                return xID;
            }
            else if (getMarkValue(0,3) > 0)//Ef talan er jöfn og talan fyrir o sinnum 3, semsagt 1*3 = 3
            {
                //winner = "O";//Leikur búinn
                return oID;
            }
            else if (player >= 9)//Ef búið er að gera 9 sinnum eða oftar(ætti ekki að gerast) þá vinnur enginn (jafntefli)
            {
                //winner = "Enginn";//Leikur búinn
                return 0;
            }
            return -1;
        }

        public static void reset() 
        {
            player = 0;//Endurstillir núverandi spilara
            tictac = new int[9];//Endurstillir matrix sem heldur utan um hnit
        }

        public static int getMarkValue(int Xs, int Os)
        {
            int marks=0;
	        int numX=0,numO=0;
	
	        //Skoða raðir
            for (int i = 0; i <= 8; i++)
            {
                if (Logic.tictac[i] == oID) // If there's a CPU mark..
                    numO++;
                else if (Logic.tictac[i] == xID) // If there's a HUMAN mark..
                    numX++;

                if ((i + 1) % 3 == 0)  // If three row positions are over..
                {
                    if (numX == Xs && numO == Os)
                    {
                        marks++;
                    }
                    numX = 0;
                    numO = 0;
                }
            }

	        numX=0;
            numO=0;
	
	        // Check Columns
	        int counter=0;
	        int next=0;
	        while (next<=2)
	        {
		        for (int i = counter;i<=8;i+=3)  
		        {
			        if (Logic.tictac[i] == oID) // If there's CPU mark..
				        numO++;
			        else if (Logic.tictac[i] == xID) // If there's HUMAN mark..
				        numX++;
		        }
		        if (numX == Xs && numO ==Os)
		        {
			        numX=0;
			        numO=0;
			        marks++;
		        }
		        next++;
		        counter=next;
                numX = 0;
                numO = 0;
	        }

	        numX=0;
            numO=0;

	        //Check Diagonal 1
	        for (int i=0;i<=8;i+=4)  
	        {
		        if (Logic.tictac[i] == oID) // If there's CPU mark..
			        numO++;
		        else if (Logic.tictac[i] == xID) // If there's HUMAN mark..
			        numX++;
	        }
	        if (numX == Xs && numO==Os)
	        {
			        numX=0;
			        numO=0;
			        marks++;
	        }

            numX = 0;
            numO = 0;

	        //Check Diagonal 2
	        for (int i=2;i<=6;i+=2)  
	        {
		        if (Logic.tictac[i] == oID) // If there's CPU mark..
			        numO++;
		        else if (Logic.tictac[i] == xID) // If there's HUMAN mark..
			        numX++;
	        }
	        if (numX == Xs && numO==Os)
	        {
		        numX=0;
		        numO=0;
		        marks++;
	        }

	        return marks;
        }

        public static bool edgesChecked(int id)//Er einhver hlið checked?
        {
            return (tictac[1] == id || tictac[3] == id || tictac[5] == id || tictac[7] == id);
        }

        public static bool cornersChecked(int id)//Er eitthvað horn checked?
        {
            return (tictac[0] == id || tictac[2] == id || tictac[6] == id || tictac[8] == id);
        }
    }
}
