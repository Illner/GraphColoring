using System.Drawing;
using System.Windows.Forms;

namespace GraphColoring.GUI
{
    public partial class GraphVisualizationForm : Form
    {
        // Variable
        private bool Dragging;
        private int xPos;
        private int yPos;

        public GraphVisualizationForm(Image image)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            graphVisualizationPictureBox.Image = image;
        }

        private void GraphVisualizationForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void GraphVisualizationForm_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (Dragging && c != null)
            {
                c.Top = e.Y + c.Top - yPos;
                c.Left = e.X + c.Left - xPos;
            }
        }

        private void GraphVisualizationForm_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
    }
}
