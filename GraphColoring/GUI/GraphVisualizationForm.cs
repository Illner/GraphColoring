using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GraphColoring.GUI
{
    public partial class GraphVisualizationForm : Form
    {
        // Variable
        private int xPos;
        private int yPos;
        private bool Dragging;
        private int MINMAX = 3;             // 3 times bigger or smaller than the ctrl
        private double ZOOMFACTOR = 1.25;   // = 25% smaller or larger

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

        private void saveImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string path;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "graph.jpg";
                saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    path = saveFileDialog.FileName;
                }
                // Path wasn't choosen
                else
                    return;
            }

            try
            {
                var bitmap = new Bitmap(graphVisualizationPictureBox.Width, graphVisualizationPictureBox.Height);
                graphVisualizationPictureBox.DrawToBitmap(bitmap, graphVisualizationPictureBox.ClientRectangle);
                bitmap.Save(path, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                ShowMessageBox("Error | " + WCM.SaveImageSomethingWrongTitle, WCM.SaveImageSomethingWrong);

                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Show a messageBox
        /// </summary>
        /// <param name="caption">The caption</param>
        /// <param name="message">The text</param>
        /// <param name="messageBoxButtons">Type of button. Default is OK</param>
        private void ShowMessageBox(string caption, string message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK)
        {
            MessageBox.Show(message, caption, messageBoxButtons);
        }

        private void GraphVisualizationForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ZoomOut();
            }
            else
            {
                ZoomIn();
            }
        }
        
        private void ZoomIn()
        {
            if ((graphVisualizationPictureBox.Width < (MINMAX * graphVisualizationPanel.Width)) &&
                (graphVisualizationPictureBox.Height < (MINMAX * graphVisualizationPanel.Height)))
            {
                graphVisualizationPictureBox.Width = Convert.ToInt32(graphVisualizationPictureBox.Width * ZOOMFACTOR);
                graphVisualizationPictureBox.Height = Convert.ToInt32(graphVisualizationPictureBox.Height * ZOOMFACTOR);
                graphVisualizationPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        
        private void ZoomOut()
        {
            if ((graphVisualizationPictureBox.Width > (graphVisualizationPanel.Width / MINMAX)) &&
                (graphVisualizationPictureBox.Height > (graphVisualizationPanel.Height / MINMAX)))
            {
                graphVisualizationPictureBox.Width = Convert.ToInt32(graphVisualizationPictureBox.Width / ZOOMFACTOR);
                graphVisualizationPictureBox.Height = Convert.ToInt32(graphVisualizationPictureBox.Height / ZOOMFACTOR);
                graphVisualizationPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
