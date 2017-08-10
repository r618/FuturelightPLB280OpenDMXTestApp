//
// Futurelight PLB 280 + OpenDMX test app
//
// Copyright (c) 2017 Martin Cvengros
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using r618;
using System;
using System.Windows.Forms;

namespace FuturelightPLB280OpenDMXTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenDMX.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OpenDMX.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Pan((byte)(sender as TrackBar).Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Tilt((byte)(sender as TrackBar).Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Dimmer((byte)(sender as TrackBar).Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Shutter_Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Shutter_Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Prism_Open();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Prism_6Facet();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Prism_8Facet();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Prism_Frost();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Focus((byte)(sender as TrackBar).Value);
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Zoom((byte)(sender as TrackBar).Value);
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Color((byte)(sender as TrackBar).Value);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int channel = (byte)int.Parse(this.textBox1.Text);
            byte value = (byte)int.Parse(this.textBox2.Text);

            Console.WriteLine(string.Format("[CHANNEL:{0}] [VALUE:{1}]", channel, value));

            OpenDMX.SetDmxValue(channel, value);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Lamp_ON();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FuturelightPLB280OpenDMX.Lamp_OFF();
        }
    }
}
