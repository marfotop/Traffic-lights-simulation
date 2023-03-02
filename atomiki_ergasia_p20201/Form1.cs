using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomiki_ergasia_p20201
{
    public partial class Form1 : Form
    {
        Timer[] timers = new Timer[3];  // timers for vertical cars
        Timer[] timers2 = new Timer[3]; // timers for horizontal cars
        PictureBox[] v_cars = new PictureBox[3];  // list of vertical cars
        PictureBox[] h_cars = new PictureBox[3];  // list of horizontal cars
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            v_cars[0] = pictureBox3;
            v_cars[1] = pictureBox4;
            v_cars[2] = pictureBox5;
            h_cars[0] = pictureBox6;
            h_cars[1] = pictureBox7;
            h_cars[2] = pictureBox8;
            timers[0] = timer3;
            timers[1] = timer4;
            timers[2] = timer5;
            timers2[0] = timer7;
            timers2[1] = timer8;
            timers2[2] = timer9;
        }

        // Drawing the road 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // left
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // top
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // right
               Color.RosyBrown, 23, ButtonBorderStyle.Solid);// bottom
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // left
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // top
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // right
               Color.RosyBrown, 23, ButtonBorderStyle.Solid);// bottom
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // left
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // top
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // right
               Color.DarkGray, 0, ButtonBorderStyle.Solid);// bottom
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // left
               Color.RosyBrown, 23, ButtonBorderStyle.Solid, // top
               Color.DarkGray, 0, ButtonBorderStyle.Solid, // right
               Color.DarkGray, 0, ButtonBorderStyle.Solid);// bottom
        }

        // control of traffic lights
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (count < 2)
            {
                pictureBox1.ImageLocation = "vertical_red.png";
            }
            else if (count < 8)
                pictureBox2.ImageLocation = "horizontal_green.png";
            else if (count < 13)
            {
                pictureBox2.ImageLocation = "horizontal_yellow.png";
            }
            else if (count < 15)
            {
                pictureBox2.ImageLocation = "horizontal_red.png";

            }
            else if (count < 17)
                pictureBox1.ImageLocation = "vertical_green.png";
            else if (count < 19)
            {
                pictureBox1.ImageLocation = "vertical_yellow.png";
            }
            else
                count = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer6.Enabled = true;
        }

        // control of vertical cars
        private void timer2_Tick(object sender, EventArgs e)
        {
            // green or yellow vertical traffic light
            if (pictureBox1.ImageLocation == "vertical_green.png" || 
                pictureBox1.ImageLocation == "vertical_yellow.png")
            {
                for (int i=0; i<v_cars.Length; i++)
                {
                    if (v_cars[i].Location.Y > 0)
                        v_cars[i].Location =
                            new Point(v_cars[i].Location.X, v_cars[i].Location.Y - 1);
                    else
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 600);
                }
            }
            else  // red traffic light
            {
                for (int i=0; i<v_cars.Length; i++)
                {
                    // if car is just behind the red traffic light
                    if (v_cars[i].Location.Y <= 289 &&
                        v_cars[i].Location.Y >= 288)
                    {
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 288);
                        timers[i].Enabled = false;
                    }
                    // if car is second at the queue of red traffic light and has one car in front of it
                    else if (i > 0 && v_cars[i].Location.Y <= 371
                        && v_cars[i].Location.Y >= 370 && v_cars[i-1].Location.Y <=
                        289 && v_cars[i-1].Location.Y >= 288)
                    {
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 370);
                        timers[i].Enabled = false;
                    }
                    // if car is third at the queue of red traffic light and has two cars in front of it
                    else if (i > 1 && v_cars[i].Location.Y <= 454
                        && v_cars[i].Location.Y >= 453 && v_cars[i-2].Location.Y <=
                        289 && v_cars[i-2].Location.Y >= 288)
                    {
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 453);
                        timers[i].Enabled = false;
                    }
                    // if the first car is now last at the queue
                    else if (i==0 && v_cars[i].Location.Y <= 454
                        && v_cars[i].Location.Y >= 453 && v_cars[i+2].Location.Y <=
                        371 && v_cars[i+2].Location.Y >= 370)
                    {
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 453);
                        timers[i].Enabled = false;
                    }
                    // if car has passed the red traffic light and is in front of it, so it doesn't need to stop
                    else if (v_cars[i].Location.Y > 0)
                        v_cars[i].Location =
                            new Point(v_cars[i].Location.X, v_cars[i].Location.Y - 1);
                    else
                        v_cars[i].Location = new Point(v_cars[i].Location.X, 600);
                }
            }
        }

        // control of horizontal cars
        private void timer6_Tick(object sender, EventArgs e)
        {
            // green or yellow horizontal traffic light
            if (pictureBox2.ImageLocation == "horizontal_green.png" ||
                pictureBox2.ImageLocation == "horizontal_yellow.png")
            {
                for (int i=0; i<h_cars.Length; i++)
                {
                    if (h_cars[i].Location.X >= -50 && h_cars[i].Location.X < 720)
                        h_cars[i].Location = new Point(h_cars[i].Location.X + 1, h_cars[i].Location.Y);
                    else
                        h_cars[i].Location = new Point(-50, h_cars[i].Location.Y);
                }
            }
            else  // red traffic light
            {
                for (int i=0; i<h_cars.Length; i++)
                {
                    // if car is just behind the red traffic light
                    if (h_cars[i].Location.X >= 183 && h_cars[i].Location.X <= 184)
                    {
                        h_cars[i].Location = new Point(184, h_cars[i].Location.Y);
                        timers2[i].Enabled = false;
                    }
                    // if car is second at the queue of red traffic light and has one car in front of it
                    else if (i > 0 && h_cars[i].Location.X >= 90
                        && h_cars[i].Location.X <= 91 && h_cars[i - 1].Location.X >=
                        163 && h_cars[i - 1].Location.X <= 184)
                    {
                        h_cars[i].Location = new Point(91, h_cars[i].Location.Y);
                        timers[i].Enabled = false;
                    }
                    // if car is third at the queue of red traffic light and has two cars in front of it
                    else if (i > 1 && h_cars[i].Location.X >= 0
                        && h_cars[i].Location.X <= 1 && h_cars[i - 2].Location.X >=
                        183 && h_cars[i - 2].Location.X <= 184)
                    {
                        h_cars[i].Location = new Point(1, h_cars[i].Location.Y);
                        timers[i].Enabled = false;
                    }
                    // if the first car is now last at the queue
                    else if (i==0 && h_cars[i].Location.X >= 0
                        && h_cars[i].Location.X <= 1 && h_cars[i + 1].Location.X >=
                        184 && h_cars[i + 1].Location.X <= 185)
                    {
                        h_cars[i].Location = new Point(0, h_cars[i].Location.Y);
                        timers[i].Enabled = false;
                    }
                    // if car has passed the red traffic light and is in front of it, so it doesn't need to stop
                    else
                    {
                        if (h_cars[i].Location.X >= -50 && h_cars[i].Location.X < 720)
                            h_cars[i].Location = new Point(h_cars[i].Location.X + 1, h_cars[i].Location.Y);
                        else
                            h_cars[i].Location = new Point(-50, h_cars[i].Location.Y);
                    }
                }
            }
        }
    }
}
