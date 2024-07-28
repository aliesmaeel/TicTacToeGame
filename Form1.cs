using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeGame.Properties;

namespace TicTacToeGame
{
    public partial class TicTacToe : Form
    {
        string Player;
        private List<PictureBox> pictureBoxes;

        public TicTacToe()
        {
            InitializeComponent();
            Player = "Player1";
            string Winner;
            InitializePictureBoxList();
        }
        private void InitializePictureBoxList()
        {
            pictureBoxes = new List<PictureBox> { pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9 };
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color Black = Color.FromArgb(255, 0, 0, 0);
            Pen Pen = new Pen(Black);
            Pen.Width = 10;
            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            e.Graphics.DrawLine(Pen, 520,this.Height, 520, 0);
            e.Graphics.DrawLine(Pen, 710,this.Height, 710, 0);
            e.Graphics.DrawLine(Pen, 340, 400, 900, 400);
            e.Graphics.DrawLine(Pen, 340, 196, 900, 196);
        }
        private string ChangePlayer()
        {
            Player = Player == "Player1" ? "Player2" : "Player1";
            return Player;
        }
        private void UpdatePlayerLabel(string Player)
        {
            lb_player.Text = Player;
        }
        private void img_clicked(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            if (pictureBox.Tag == "?")
            {
                ShowImage(Player, sender);

                pictureBox.Tag = Player == "Player1" ? "x" : "o";

                if (IsWinner(Player, sender))
                {
                    lb_winner.Text = Player;
                    lb_player.Text = GameOver();
                    MessageBox.Show(Player + " Is The Winner ");
                }
                else if(IsDraw())
                {
                    MessageBox.Show("Draw");
                    lb_winner.Text = "Draw";
                    lb_player.Text = GameOver();
                }
                else
                {
                    Player = ChangePlayer();
                    UpdatePlayerLabel(Player);
                }
            }
            else
            {
                MessageBox.Show("You can't Choose This Because It's Already Taken", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GameOver()
        {
            DisableImages();
            return "Game Over";
        }
        private void DisableImages()
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Enabled = false;
            }
        }

        private bool IsWinner(string Player,Object sender)
        {
            string str = Player == "Player1" ? "x" : "o";

            if (CheckPictures(pic1, pic2, pic3, str))
                return true;
            if (CheckPictures(pic4, pic5, pic6, str))
                return true;
            if (CheckPictures(pic7, pic8, pic9, str))
                return true;
            if (CheckPictures(pic1, pic5, pic9, str))
                return true;
            if (CheckPictures(pic3, pic5, pic7, str))
                return true;
            if (CheckPictures(pic1, pic4, pic7, str))
                return true;
            if (CheckPictures(pic2, pic5, pic8, str))
                return true;
            if (CheckPictures(pic3, pic6, pic9, str))
                return true;

            return false;
        }

        private bool IsDraw()
        {
            bool result = true;

            foreach(var pictureBox in pictureBoxes)
            {
                result = result && pictureBox.Tag.ToString() != "?";
            }

            return result;
        }



        private bool CheckPictures(PictureBox pic1,PictureBox pic2,PictureBox pic3,string str)
        {

            if (pic1.Tag.ToString() == str && pic2.Tag.ToString() == str && pic3.Tag.ToString() == str)
                return true;

            return false;
        }

        private void ShowImage(string Player,Object sender)
        {
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.BackgroundImage = Player == "Player1"? Resources.x: Resources.opng;
        }

        private void b_reset_Click(object sender, EventArgs e)
        {
            lb_player.Text = "Player1";
            lb_winner.Text = "In Progress";

            foreach(var pictureBox in pictureBoxes)
            {
                ResetPictures(pictureBox);
            }
          
        }
        private void ResetPictures(PictureBox pic)
        {
            pic.Tag = "?";
            pic.BackgroundImage = Resources.question;
            pic.Enabled = true;
        }
    }
}
