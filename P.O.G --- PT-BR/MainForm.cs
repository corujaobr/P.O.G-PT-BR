using LapsAPI;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using POG_BR.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

namespace POG_BR
{
    public partial class MainForm : Form
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


        private SREngine lapsamEngine = null;
        private ListaComandos lc = new ListaComandos();
        private Boolean open, lapsamOn, close,OpenSite;
        private SpeechRecognitionEngine microsftEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        private SpeechSynthesizer SpeechEngine = new SpeechSynthesizer();  
        private int X, Y;
        public MainForm()
        {
            InitializeComponent();
            SREngine.OnRecognition += handleResult;//BR recognition
            microsftEngine.SetInputToDefaultAudioDevice();//US
            Choices programs = new Choices();//US
            programs.Add(getDATABASE());//US
            GrammarBuilder gb = new GrammarBuilder();//US
            gb.Append(programs);//US
            Microsoft.Speech.Recognition.Grammar g = new Microsoft.Speech.Recognition.Grammar(gb);//US
            microsftEngine.LoadGrammar(g);//US
            microsftEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);//US
            SpeechEngine.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(handleSpeechStarted);//BR speaker
            SpeechEngine.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(handleSpeechDone);//BR speaker
            SpeechEngine.SetOutputToDefaultAudioDevice();//BR speaker
            open = false;
            close = false;
            lapsamOn = true;
        }

        public String[] getDATABASE()
        {
            StreamReader reader = new StreamReader(@"C:\\Users\\User\\Desktop\\POG\\POG BR\\POG BR\\Gramatica\\DATABASE.pog");
            String line = "";
            String[] finalDATA;
            while ((line = reader.ReadLine())!=null)
            {
                String[] builder = line.Split('@');
                lc.inserirOrdenado(new ComandoNo(builder[0],builder[1],builder[2],Convert.ToInt32(builder[3])));
            }
            ComandoNo atual = lc.getPrimeiro();
            finalDATA = new String[lc.getTamanho()];
            for (int ct=0;ct<lc.getTamanho();ct++)
            {
                finalDATA[ct] = atual.getComando();
                atual = atual.getPosterior();
            }
            return finalDATA;
        }

        public void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            ComandoNo no = lc.pesquisaComando(e.Result.Text);
            String URL_SorP = no.getOperacaoA();

           
            if (open )
            {
                if (URL_SorP.Substring(0, 3).Equals("C:\\"))
                {
                    SpeechEngine.SpeakAsync("ABRINDO " + e.Result.Text);
                    Process proc = new Process();
                    proc.StartInfo.FileName = no.getOperacaoA();
                    proc.Start();
                    microsftEngine.RecognizeAsyncStop();
                    recognitionMode(0);
                }
                else
                {
                    SpeechEngine.SpeakAsync("DESCULPE SENHOR, NÃO CONSEGUI ABRIR O PROGRAMA");
                    recognitionMode(0);
                }
            }
            else if (OpenSite)
            {
                
                    if (URL_SorP.Substring(0, 8).Equals("https://") || URL_SorP.Substring(0, 7).Equals("http://"))
                    {
                        SpeechEngine.SpeakAsync("ABRINDO " + e.Result.Text);
                        Process proc = new Process();
                        proc.StartInfo.FileName = no.getOperacaoA();
                        proc.Start();
                        microsftEngine.RecognizeAsyncStop();
                        recognitionMode(0);
                    }
                else
                {
                    SpeechEngine.SpeakAsync("DESCULPE SENHOR, NÃO CONSEGUI ABRIR O SITE");
                    recognitionMode(0);
                }
            }
            else if (close)
            {
                closeProgramByName(no.getOperacaoF());
                SpeechEngine.SpeakAsync("FECHANDO" + e.Result.Text);
                microsftEngine.RecognizeAsyncStop();
                recognitionMode(0);
            }
            microsftEngine.RecognizeAsyncStop();
            recognitionMode(0);
        }

        public void recognitionMode(int mode)
        {
            if(mode == 0)
            {
                lapsamOn = true;
                open = false;
                close = false;
            }else if (mode == 1)
            {
                lapsamOn = false;
                open = true;
                close = false;
            }
            else if(mode==3)
            {
                lapsamOn = false;
                open = false;
                close = false;
                OpenSite = true;
            }
            else if(mode == 2)
            {
                lapsamOn = false;
                open = false;
                close = true;
            }
        }

        public void handleResult(RecoResult result)
        {
            String res = result.getUterrance();
            res = res.Remove(0, 5);
            res = res.Remove(res.Length - 5, 5);
            res = res.Replace('_', ' ');
            if (result.getConfidence() > 0.99999 && lapsamOn == true)
            {
                if (res.ToUpper().Equals("QUERO ABRIR UM PROGRAMA"))
                {
                    Talking(1);
                    recognitionMode(1);
                    microsftEngine.RecognizeAsync();
                }
                else if (res.ToUpper().Equals("QUERO FECHAR UM PROGRAMA"))
                {
                    Talking(2);
                    recognitionMode(2);
                    microsftEngine.RecognizeAsync();
                }
                else if(res.ToUpper().Equals("QUERO ABRIR UM SITE"))
                {
                    Talking(4);
                    recognitionMode(3);
                    microsftEngine.RecognizeAsync(); 
                }
            }
        }

        public void handleSpeechDone(Object sender, SpeakCompletedEventArgs e)
        {
            waveGif.Image = null;
        }

        public void handleSpeechStarted(Object sender, SpeakStartedEventArgs e)
        {
            waveGif.Image = Resources.gif_ondas2;
        }

        public void Talking(int CHOOSE)
        {
            if (CHOOSE == 1)
            {
                SpeechEngine.SpeakAsync("QUE PROGRAMA O SENHOR DESEJA ABRIR?");
            }
            else if (CHOOSE == 2)
            {
                SpeechEngine.SpeakAsync("QUE PROGRAMA O SENHOR DESEJA FECHAR?");
            }
            else if (CHOOSE == 3)
            {
                SpeechEngine.SpeakAsync("QUAL PROGRAMA VOCE QUER ABRIR?");
            }
            else if(CHOOSE == 4)
            {
                SpeechEngine.SpeakAsync("QUAL SAITE O SENHOR DESEJA ABRIR");
            }
        }

        private void closeProgramByName(String processName)
        {
            Process[] proc = Process.GetProcessesByName(processName);

            foreach (Process pr in proc)
            {
                pr.CloseMainWindow();
                pr.Kill();
                pr.Close();
            }
        }

        private void btnInserir_MouseHover(object sender, EventArgs e)
        {
            pcNEON1.Image = Resources.neon_effect_1;
            btnInserir.ForeColor = Color.Orange;
        }

        private void btnInserir_MouseLeave(object sender, EventArgs e)
        {
            pcNEON1.Image = null;
            btnInserir.ForeColor = Color.Black;
        }

        private void btnSair_MouseHover(object sender, EventArgs e)
        {
            pcNEON2.Image = Resources.neon_effect_1;
            btnSair.ForeColor = Color.Orange;
        }

        private void btnSair_MouseLeave(object sender, EventArgs e)
        {
            pcNEON2.Image = null;
            btnSair.ForeColor = Color.Black;
        }

        private void lbX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbX_MouseHover(object sender, EventArgs e)
        {
            lbX.ForeColor = Color.Black;
            lbX.BackColor = Color.Orange;
        }

        private void lbX_MouseLeave(object sender, EventArgs e)
        {
            lbX.ForeColor = Color.White;
            lbX.BackColor = Color.Transparent;
        }

        private void LbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LbMinimize_MouseHover(object sender, EventArgs e)
        {
            LbMinimize.ForeColor = Color.Black;
            LbMinimize.BackColor = Color.Orange;
        }

        private void LbMinimize_MouseLeave(object sender, EventArgs e)
        {
            LbMinimize.ForeColor = Color.White;
            LbMinimize.BackColor = Color.Transparent;
        }

        private void lbX_MouseDown(object sender, MouseEventArgs e)
        {
            lbX.ForeColor = Color.White;
            lbX.BackColor = Color.Transparent;
        }

        private void lbX_MouseUp(object sender, MouseEventArgs e)
        {
            lbX.ForeColor = Color.Black;
            lbX.BackColor = Color.Orange;
        }

        private void LbMinimize_MouseDown(object sender, MouseEventArgs e)
        {
            LbMinimize.ForeColor = Color.White;
            LbMinimize.BackColor = Color.Transparent;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            X = this.Left - MousePosition.X;
            Y = this.Top - MousePosition.Y;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left = X + MousePosition.X;
            this.Top = Y + MousePosition.Y;
        }

        private void LbMinimize_MouseUp(object sender, MouseEventArgs e)
        {
            lbX.ForeColor = Color.Black;
            lbX.BackColor = Color.Orange;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lapsamEngine = new SREngine(@".\LaPSAM\LaPSAM1.5.jconf");
            lapsamEngine.loadGrammar(@".\Gramatica\grammar.xml");
            lapsamEngine.startRecognition();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Insert INS = new Insert();
            INS.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
