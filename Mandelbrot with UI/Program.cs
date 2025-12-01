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
            xControl.Minimum = 0;
            xControl.Maximum = 10000;
            xControl.Value = 0;


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
            yControl.Minimum = 0;
            yControl.Maximum = 10000;
            yControl.Value = 0;


            int CY = YY + 80;
            // Color control



            int LZY = CY + 80;
            // Location+Zoom selection for interesting views



            // Background of the window
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



            // Values that changes the zoom
            double rangeMax = 2;
            double rangeMin = -2;
            double rangeWidth = rangeMax - rangeMin;

            // Values that chenges the center (x,y)
            double centerX = 0;
            double centerY = 0;

            // Get Iteration value from the box
            int maxIteration = (int)itControl.Value;

            // Iteration function
            for (int pixelX = 0; pixelX < bitmapWidth; pixelX++)
            {
                for (int pixelY = 0; pixelY < bitmapHeight; pixelY++)
                {
                    // mapping functie
                    double x = ((pixelX / (double)screenWidth * rangeWidth + rangeMin)) + centerX;
                    double y = ((pixelY / (double)screenHeight * rangeWidth + rangeMin)) + centerY;

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

                        //Debug.Print($"a={a}|b={b}");
                    }

                    // Color for the pixels
                    Color finalColor;

                    if (iteration % 2 == 0)
                    {
                        finalColor = Color.White;
                    }

                    else
                    {

                        int R = (iteration == maxIteration) ? 0 : iteration % 255;
                        int G = (iteration == maxIteration) ? 0 : iteration % 255;
                        int B = (iteration == maxIteration) ? 0 : iteration % 255;

                        finalColor = Color.FromArgb(R, G, B);
                    }



                    bitmap.SetPixel(pixelX, pixelY, finalColor);
                }
            }

            figuur.Image = bitmap;
            achtergrond.Image = background;

            Application.Run(screen);
        }

    }


}
