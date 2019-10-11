/******************************************************************************
* All rights reserved. No part of this publication may be reproduced, stored
* in a retrieval system, or transmitted in any form or by any means,
* electronic, mechanical, photocopying, recording or otherwise, without the
* prior written permission of Gethin Taylor
******************************************************************************/

/******************************************************************************
* \file  Form.cs   
* \brief this file contains all function relating to the 
* function of the game Gravy   
******************************************************************************/

/******************************************************************************
* INCLUDE FILES
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/******************************************************************************
* NAMESPACE AND CLASSESS
******************************************************************************/
namespace Minefield
{
    public partial class Form1 : Form
    {

        /******************************************************************************
        * TYPEDEFS
        ******************************************************************************/


        /******************************************************************************
        * #DEFINES and CONSTANTS
        ******************************************************************************/


        /******************************************************************************
        * GLOBAL VARIABLES
        ******************************************************************************/
        //start location of sprite
        int atCol = 10;
        int atRow = 19;
        bool[,] bombs = new bool[20, 20];
        int timeleft = 10;


        /******************************************************************************
        * STATIC FUNCTION PROTOTYPES
        ******************************************************************************/


        /******************************************************************************
        * LOCAL VARIABLES (static)
        ******************************************************************************/

        /*
         * This function counts the number of bombs in the area of the sprite
         */
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
            return soFar; //returns the value after incrementations.
        }

        /*
         * This function counts the number of bombs in the area of the sprite
         */
        private void ShowDaBomb()
        {
            Label lbl;
            for (int atRow = 0; atRow < 20; atRow++)
            {
                for (int atCol = 0; atCol < 20; atCol++)
                {
                    lbl = getLabel(atCol, atRow);
                    if (bombs[atCol, atRow])
                    {
                        lbl.Image = Properties.Resources.monster;
                        lbl.BackColor = Color.Black;
                    }
                }
            }

        }

        /*
         * This function counts the number of bombs in the area of the sprite
         */
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
                MessageBox.Show("A dreaded Error has happend. This is a calculation issue, " +
                    "i think.... please reset the game.");
            }
        }

        /*
        * Function to return Label at (atCol, atRow)
        */
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

        /*
        *function to show sprite at (atCol, atRow)
        */
        private void showSpriteAt(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow);     //get label at (atCol,atRow)
            lbl.BackColor = Color.Red;          //set backcolour to white
            lbl.Image = Properties.Resources.ghost;     //set to show image 

        }

        /*
        *function to Hide sprite at (atCol, atRow)
        */
        private void HideSpriteImage(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow); //gets the location of the sprite
            lbl.Image = null;                   // removes the image of the sprite
        }

        /*
        *function that returns a messagebox if you land on a bomb
        * or land on the grave
        */
        private void amIDead(int atCol, int atRow)
        {
            if (bombs[atCol, atRow])
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
                MessageBox.Show("Mwaha you are trapped on this earth for eternity!!");
            }
            else if (atCol == 9 && atRow == 0)
            {
                btndown.Enabled = false;
                btnup.Enabled = false;
                btnleft.Enabled = false;
                btnright.Enabled = false;
                ShowDaBomb();
                MessageBox.Show("You are free to rest your soul!");

            }
            else
            {
                lbldanger.Text = "Danger level: " + bombCount(atCol, atRow);
            }

        }

        private void endgame()
        {
            timer1.Stop();
        }


        /******************************************************************************
        * INITIALISATION OF GAME
        ******************************************************************************/
        public Form1()
        {
            InitializeComponent();
        }


        /******************************************************************************
        * GAME FUNCTION
        ******************************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bob is a result of taking life for granted, As he does every year" +
                " on Halloween after the snooker accident, his restless soul travels the earth. " +
                "Demons approaches Bob promising rest if he can make it to his grave. the sly Demon " +
                "failed to mention that there will be more of them hiding in the dark.");
            label10.Image = Properties.Resources.gravestone;
            showSpriteAt(atCol, atRow); //set ghost at location
            PlaceBombs(40); //planting the bombs
            lbltimer.Text = "90";
            timer1.Start();
            
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbltimer.Text = (int.Parse(lbltimer.Text) - 1).ToString();
            if (int.Parse(lbltimer.Text) == 0)  //if the countdown reaches '0', we stop it
            {
                endgame();
            }
        }

        private void Btnreset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        /******************************************************************************
        * END
        ******************************************************************************/
    }
}