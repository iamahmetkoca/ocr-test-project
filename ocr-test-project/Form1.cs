using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronOcr;
using System.Speech.Synthesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ocr_test_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            try
            {
                openImage.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpeg;*.jpg)|*.jpeg;*.jpg|All files (*.*)|*.*";

                if (openImage.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openImage.FileName;
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error"  +exception.Message);
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            try
            {
                if(pictureBox1.Image != null)
                {
                    var Ocr = new IronTesseract();
                    var result = Ocr.Read(openImage.FileName);
                    rtxtResult.Text = result.Text;
                }
                else
                {
                    MessageBox.Show("Not selected image");

                }


            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + exception.Message);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                pictureBox1.Invalidate();
            }

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Image img = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = img;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( rtxtResult.Text!= null )
            {
                Clipboard.SetText(rtxtResult.Text);
            }

        }
        private SpeechSynthesizer synth = new SpeechSynthesizer();
        private void button2_Click(object sender, EventArgs e)
        {
            string textToSpeak = rtxtResult.Text;

            if (rtxtResult.Text != null)
            {
                synth.SpeakAsync(textToSpeak);
            }
        }
    }
}
