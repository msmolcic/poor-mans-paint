namespace PoorMansPaint
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    public partial class Main : Form
    {
        private bool _isDrawing;
        private Point _previousLocation;

        public Main()
        {
            InitializeComponent();
            SetupMenu();
            CreateBlankCanvas();
        }

        private void SetupMenu()
        {
            NewFileMenuItem.Click += NewFileMenuItem_Click;
            SaveMenuItem.Click += SaveMenuItem_Click;

            void NewFileMenuItem_Click(object sender, EventArgs e)
                => CreateBlankCanvas();

            void SaveMenuItem_Click(object sender, EventArgs e)
            {
                using var dialog = new SaveFileDialog();
                dialog.Filter = $"Images|{SupportedImageFormat.DialogFilter}";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var imageToSave = new Bitmap(Canvas.Image.Width, Canvas.Image.Height);
                    var graphics = Graphics.FromImage(imageToSave);
                    graphics.Clear(Color.White);
                    graphics.DrawImage(Canvas.Image, 0, 0, Canvas.Image.Width, Canvas.Image.Height);

                    imageToSave.Save(
                        dialog.FileName,
                        SupportedImageFormat.ForFile(dialog.FileName));
                }
            }
        }

        private void CreateBlankCanvas() =>
            Canvas.Image = new Bitmap(Canvas.Width, Canvas.Height);

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _isDrawing = true;
            _previousLocation = e.Location;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
            => _isDrawing = false;

        private void Canvas_MouseLeave(object sender, EventArgs e)
            => _isDrawing = false;

        private void Canvas_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawImage(Canvas.Image, 0, 0);

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing)
            {
                return;
            }

            using var graphics = Graphics.FromImage(Canvas.Image);
            graphics.DrawLine(Pens.Black, _previousLocation, e.Location);
            _previousLocation = e.Location;

            Canvas.Invalidate();
        }
    }
}
