using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsKeyIndicator
{
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private TransparentPanel panel1;

        public Form1()
        {
            InitializeComponent();
        }

        private void keyChecker_Tick(object sender, EventArgs e)
        {
            checkBox1.Checked = Control.IsKeyLocked(Keys.NumLock);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1 = new TransparentPanel();
            panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            panel1.MouseDoubleClick += new MouseEventHandler(panel1_MouseDoubleClick);
            panel1.AutoSize = false;
            Size = checkBox1.Size;
            panel1.Size = checkBox1.Size;
            
            Controls.Add(panel1);

            panel1.BringToFront();

            panel1.Location = new Point(0, 0);
            keyChecker.Start();
            Location = new Point(1400, 0);
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
            panel1.Size = panel1.Parent.ClientSize;
            panel1.MaximumSize = panel1.Parent.ClientSize;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point(
                    (Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }
    }
}
