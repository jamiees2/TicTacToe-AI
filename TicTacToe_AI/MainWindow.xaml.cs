using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe_AI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /*
     * Höfundur: James Elías Sigurðarson
     * Dagsetning: 21.9.2012
     * 
     * Forrit sem er tic tac toe leikur, krefst tveggja spilara
     */
    


    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void box2_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;//Nær í takkann
            int identifier = Convert.ToInt32(tempButton.Tag) - 1;//Les inn númer takkans

            //MessageBox.Show(identifier + "");
            tempButton.Content = Logic.Player();//Setur nafn á takkann
            tempButton.IsEnabled = false;//Ekki hægt að fikta meir í takkanum

            if (!completeMove(identifier))
            {//Klára hreyfingu
                CPUmove();
            }
            //MessageBox.Show(x + " " + y);
            //Gera CPU hreyfingu
        }

        
        private void gameOver(string winner)
        {
            foreach (object child in ticTacGrid.Children)//Fyrir hvern takka í griddinum undir groupboxinu sem heldur utan um leikinn
            {
                if (child is Button)//Ef þetta er örugglega takki
                {
                    Button tempButton = (Button)child;//Breytir í takka

                    tempButton.IsEnabled = false;//Allir takkar eru ónothhæfir
                }
            }
            infoLabel.Content = winner + " vann, nýr leikur?";//Hver vann?
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {

            foreach (object child in ticTacGrid.Children)//Fyrir hvern takka í griddinum undir groupboxinu sem heldur utan um leikinn
            {
                if (child is Button)//Ef þetta er örugglega takki
                {
                    Button tempButton = (Button)child;//Breytir í takka

                    tempButton.IsEnabled = true;//Allir takkar eru nothhæfir
                    tempButton.Content = "";//Tæmir texta úr öllum tökkum
                }
            }
            infoLabel.Content = "O byrjar";//X byrjar
            Logic.reset();
            if (Logic.CPUbegins) CPUmove();//Ef cpu á að byrja, byrja
        }

        public void CPUmove() //Hreyfing CPU
        {
            //MessageBox.Show("CPU moving");
            int position= Logic.CPU.calculateBestPosition();//Reikna út bestu stöðu
            //MessageBox.Show(position + "");


            int tag = position + 1;//Reikna út hvaða takki þetta er
            Button tempButton = null;//Temp takki
            foreach (object child in ticTacGrid.Children)//Fyrir hvern takka í griddinum undir groupboxinu sem heldur utan um leikinn
            {
                if (child is Button)//Ef þetta er örugglega takki
                {
                    if(((Button)child).Tag.Equals(tag+""))//Ef takkinn er sá sem við leitum að
                    {
                        tempButton = (Button)child;//Breytir í takka
                    }
                    
                }
            }
            if (!(tempButton is Button)) { return; }//Ef ekki takki, sleppum þessu
            tempButton.Content = Logic.Player();//Setur nafn á takkann
            tempButton.IsEnabled = false;//Ekki hægt að fikta meir í takkanum
            completeMove(position);//Klárum hreyfinguna
            //MessageBox.Show(x + " " + y);
            
        }

        public bool completeMove(int pos)
        {
            Logic.tictac[pos] = Logic.PlayerID();//Setur inn númer spilarans
            Logic.player++;//Bætir einum við spilarann
            int winner = Logic.checkWins();//Kannar hvort einhver hafi unnið

            if (winner != -1)//Ef einchver vann
            {
                Logic.score.Update(winner);//Uppfæra stöðu
                gameOver(Logic.Player(winner));//Leikur búinn
                return true;
            }
            return false;
        }
        
        public void updateScore() 
        {
            scoreO.Text = "" + Logic.score.O;//Staðan fyrir O
            scoreX.Text = "" + Logic.score.X;//Staðan fyrir X
            scoreDraw.Text = "" + Logic.score.Draw;//Staða jafntefla
        }
        
        private void resetScore_Click(object sender, RoutedEventArgs e)
        {
            Logic.score.Clear();//Hreinsa stöðu
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logic.window = this;//Setur gluggann
            updateScore();//Uppfærir stöðu
        }

        private void byrjarBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            Logic.CPUbegins = !Logic.CPUbegins;//Snúa við hver byrjar
        }
    }
}
