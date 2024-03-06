using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Pawn_promotion : Form
    {
        public char selected_item;
        public Pawn_promotion()
        {
            InitializeComponent();
        }
        private void queen_Click(object sender, EventArgs e)
        {
            selected_item = 'q';
            this.Close();
        }
        private void rook_Click(object sender, EventArgs e)
        {
            selected_item = 'r';
            this.Close();
        }
        private void Bishop_Click(object sender, EventArgs e)
        {
            selected_item = 'b';
            this.Close();
        }
        private void Knight_Click(object sender, EventArgs e)
        {
            selected_item = 'n';
            this.Close();
        }
    }
}