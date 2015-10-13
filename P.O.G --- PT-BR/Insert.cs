using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POG_BR
{
    public partial class Insert : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
  (
      int nLeftRect, // x-coordinate of upper-left corner
      int nTopRect, // y-coordinate of upper-left corner
      int nRightRect, // x-coordinate of lower-right corner
      int nBottomRect, // y-coordinate of lower-right corner
      int nWidthEllipse, // height of ellipse
      int nHeightEllipse // width of ellipse
   );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;

        }

        public Insert()
        {
            InitializeComponent();
        }

        private int X;
        private int Y;

        public Boolean Verify()
        {
            if (txtCommand.Text.Equals(""))
                return false;
            else if (txtProcess.Text.Equals(""))
                return false;
            else if (txtURL.Text.Equals(""))
                return false;

            return true;
        }

        public Boolean Verify2()
        {
            if (txtCommandSITE.Text.Equals("") || txtURLSITE.Text.Equals(""))
                return false;

            return true;
           
        }

        private void btnFILE_Click(object sender, EventArgs e)
        {
           _File.Filter = "Applications (*.exe)|*.exe";

            if(_File.ShowDialog() == DialogResult.OK)
            {
                txtURL.Text = _File.FileName.ToString();
                string path = _File.SafeFileName.ToString();
                //string newpath;
              //  for (int x = 0; x < path.Length; x++)
               // {

                    //if (path.ToString().Substring(x, 4) == ".exe")
                    //{
                    //    newpath = path.ToString().Remove(x, 4);
                        txtProcess.Text = path.ToString();
                    }
          }
            
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (Verify())
            {
                String URL = txtURL.Text;
                String Process = txtProcess.Text;
                String Command = txtCommand.Text;
                String FileText = "";
                FileText = Command + "@" + URL + "@" + Process +"@"+"0\n";
                System.IO.File.AppendAllText(@"C:\\Users\\User\\Desktop\\POG\\POG BR\\POG BR\\Gramatica\\DATABASE.pog", FileText);
                this.Close();
            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if(Verify2())
            {
                String Command = txtCommandSITE.Text;
                String URL = txtURLSITE.Text;
                String FILE = Command + "@" + URL + "@" + " " + "@" + "1\n";
                System.IO.File.AppendAllText(@"C:\\Users\\User\\Desktop\\POG\\POG BR\\POG BR\\Gramatica\\DATABASE.pog",FILE);
                this.Close();
            }
        }

        private void Insert_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            X = this.Left - MousePosition.X;
            Y = this.Top - MousePosition.Y;
        }

        private void Insert_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left = X + MousePosition.X;
            this.Top = Y + MousePosition.Y;
        }
    }
}
