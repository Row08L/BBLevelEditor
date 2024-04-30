using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BBLevelEditor
{
    public partial class Form1 : Form
    {
        XmlDocument level = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            level = new XmlDocument();
            List<Image> encodeImageList = new List<Image>();
            XmlNode root = level.CreateElement("level");
            level.AppendChild(root);
            //if (File.Exists(filePath) == true)
            //{
            //    level.Load(filePath);
            //}

            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                    if (encodeImageList.Count() != 0)
                    {
                        int index = 0;
                        foreach (Image i in encodeImageList)
                        {
                            if (i == c.BackgroundImage)
                            {
                                break;
                            }
                            index++;
                        }
                        if (index == encodeImageList.Count())
                        {
                            encodeImageList.Add(c.BackgroundImage);
                        }
                    }
                    else
                    {
                        encodeImageList.Add(c.BackgroundImage);
                    }
                }
            }
            if (encodeImageList.Count > 0)
            {
                WriteDecoder(encodeImageList, level, root);
            }
            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                    bool vines = false;
                    int type = 0;
                    foreach (Image i in encodeImageList)
                    {

                        if (i == c.BackgroundImage)
                        {
                            break;
                        }
                        type++;
                    }
                    if (c.ForeColor == Color.Green)
                    {
                        vines = true;
                    }
                    
                    WriteDataBrick(level, c.Location.X, c.Location.Y, c.Text, type, c.Width, c.Height, vines, root);
                }
            }
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string parent1 = Directory.GetParent(currentDirectory).FullName;
            string parent2 = Directory.GetParent(parent1).FullName;
            string parent3 = Directory.GetParent(parent2).FullName;

            string fullPath = Path.Combine(parent3, "levels", "level" + levelNumberInput.Value);
            if (panel1.BackgroundImage != null)
            {
                string base64Back = ImageToBase64(panel1.BackgroundImage);
                XmlElement backround = level.CreateElement("backround");
                backround.InnerText = base64Back;
                level.AppendChild(backround);
            }

            level.Save(fullPath);
        }

        static void WriteDataBrick(XmlDocument xmlDoc, int x, int y, string hp, int type, int width, int hight, bool vines, XmlNode root)
        {
            XmlNode brick = xmlDoc.CreateElement("brick");
            root.AppendChild(brick);
            XmlElement rectangle = xmlDoc.CreateElement("rectangle");
            brick.AppendChild(rectangle);
            XmlElement xmlX = xmlDoc.CreateElement("x");
            xmlX.InnerText = "" + x;
            rectangle.AppendChild(xmlX);
            XmlElement xmlY = xmlDoc.CreateElement("y");
            xmlY.InnerText = "" + y;
            rectangle.AppendChild(xmlY);
            XmlElement xmlWidth = xmlDoc.CreateElement("width");
            xmlWidth.InnerText = "" + width;
            rectangle.AppendChild(xmlWidth);
            XmlElement xmlHight = xmlDoc.CreateElement("hight");
            xmlHight.InnerText = "" + hight;
            rectangle.AppendChild(xmlHight);
            XmlElement xmlHp = xmlDoc.CreateElement("hp");
            xmlHp.InnerText = "" + hp;
            brick.AppendChild(xmlHp);
            XmlElement xmlBrickType = xmlDoc.CreateElement("brickType");
            xmlBrickType.InnerText = "" + type;
            brick.AppendChild(xmlBrickType);
            XmlElement xmlVines = xmlDoc.CreateElement("vines");
            xmlVines.InnerText = "" + vines;
            brick.AppendChild(xmlVines);
        }

        static void WriteDecoder(List<Image> imageList, XmlDocument xmlDoc, XmlNode root)
        {
            XmlNode textures = xmlDoc.CreateElement("textures");
            root.AppendChild(textures);
            foreach (Image i in imageList)
            {
                string base64Image = "";
                if (i != null)
                {
                    base64Image = ImageToBase64(i);
                }
                XmlElement texture = xmlDoc.CreateElement("texture");
                texture.InnerText = base64Image;
                textures.AppendChild(texture);
            }
        }

        static string ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert the image to a byte array

                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                // Convert the byte array to a base64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {

        }
    }
}
