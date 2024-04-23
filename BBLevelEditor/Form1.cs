using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BBLevelEditor
{
    public partial class Form1 : Form
    {
        string filePath = "C:/Users/Rowan Locke/3D Objects/level7.xml";
        XmlWriterSettings settings = new XmlWriterSettings();
        public Form1()
        {
            InitializeComponent();
            
            settings.Indent = true; // Enable indentation for readability
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            XmlWriter writer = XmlWriter.Create(filePath, settings);
            writer.WriteStartDocument();

            // Write the root element
            writer.WriteStartElement("level");
            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                        WriteData(writer, c.Location.X, c.Location.Y, c.Text, c.BackColor);
                }
            }
            writer.WriteEndElement();
            // End the root element

            // End the XML document
            writer.Close();
        }
        static void WriteData(XmlWriter writer, int x, int y, string hp, Color color)
        {
            writer.WriteStartElement("brick");
            writer.WriteElementString("x", "" + x );
            writer.WriteElementString("y", "" + y);
            writer.WriteElementString("hp", hp);
            writer.WriteElementString("colour", color.Name);
            writer.WriteEndElement();
        }

        private void button54_Click(object sender, EventArgs e)
        {

        }
    }
}
