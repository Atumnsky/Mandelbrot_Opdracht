using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class SmoothForm : Form
{
    public SmoothForm(Size window)
    {
        this.Text = "MandelBrot in C#";
        this.ClientSize = window;

        this.AutoScaleMode = AutoScaleMode.Dpi;

        DoubleBuffered = true;
    }

    class Program
    {
        static void Main()
        {
            // Voor de DPI probleem van mijn scherm
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int screenWidth = 2000;
            int screenHeight = 1500;

            Size window = new Size(screenWidth, screenHeight);
            SmoothForm screen = new SmoothForm(window);

            // Bitmap for the Mandelbrot
            int bitmapWidth = 1500;
            int bitmapHeight = 1500;

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);

            // Voor de DPI probleem van mijn scherm
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                bitmap.SetResolution(g.DpiX, g.DpiY);
            }

            Label figuur = new Label();
            screen.Controls.Add(figuur);
            figuur.BackColor = Color.Black;
            figuur.Location = new Point(500, 0);
            figuur.Size = new Size(bitmapWidth, bitmapHeight);

            Color backgroundGray = Color.FromArgb(32, 32, 32);

            Font Arial = new Font("Arial", 10, FontStyle.Bold);
            Font Numbers = new Font("Noto Sans SC", 10, FontStyle.Regular);

            Size textSize = new Size(150, 30);
            Size buttonSize = new Size(250, 25);

            int textX = 10;
            int buttonX = textX + 160;


            int itY = 10;
            // Iteration controls
            Label it = new Label();
            screen.Controls.Add(it);
            it.Text = "Iterations";
            it.Font = Arial;
            it.ForeColor = Color.White;
            it.BackColor = backgroundGray;
            it.Size = textSize;
            it.Location = new Point(textX, itY);
            it.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown itControl = new NumericUpDown();
            screen.Controls.Add(itControl);
            itControl.Font = Numbers;
            itControl.Anchor = AnchorStyles.Left;
            itControl.ForeColor = Color.Black;
            itControl.Location = new Point(buttonX, itY);
            itControl.Size = buttonSize;
            itControl.Value = 100;
            itControl.Maximum = 100000;
            itControl.Minimum = 1;


            int XY = itY + 80;
            // X location control
            Label xLocation = new Label();
            screen.Controls.Add(xLocation);
            xLocation.Text = "Center x";
            xLocation.Font = Arial;
            xLocation.ForeColor = Color.White;
            xLocation.BackColor = backgroundGray;
            xLocation.Size = textSize;
            xLocation.Location = new Point(textX, XY);
            xLocation.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown xControl = new NumericUpDown();
            screen.Controls.Add(xControl);
            xControl.Font = Numbers;
            xControl.Anchor = AnchorStyles.Left;
            xControl.ForeColor = Color.Black;
            xControl.Location = new Point(buttonX, XY);
            xControl.Size = buttonSize;
            xControl.DecimalPlaces = 10;
            xControl.Minimum = -10000;
            xControl.Maximum = 10000;
            xControl.Value = 0;
            xControl.Increment = 0.01M;


            int YY = XY + 80;
            // Y location control
            Label yLocation = new Label();
            screen.Controls.Add(yLocation);
            yLocation.Text = "Center y";
            yLocation.Font = Arial;
            yLocation.ForeColor = Color.White;
            yLocation.BackColor = backgroundGray;
            yLocation.Size = textSize;
            yLocation.Location = new Point(textX, YY);
            yLocation.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown yControl = new NumericUpDown();
            screen.Controls.Add(yControl);
            yControl.Font = Numbers;
            yControl.Anchor = AnchorStyles.Left;
            yControl.ForeColor = Color.Black;
            yControl.Location = new Point(buttonX, YY);
            yControl.Size = buttonSize;
            yControl.DecimalPlaces = 10;
            yControl.Minimum = -10000;
            yControl.Maximum = 10000;
            yControl.Value = 0;
            yControl.Increment = 0.01M;

            int ZC = YY + 80;
            // Zoom control
            Label zoom = new Label();
            screen.Controls.Add(zoom);
            zoom.Text = "Zoom";
            zoom.Font = Arial;
            zoom.ForeColor = Color.White;
            zoom.BackColor = backgroundGray;
            zoom.Size = textSize;
            zoom.Location = new Point(textX, ZC);
            zoom.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown zoomControl = new NumericUpDown();
            screen.Controls.Add(zoomControl);
            zoomControl.Font = Numbers;
            zoomControl.Anchor = AnchorStyles.Left;
            zoomControl.ForeColor = Color.Black;
            zoomControl.Location = new Point(buttonX, ZC);
            zoomControl.Size = buttonSize;
            zoomControl.DecimalPlaces = 0;
            zoomControl.Minimum = 0;
            zoomControl.Maximum = 100000000000000000;
            zoomControl.Value = 0;

            // Mouse zoom magnification control
            Label text1 = new Label();
            screen.Controls.Add(text1);
            text1.Text = "Mouse magnification";
            text1.Font = Arial;
            text1.ForeColor = Color.White;
            text1.BackColor = backgroundGray;
            text1.Size = new Size(300, 50);
            int text1Y = ZC + 80;
            text1.Location = new Point(textX, text1Y);
            text1.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown mouseMag = new NumericUpDown();
            screen.Controls.Add(mouseMag);
            mouseMag.Font = Numbers;
            mouseMag.Anchor = AnchorStyles.Left;
            mouseMag.ForeColor = Color.Black;
            mouseMag.Location = new Point(buttonX + 150, text1Y);
            mouseMag.Size = new Size(100, 25);
            mouseMag.Minimum = 1;
            mouseMag.Maximum = 1000;
            mouseMag.Value = 5;

            // beschrijving
            Label kleurBeschrijving = new Label();
            screen.Controls.Add(kleurBeschrijving);
            kleurBeschrijving.Text = "Color Controls (R,G,B)";
            kleurBeschrijving.Font = Arial;
            kleurBeschrijving.ForeColor = Color.White;
            kleurBeschrijving.BackColor = backgroundGray;
            kleurBeschrijving.Size = new Size(500, 30);
            int kleurBY = text1Y + 80;
            kleurBeschrijving.Location = new Point(textX, kleurBY);
            kleurBeschrijving.TextAlign = ContentAlignment.MiddleCenter;


            int CY = kleurBY + 80;
            // Color control
            Label colorGradient = new Label();
            screen.Controls.Add(colorGradient);
            colorGradient.Text = "Gradients";
            colorGradient.Font = Arial;
            colorGradient.ForeColor = Color.White;
            colorGradient.BackColor = backgroundGray;
            colorGradient.Size = textSize;
            colorGradient.Location = new Point(textX, CY);
            colorGradient.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown gradientR = new NumericUpDown();
            screen.Controls.Add(gradientR);
            gradientR.Font = Numbers;
            gradientR.Anchor = AnchorStyles.Left;
            gradientR.ForeColor = Color.Black;
            gradientR.Location = new Point(buttonX, CY);
            gradientR.Size = new Size(100, 25);
            gradientR.DecimalPlaces = 1;
            gradientR.Minimum = 1;
            gradientR.Maximum = 255;
            gradientR.Value = 22;
            gradientR.Increment = 0.1M;

            NumericUpDown gradientG = new NumericUpDown();
            screen.Controls.Add(gradientG);
            gradientG.Font = Numbers;
            gradientG.Anchor = AnchorStyles.Left;
            gradientG.ForeColor = Color.Black;
            gradientG.Location = new Point(buttonX + 105, CY);
            gradientG.Size = new Size(100, 25);
            gradientG.DecimalPlaces = 1;
            gradientG.Minimum = 1;
            gradientG.Maximum = 255;
            gradientG.Value = 33;
            gradientG.Increment = 0.1M;

            NumericUpDown gradientB = new NumericUpDown();
            screen.Controls.Add(gradientB);
            gradientB.Font = Numbers;
            gradientB.Anchor = AnchorStyles.Left;
            gradientB.ForeColor = Color.Black;
            gradientB.Location = new Point(buttonX + 2 * 105, CY);
            gradientB.Size = new Size(100, 25);
            gradientB.DecimalPlaces = 1;
            gradientB.Minimum = 1;
            gradientB.Maximum = 255;
            gradientB.Value = 44;
            gradientB.Increment = 0.1M;

            // RGB (ABC) voor mandel getallen die naar oneindigd gaat (Islands)
            Label islandColor = new Label();
            screen.Controls.Add(islandColor);
            islandColor.Text = "Islands";
            islandColor.Font = Arial;
            islandColor.ForeColor = Color.White;
            islandColor.BackColor = backgroundGray;
            islandColor.Size = textSize;
            islandColor.Location = new Point(textX, CY + 80);
            islandColor.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown colorA = new NumericUpDown();
            screen.Controls.Add(colorA);
            colorA.Font = Numbers;
            colorA.Anchor = AnchorStyles.Left;
            colorA.ForeColor = Color.Black;
            colorA.Location = new Point(buttonX, CY + 80);
            colorA.Size = new Size(100, 25);
            colorA.DecimalPlaces = 1;
            colorA.Minimum = 0;
            colorA.Maximum = 255;
            colorA.Value = 0;
            colorA.Increment = 1;

            NumericUpDown colorB = new NumericUpDown();
            screen.Controls.Add(colorB);
            colorB.Font = Numbers;
            colorB.Anchor = AnchorStyles.Left;
            colorB.ForeColor = Color.Black;
            colorB.Location = new Point(buttonX + 105, CY + 80);
            colorB.Size = new Size(100, 25);
            colorB.DecimalPlaces = 1;
            colorB.Minimum = 0;
            colorB.Maximum = 255;
            colorB.Value = 0;
            colorB.Increment = 1;

            NumericUpDown colorC = new NumericUpDown();
            screen.Controls.Add(colorC);
            colorC.Font = Numbers;
            colorC.Anchor = AnchorStyles.Left;
            colorC.ForeColor = Color.Black;
            colorC.Location = new Point(buttonX + 2 * 105, CY + 80);
            colorC.Size = new Size(100, 25);
            colorC.DecimalPlaces = 1;
            colorC.Minimum = 0;
            colorC.Maximum = 255;
            colorC.Value = 0;
            colorC.Increment = 1;



            int LZY = CY + 190;
            // Selection for interesting views



            // Render button
            Button render = new Button();
            screen.Controls.Add(render);
            render.Text = "Render";
            render.Font = new Font("Arial Black", 15, FontStyle.Regular);
            render.Size = new Size(480, 120);
            render.Location = new Point(10, screenHeight - 150);


            // UI Background
            Bitmap background = new Bitmap(screenWidth, screenHeight);

            // Voor de DPI probleem van mijn scherm
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                background.SetResolution(g.DpiX, g.DpiY);
            }

            Label achtergrond = new Label();
            screen.Controls.Add(achtergrond);
            achtergrond.BackColor = backgroundGray;
            achtergrond.Size = new Size(screenWidth, screenHeight);

            renderPicture(null, new EventArgs());



            void mouseZoom(object sender, MouseEventArgs mea)
            {
                int magnification = (int)mouseMag.Value;
                if (mea.Button == MouseButtons.Left)
                {
                    zoomControl.Value += magnification;
                }

                else if (mea.Button == MouseButtons.Right)
                {
                    if (zoomControl.Value > 0)
                        zoomControl.Value -= magnification;
                }


                // zoom en view
                double zoomValue = (double)zoomControl.Value;
                double zoomFactor = Math.Pow(1.1, zoomValue);

                double rangeMax = 2;
                double rangeMin = -2;
                double rangeWidth = rangeMax - rangeMin;

                double viewWidth = rangeWidth / zoomFactor;
                double viewHeight = viewWidth * (bitmapHeight / bitmapWidth);

                double pixelDx = viewWidth / bitmapWidth;
                double pixelDy = viewHeight / bitmapHeight;

                double newCenterX = ((mea.X - bitmapWidth / 2) * pixelDx) + (double)xControl.Value;
                double newCenterY = ((mea.Y - bitmapHeight / 2) * pixelDy) + (double)yControl.Value;

                xControl.Value = (decimal)newCenterX;
                yControl.Value = (decimal)newCenterY;

                render.PerformClick();

            }

            void renderPicture(object sender, EventArgs ea)
            {
                double zoomValue = (double)zoomControl.Value;
                double zoomFactor = Math.Pow(1.1, zoomValue);

                double rangeMax = 2;
                double rangeMin = -2;
                double rangeWidth = rangeMax - rangeMin;

                double viewWidth = rangeWidth / zoomFactor;
                double viewHeight = viewWidth * (bitmapHeight / bitmapWidth);

                double pixelDx = viewWidth / bitmapWidth;
                double pixelDy = viewHeight / bitmapHeight;

                double centerX = (double)xControl.Value;
                double centerY = (double)yControl.Value;


                // Iteratie waarde nemen van de control
                int maxIteration = (int)itControl.Value;

                for (int pixelX = 0; pixelX < bitmapWidth; pixelX++)
                {
                    for (int pixelY = 0; pixelY < bitmapHeight; pixelY++)
                    {
                        // mapping functie
                        double x = ((pixelX - bitmapWidth / 2) * pixelDx) + centerX;
                        double y = ((pixelY - bitmapHeight / 2) * pixelDy) + centerY;

                        double a = 0;
                        double b = 0;
                        int iteration = 0;

                        // iteratie functie
                        while (a * a + b * b <= 4 && iteration < maxIteration)
                        {

                            double newA = a * a - b * b + x;
                            double newB = 2 * a * b + y;

                            a = newA;
                            b = newB;

                            iteration++;

                        }

                        // Color for the pixels

                        if (a * a + b * b > 4)
                        {
                            double redG = (double)gradientR.Value;
                            double greenG = (double)gradientG.Value;
                            double blueG = (double)gradientB.Value;

                            double tr = (iteration / redG) % 1;
                            double tg = (iteration / greenG) % 1;
                            double tb = (iteration / blueG) % 1;
                            int R = (int)(255 * tr + 0 * (1 - tr) + 0.5);
                            int G = (int)(255 * tg + 0 * (1 - tg) + 0.5);
                            int B = (int)(255 * tb + 0 * (1 - tb) + 0.5);
                            bitmap.SetPixel(pixelX, pixelY, Color.FromArgb(R, G, B));
                        }
                        else
                        {
                            bitmap.SetPixel(pixelX, pixelY, Color.FromArgb((int)colorA.Value, (int)colorB.Value, (int)colorC.Value));
                        }
                    }
                }
                figuur.Image = bitmap;
                figuur.Refresh();


            }


            figuur.MouseClick += mouseZoom;
            achtergrond.Image = background;
            render.Click += renderPicture;
            render.PerformClick();
            Application.Run(screen);


        }


    }

}




