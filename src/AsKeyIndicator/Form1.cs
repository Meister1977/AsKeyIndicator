using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsKeyIndicator
{
    public partial class Form1 : Form
    {
        private bool _mouseDown;
        private Point _lastLocation;
        private TransparentPanel _panel1;

        public Form1()
        {
            InitializeComponent();
        }

        private void keyChecker_Tick(object sender, EventArgs e)
        {
            checkBox1.Checked = IsKeyLocked(Keys.NumLock);

            if (checkBox1.Checked)
                return;

            NativeMethods.SimulateKeyPress(NativeMethods.VkNumlock);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _panel1 = new TransparentPanel();
            _panel1.MouseMove += panel1_MouseMove;
            _panel1.MouseUp += panel1_MouseUp;
            _panel1.MouseDown += panel1_MouseDown;
            _panel1.MouseDoubleClick += panel1_MouseDoubleClick;
            _panel1.AutoSize = false;
            Size = checkBox1.Size;
            _panel1.Size = checkBox1.Size;

            Controls.Add(_panel1);

            _panel1.BringToFront();

            _panel1.Location = new Point(0, 0);
            keyChecker.Start();
            Location = new Point(1400, 0);
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastLocation = e.Location;
            _panel1.Size = _panel1.Parent.ClientSize;
            _panel1.MaximumSize = _panel1.Parent.ClientSize;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                Location = new Point(
                    (Location.X - _lastLocation.X) + e.X, (Location.Y - _lastLocation.Y) + e.Y);

                Update();
            }
        }
    }
}
