using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace ตัวอย่าง2
{
    public partial class Form1 : Form
    {
        OpenFileDialog op = new OpenFileDialog(); //อย่างลืมเรียกclass มาใช้นะ
        SaveFileDialog sa = new SaveFileDialog();
        Image<Rgb, byte> imagecome;
        Image<Rgb, byte> imageprocess ;
        public Form1()
        {
            InitializeComponent();
            op.Filter = "JPEG file(*.JPG|*.Jpg|All file(*.*)|*.*)";
            sa.Filter = "JPEG file(*.JPG|*.Jpg|All file(*.*)|*.*)";
        }

        private void button1_Click(object sender, EventArgs e) //รับภาพ
        {
            if (op.ShowDialog() == DialogResult.OK) //op.showdialog เป็นการบอกว่า ถ้ามีการกด เปิดไดอะล็อก ให้ เข้ามาในนี้
            {
                imagecome = new Image<Rgb, byte>(op.FileName);
                imageBox1.Image = imagecome;
                textBox1.Text = imagecome.Width.ToString();
                textBox2.Text = imagecome.Height.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e) //โหลดภาพ
        {
            if (imagecome != null)
            {
                if (op.ShowDialog() == DialogResult.OK)
                {
                    imageprocess = imagecome.Convert<Rgb, byte>();
                    imageprocess.Save(sa.FileName); //save file
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //แปลงภาพเทา
        {
            if (imagecome != null)
            {
                imageprocess = imagecome.Convert<Rgb, byte>();
                Image<Gray, byte> G1 = imageprocess.Convert<Gray, byte>();
                imageBox2.Image = G1;
                textBox3.Text = G1.Width.ToString();
                textBox4.Text = G1.Height.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e) //ปรับขนาดภาพ
        {
            if (imagecome != null)
            {
                imageprocess = imagecome.Convert<Rgb, byte>(); //รับค่าภาพ
                                                               // Image<Gray, byte> G1 = imageprocess.Convert<Gray,byte>();
                Image<Rgb, Byte> image1 = imageprocess.Resize(100, 100, Emgu.CV.CvEnum.Inter.Nearest);
                imageBox2.Image = image1;
                textBox3.Text = image1.Width.ToString();
                textBox4.Text = image1.Height.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e) //ปรับแสงให้ ใสขึ้น
        {
            if(imagecome != null)
            {
                imageprocess = imagecome.Mul(1.5);
                imageBox2.Image = imageprocess;
                textBox3.Text = imageprocess.Width.ToString();
                textBox4.Text = imageprocess.Height.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(imagecome != null)
            {
                imageprocess = Crop(imagecome, new Point(200, 200), new Size(500, 500));
                //input1= ภาพที่ต้องการ,input2 = จุดเริ่มต้น , input3 = ขนาด ที่ต้องการ
                imageBox2.Image = imageprocess;
                textBox3.Text = imageprocess.Width.ToString();
                textBox4.Text = imageprocess.Height.ToString();
            }
        }
        public static Image<Rgb,byte>Crop(Image<Rgb,byte>image0,Point start ,Size s) //ฟังชั้นการตัด 
        {

            image0.ROI = new Rectangle(start,s);//ตีพน4เหลี่ยม
            Image<Rgb, byte> image1 = image0.Copy();
            return image1;
        }

        private void button7_Click(object sender, EventArgs e) // การหมุนภาพ 
        {
            if(imagecome != null)
            {
                imageprocess = imagecome.Rotate(45, new Rgb(0, 0, 0), true); 
                //inputแรกคือการหมุน ,input2 คือการปรับสีพื้นหลัง , input 3 คือการ ตัด True =ตัด false = ไม่ตัด 
                imageBox2.Image = imageprocess ;
                textBox3.Text = imageprocess.Width.ToString();
                textBox4.Text = imageprocess.Height.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(imagecome != null)
            {
                imageprocess = imagecome.SmoothBlur(50, 50);//เรียกใช้ฟังชั้นเบอ
                imageBox2.Image = imageprocess;
                textBox3.Text = imageprocess.Width.ToString();
                textBox4.Text = imageprocess.Height.ToString();
            }
        }
    }
}
