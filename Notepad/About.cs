using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class About : Form
    {
        SoundPlayer simpleSound = new SoundPlayer("sigmamusic.wav");
        public About()
        {
            InitializeComponent();
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            simpleSound.Stop(); ;
        }

        private void About_Load(object sender, EventArgs e)
        {
            simpleSound.Play();
        }
    }
}
