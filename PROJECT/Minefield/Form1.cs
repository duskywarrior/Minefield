using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minefield
{
    public partial class Form1 : Form
    {
        //start location of sprite
        int atCol = 10;
        int atRow = 19;
        bool[,] bombs = new bool[20, 20];


        public Form1()
        {
            InitializeComponent();
        }

        private int bombCount(int atCol, int atRow)
        {
            int soFar = 0;

            if (atCol > 0)
            {
                if (bombs[atCol - 1, atRow]) //if bomb is at same column location - 1.
                {
                    soFar++;
                }
            }
            if (atCol < 19)
            {
                if (bombs[atCol + 1, atRow]) //if bomb is at same column location + 1.
                {
                    soFar++;
                }
            }
            if (atRow > 0)
            {
                if (bombs[atCol, atRow - 1]) //if bomb is at same row location - 1.
                {
                    soFar++;
                }
            }
            if (atRow < 19)
            {
                if (bombs[atCol, atRow + 1]) //if bomb is at same row location + 1.
                {
                    soFar++;
                }
            }

            return soFar;
        }

        private void ShowDaBomb()
        {
            Label lbl;
            for(int atRow = 0; atRow < 20; atRow++)
            {
                for (int atCol = 0; atCol < 20; atCol++)
                {
                    lbl = getLabel(atCol, atRow);
                    if(bombs[atCol,atRow])
                    {
                        lbl.BackColor = Color.Red;
                    }
                    else
                    {
                        lbl.BackColor = Color.Black;
                    }

                }
            }

        }

        private void PlaceBombs(int numBombs)
        {
            Random r = new Random(); //making random number
            //creating the variable for the algirithm
            int tryCol, tryRow;
            int setSofar = 0;

            //clear bombs 
            Array.Clear(bombs, 0, bombs.Length);
            try
            {
                //dowhile to figure out where the bombs are going
                do
                {
                    tryCol = r.Next(0, 20); // this includes all columns
                    tryRow = r.Next(0, 19); //avoid bottom row
                    if (!bombs[tryCol, tryRow])
                    {
                        bombs[tryCol, tryRow] = true;
                        setSofar++;
                    }
                } while (setSofar < numBombs);
            }
            catch
            {
                MessageBox.Show("A dreaded Error has happend. This is a calculation issue, i think.... please reset the game.");
            }
        }

        //function to return Label at (atCol, atRowl)
        private Label getLabel(int atCol, int atRow)
        {
            int k = atRow * 20 + atCol + 1;
            string s = "label" + k.ToString();

            foreach (Control c in panel1.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.Label))
                {
                    if (c.Name == s)
                    {
                        return (Label)c;
                    }
                }
            }
            return null;
        }

        //function to show sprite at (atCol, atRow)
        private void showSpriteAt(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow); 	//get label at (atCol,atRow)
            lbl.BackColor = Color.Red;    		//set backcolour to white
            lbl.Image = Properties.Resources.ghost; 	//set to show image 

        }

        private void HideSpriteImage(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow); //gets the location of the sprite
            lbl.Image = null;                   // removes the image of the sprite
        }

        private void amIDead(int atCol, int atRow)
        {
            if (bombs[atCol,atRow])
            {
                this.BackColor = Color.Black;
                btndown.Enabled = false;
                btndown.BackColor = Color.Red;
                btnup.Enabled = false;
                btnup.BackColor = Color.Red;
                btnleft.Enabled = false;
                btnleft.BackColor = Color.Red;
                btnright.Enabled = false;
                btnright.BackColor = Color.Red;
                ShowDaBomb();
                lblstatus.Text = "Mwahahaha you are bound to earth for eternity!!";

            }
            else
            {
                lbldanger.Text = "Danger level: " + bombCount(atCol, atRow);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bob is a result of taking life for granted, As he does every year after the snooker accident   ");
            label10.Image = Properties.Resources.gravestone;
            showSpriteAt(atCol, atRow); //set fish at location
            PlaceBombs(40); //planting the bombs
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if (atRow > 0)
            {
                HideSpriteImage(atCol, atRow);
                atRow--;
                showSpriteAt(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            if (atRow < 19)
            {
                HideSpriteImage(atCol, atRow);
                atRow++;
                showSpriteAt(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btnleft_Click(object sender, EventArgs e)
        {
            if (atCol > 0)
            {
                HideSpriteImage(atCol, atRow);
                atCol--;
                showSpriteAt(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (atCol < 19)
            {
                HideSpriteImage(atCol, atRow);
                atCol++;
                showSpriteAt(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

    }
}
