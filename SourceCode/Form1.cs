using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace QuickCapture
{
    public partial class Form1 : Form
    {
        private String saveDirectory = "";
        private int saveCounter = 0;
        private PrintScreenKeyHandler keyHandler = null;

        public Form1()
        {
            InitializeComponent();
            keyHandler = new PrintScreenKeyHandler(Keys.PrintScreen, this);
            keyHandler.Register();
        }

        private void HandleHotkey()
        {
            captureFullScreen();
        }

        protected override void WndProc(ref Message msg)
        {
            if (KeyConstant.WM_HOTKEY_MSG_ID == msg.Msg)
            {
                HandleHotkey();
            }
            base.WndProc(ref msg);
        }

        private void enableQuickCapture_Click(object sender, EventArgs e)
        {
            enableQuickCapture.Enabled = false;
            disableQuickCapture.Enabled = true;
            
            DirectoryInfo LocalDirectory = Directory.CreateDirectory(string.Format((saveScreenshotPath.Text + @"\\{0}-{1}-{2}-{3}-{4}"), DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute));
            saveDirectory = LocalDirectory.FullName;
            
            captureFullScreen();
        }

        private void disableQuickCapture_Click(object sender, EventArgs e)
        {
            enableQuickCapture.Enabled = true;
            disableQuickCapture.Enabled = false;
        }

        private void captureFullScreen()
        {
            Image img = ScreenCapture.CaptureFullscreen();
            img.Save(saveDirectory + @"\" + saveCounter.ToString() + @".jpg", ImageFormat.Jpeg);
            saveCounter++;
            img.Dispose();
        }
    }
}
