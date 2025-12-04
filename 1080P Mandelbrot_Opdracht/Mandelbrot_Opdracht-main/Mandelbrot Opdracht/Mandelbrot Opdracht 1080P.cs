using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class SmoothForm : Form //SmoothForm werd gebruikt voor double buffer
{
    public SmoothForm(Size window)
    {
        this.Text = "MandelBrot in C#";
        this.ClientSize = window;

        DoubleBuffered = true;
    }

    static class Program
    {
        static void Main() 
        {
        //UI

            // Window en bitmap size
            int screenWidth = 1000;
            int screenHeight = 750;

            int bitmapWidth = 750;
            int bitmapHeight = 750;

            // Meer moderne UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Size window = new Size(screenWidth, screenHeight);
            SmoothForm screen = new SmoothForm(window);

            // Bitmap for the Mandelbrot
            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);


            Label figuur = new Label();
            screen.Controls.Add(figuur);
            figuur.BackColor = Color.Black;
            figuur.Location = new Point(250, 0);
            figuur.Size = new Size(bitmapWidth, bitmapHeight);

            Color backgroundGray = Color.FromArgb(32, 32, 32);

            Font Arial = new Font("Arial", 10, FontStyle.Bold);
            Font Numbers = new Font("Noto Sans SC", 10, FontStyle.Regular);

            Size textSize = new Size(75, 15);
            Size buttonSize = new Size(125, 13);

            int textX = 5;
            int buttonX = textX + 80;


            int itY = 5;
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


            int XY = itY + 40;
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
            xControl.DecimalPlaces = 12;
            xControl.Minimum = -10000;
            xControl.Maximum = 10000;
            xControl.Value = -0.5M;
            xControl.Increment = 0.01M;


            int YY = XY + 40;
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
            yControl.DecimalPlaces = 12;
            yControl.Minimum = -10000;
            yControl.Maximum = 10000;
            yControl.Value = 0;
            yControl.Increment = 0.01M;

            int ZC = YY + 40;
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
            text1.Size = new Size(160, 25);
            int text1Y = ZC + 40;
            text1.Location = new Point(textX, text1Y);
            text1.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown mouseMag = new NumericUpDown();
            screen.Controls.Add(mouseMag);
            mouseMag.Font = Numbers;
            mouseMag.Anchor = AnchorStyles.Left;
            mouseMag.ForeColor = Color.Black;
            mouseMag.Location = new Point(buttonX + 80, text1Y);
            mouseMag.Size = new Size(45, 13);
            mouseMag.Minimum = 0;
            mouseMag.Maximum = 1000;
            mouseMag.Value = 5;

            // beschrijving
            Label kleurBeschrijving = new Label();
            screen.Controls.Add(kleurBeschrijving);
            kleurBeschrijving.Text = "Color Controls (R,G,B)";
            kleurBeschrijving.Font = Arial;
            kleurBeschrijving.ForeColor = Color.White;
            kleurBeschrijving.BackColor = backgroundGray;
            kleurBeschrijving.Size = new Size(250, 15);
            int kleurBY = text1Y + 40;
            kleurBeschrijving.Location = new Point(textX, kleurBY);
            kleurBeschrijving.TextAlign = ContentAlignment.MiddleCenter;


            int CY = kleurBY + 25;
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
            

            // Make the gradient value random at every start
            Random red = new Random();
            Random green = new Random();
            Random blue = new Random();

            NumericUpDown gradientR = new NumericUpDown();
            screen.Controls.Add(gradientR);
            gradientR.Font = Numbers;
            gradientR.Anchor = AnchorStyles.Left;
            gradientR.ForeColor = Color.Black;
            gradientR.Location = new Point(buttonX, CY);
            gradientR.Size = new Size(50, 13);
            gradientR.DecimalPlaces = 1;
            gradientR.Minimum = 0;
            gradientR.Maximum = 255;
            gradientR.Value = red.Next(0,255);
            gradientR.Increment = 0.1M;

            NumericUpDown gradientG = new NumericUpDown();
            screen.Controls.Add(gradientG);
            gradientG.Font = Numbers;
            gradientG.Anchor = AnchorStyles.Left;
            gradientG.ForeColor = Color.Black;
            gradientG.Location = new Point(buttonX + 53, CY);
            gradientG.Size = new Size(50, 13);
            gradientG.DecimalPlaces = 1;
            gradientG.Minimum = 0;
            gradientG.Maximum = 255;
            gradientG.Value = green.Next(0,255);
            gradientG.Increment = 0.1M;

            NumericUpDown gradientB = new NumericUpDown();
            screen.Controls.Add(gradientB);
            gradientB.Font = Numbers;
            gradientB.Anchor = AnchorStyles.Left;
            gradientB.ForeColor = Color.Black;
            gradientB.Location = new Point(buttonX + 2 * 53, CY);
            gradientB.Size = new Size(50, 13);
            gradientB.DecimalPlaces = 1;
            gradientB.Minimum = 0;
            gradientB.Maximum = 255;
            gradientB.Value = blue.Next(0,255);
            gradientB.Increment = 0.1M;

            // Iteration Range control
            Label itRange = new Label();
            screen.Controls.Add(itRange);
            itRange.Text = "Iteration Range";
            itRange.Font = Arial;
            itRange.ForeColor = Color.White;
            itRange.BackColor = backgroundGray;
            itRange.Location = new Point(textX+10, screenHeight - 180);
            itRange.Size = new Size(120,30);

            NumericUpDown itRangeC = new NumericUpDown();
            screen.Controls.Add(itRangeC);
            itRangeC.Font = Numbers;
            itRangeC.Anchor = AnchorStyles.Left;
            itRangeC.ForeColor = Color.Black;
            itRangeC.Location = new Point(textX + 145, screenHeight - 185);
            itRangeC.Size = new Size(80, 13);
            itRangeC.DecimalPlaces = 1;
            itRangeC.Minimum = 0.1M;
            itRangeC.Maximum = decimal.MaxValue;
            itRangeC.Value = 4;
            itRangeC.Increment = 0.1M;


            // Random Gradient button
            Button random = new Button();
            screen.Controls.Add(random);
            random.Text = "Random Gradients";
            random.Font = Arial;
            random.Location = new Point(5, screenHeight - 150);
            random.Size = new Size(240, 25);


            // RGB (ABC) voor mandel getallen die naar oneindigd gaat (Islands)
            Label islandColor = new Label();
            screen.Controls.Add(islandColor);
            islandColor.Text = "Islands";
            islandColor.Font = Arial;
            islandColor.ForeColor = Color.White;
            islandColor.BackColor = backgroundGray;
            islandColor.Size = textSize;
            islandColor.Location = new Point(textX, CY + 35);
            islandColor.TextAlign = ContentAlignment.MiddleCenter;

            NumericUpDown colorA = new NumericUpDown();
            screen.Controls.Add(colorA);
            colorA.Font = Numbers;
            colorA.Anchor = AnchorStyles.Left;
            colorA.ForeColor = Color.Black;
            colorA.Location = new Point(buttonX, CY + 35);
            colorA.Size = new Size(50, 13);
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
            colorB.Location = new Point(buttonX + 53, CY + 35);
            colorB.Size = new Size(50, 13);
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
            colorC.Location = new Point(buttonX + 2 * 53, CY + 35);
            colorC.Size = new Size(50, 13);
            colorC.DecimalPlaces = 1;
            colorC.Minimum = 0;
            colorC.Maximum = 255;
            colorC.Value = 0;
            colorC.Increment = 1;



            int VWY = CY + 80;
            // Selection for interesting views
            Label preview = new Label();
            screen.Controls.Add(preview);
            preview.Text = "Interesting views";
            preview.Font = Arial;
            preview.ForeColor = Color.White;
            preview.BackColor = backgroundGray;
            preview.Size = new Size(250, 15);
            preview.Location = new Point(textX, VWY);
            preview.TextAlign = ContentAlignment.MiddleCenter;

            Point leftAlign = new Point(15, VWY + 25);
            Point rightAlign = new Point(133, VWY + 25);
            Size previewSize = new Size(100, 100);

            // view 1
            Button view1 = new Button();
            screen.Controls.Add(view1);
            view1.Size = previewSize;
            view1.Location = leftAlign;
            view1.FlatStyle = FlatStyle.Flat;
            view1.FlatAppearance.BorderSize = 0;
            Image png1 = Mandelbrot_Opdracht.Properties.Resources.view1;
            view1.Image = png1;

            // view 2
            Button view2 = new Button();
            screen.Controls.Add(view2);
            view2.Size = previewSize;
            view2.Location = rightAlign;
            view2.FlatStyle = FlatStyle.Flat;
            view2.FlatAppearance.BorderSize = 0;
            Image png2 = Mandelbrot_Opdracht.Properties.Resources.view2;
            view2.Image = png2;

            // view 3
            Button view3 = new Button();
            screen.Controls.Add(view3);
            view3.Size = previewSize;
            view3.Location = new Point(leftAlign.X,leftAlign.Y+118);
            view3.FlatStyle = FlatStyle.Flat;
            view3.FlatAppearance.BorderSize = 0;
            Image png3 = Mandelbrot_Opdracht.Properties.Resources.view3;
            view3.Image = png3;

            // view 4
            Button view4 = new Button();
            screen.Controls.Add(view4);
            view4.Size = previewSize;
            view4.Location = new Point(rightAlign.X,rightAlign.Y+118);
            view4.FlatStyle = FlatStyle.Flat;
            view4.FlatAppearance.BorderSize = 0;
            Image png4 = Mandelbrot_Opdracht.Properties.Resources.view4;
            view4.Image = png4;


            // Render button
            int renderY = screenHeight - 75;
            Button render = new Button();
            screen.Controls.Add(render);
            render.Text = "Render";
            render.Font = new Font("Arial Black", 15, FontStyle.Regular);
            render.Size = new Size(240, 60);
            render.Location = new Point(5, renderY);

            // Refresh Button
            Button refresh = new Button();
            screen.Controls.Add(refresh);
            refresh.Text = "Reset";
            refresh.Font = Arial;
            refresh.Size = new Size(115, 30);
            refresh.Location = new Point(130, renderY - 40);


            // Show Center button
            Button showC = new Button();
            screen.Controls.Add(showC);
            showC.Text = "Zoom Center";
            showC.Font = Arial;
            showC.Size = new Size(115, 30);
            showC.Location = new Point(5, renderY - 40);
            bool center = false;

            // UI Background
            Bitmap background = new Bitmap(screenWidth, screenHeight);

            Label achtergrond = new Label();
            screen.Controls.Add(achtergrond);
            achtergrond.BackColor = backgroundGray;
            achtergrond.Size = new Size(screenWidth, screenHeight);

            renderPicture(null, new EventArgs());

            // Functies
            //////////////////////////////////////////////////////////////////////////////////////////////

            //Random gradients
            void randomG(object sender, EventArgs e)
            {
                gradientR.Value = red.Next(1, 255);
                gradientG.Value = green.Next(1, 255);
                gradientB.Value = blue.Next(1, 255);
                renderPicture(null, new EventArgs());
            }

            // Render view1
            void renderView1(object sender, EventArgs e)
            {
                itControl.Value = 1000;

                xControl.Value = (decimal)-1.786032334315;
                yControl.Value = (decimal)0.000311613950;

                zoomControl.Value = 242;

                mouseMag.Value = 5;

                gradientR.Value = 22;
                gradientG.Value = 33;
                gradientB.Value = 44;

                colorA.Value = 0;
                colorB.Value = 0;
                colorC.Value = 0;

                itRangeC.Value = 4;

                renderPicture(null, new EventArgs());
            }

            // Render view2
            void renderView2(object sender, EventArgs e)
            {
                itControl.Value = 1000;

                xControl.Value = (decimal)-1.5028133092;
                yControl.Value = (decimal)-0.0077501074;

                zoomControl.Value = 114;

                mouseMag.Value = 5;

                gradientR.Value = (decimal)29.2;
                gradientG.Value = (decimal)27.3;
                gradientB.Value = (decimal)49.1;

                colorA.Value = 0;
                colorB.Value = 0;
                colorC.Value = 0;

                itRangeC.Value = 4;

                renderPicture(null, new EventArgs());
            }

            // Render view3
            void renderView3(object sender, EventArgs e)
            {
                itControl.Value = 500;

                xControl.Value = (decimal)-1.383543796479;
                yControl.Value = (decimal)-0.018932348872;

                zoomControl.Value = 185;

                mouseMag.Value = 5;

                gradientR.Value = (decimal)3.0;
                gradientG.Value = (decimal)21.3;
                gradientB.Value = (decimal)65.1;

                colorA.Value = 0;
                colorB.Value = 0;
                colorC.Value = 0;

                itRangeC.Value = 4;
                renderPicture(null, new EventArgs());
            }

            // Render view4
            void renderView4(object sender, EventArgs e)
            {
                itControl.Value = 1000;

                xControl.Value = (decimal)0.001643721972;
                yControl.Value = (decimal)0.822467633299;

                zoomControl.Value = 260;

                mouseMag.Value = 5;

                gradientR.Value = (decimal)3.3;
                gradientG.Value = (decimal)15.7;
                gradientB.Value = (decimal)108.7;

                colorA.Value = 0;
                colorB.Value = 0;
                colorC.Value = 0;

                itRangeC.Value = 4;
                renderPicture(null, new EventArgs());
            }


            // reset functie
            void Reset(object sender, EventArgs ea)
            {
                itControl.Value = 100;

                xControl.Value = 0;
                yControl.Value = 0;

                zoomControl.Value = 0;

                mouseMag.Value = 5;

                gradientR.Value = 8;
                gradientG.Value = 8;
                gradientB.Value = 8;

                colorA.Value = 0;
                colorB.Value = 0;
                colorC.Value = 0;

                center = false;
                showC.BackColor = Color.LightGray;

                itRangeC.Value = 4;
                renderPicture(null, new EventArgs());
            }

            // mouse zoom functie
            void mouseZoom(object sender, MouseEventArgs mea)
            {
                int magnification = (int)mouseMag.Value;
                if (mea.Button == MouseButtons.Left)
                {
                    zoomControl.Value += magnification;
                }

                else if (mea.Button == MouseButtons.Right)
                {
                    if (zoomControl.Value > 0 && zoomControl.Value-mouseMag.Value>= 0)
                        zoomControl.Value -= magnification;

                    else if (zoomControl.Value-mouseMag.Value <0)
                    {
                        zoomControl.Value = 0;
                    }
                }

                if (mouseMag.Value < 6)
                {
                    // zoom en view
                    double zoomValue = (double)zoomControl.Value;
                    // 1.1^ exponentiele zoom werd veel gebruikt voor smooth exponential zooming
                    double zoomFactor = Math.Pow(1.1, zoomValue);

                    double rangeMax = 2;
                    double rangeMin = -2;
                    double rangeWidth = rangeMax - rangeMin;

                    // Als zoomFactor groter wordt, wordt de viewWidth kleiner (inzoomen)
                    double viewWidth = rangeWidth / zoomFactor;
                    // uit (bitmapHeight/bitmapWidth) krijgt je de ratio tussen Width en Height
                    double viewHeight = viewWidth * (bitmapHeight / bitmapWidth);

                    // pixelDx|Dy is hoeveel ruimte een pixel vertegenwoordigd in de complex as van de Mandelbrot
                    double pixelDx = viewWidth / bitmapWidth;
                    double pixelDy = viewHeight / bitmapHeight;

                    // (mea.X - bitmapWidth/2) Als deze waarde pos. is dan gaat het naar rechts, negatief naar links
                    // *pixelDx om de waarde te omzetten naar de complex as. + de huidige x verplaatsing
                    // zelfde voor Y
                    double newCenterX = ((mea.X - bitmapWidth / 2) * pixelDx) + (double)xControl.Value;
                    double newCenterY = ((mea.Y - bitmapHeight / 2) * pixelDy) + (double)yControl.Value;

                    // de locatie omzetten naar nieuwe locatie. Decimal omdat NumericUpDown decimal gebruikt
                    xControl.Value = (decimal)newCenterX;
                    yControl.Value = (decimal)newCenterY;

                    render.PerformClick();
                }
                else
                {
                    renderPicture(null, new EventArgs());

                }
            }

            // Show zoom center functie
            void centerOn(object sender, EventArgs ea)
            {
                center = !center;
                using (Graphics gr = Graphics.FromImage(bitmap))

                    if (center)
                    {
                        showC.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        showC.BackColor = Color.LightGray;
                    }
                renderPicture(null, new EventArgs());

            }

            // Render functie
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

                int maxIteration = (int)itControl.Value;

                for (int pixelX = 0; pixelX < bitmapWidth; pixelX++)
                {
                    for (int pixelY = 0; pixelY < bitmapHeight; pixelY++)
                    {
                        // mapping
                        double x = ((pixelX - bitmapWidth / 2) * pixelDx) + centerX;
                        double y = ((pixelY - bitmapHeight / 2) * pixelDy) + centerY;

                        double a = 0;
                        double b = 0;
                        int iteration = 0;

                        // iteratie
                        while (a * a + b * b <= (double)itRangeC.Value && iteration < maxIteration)
                        {

                            double newA = a * a - b * b + x;
                            double newB = 2 * a * b + y;

                            a = newA;
                            b = newB;

                            iteration++;

                        }

                        // Color gradient

                        if (a * a + b * b > 4)
                        {
                            double redG = (double)gradientR.Value;
                            double greenG = (double)gradientG.Value;
                            double blueG = (double)gradientB.Value;

                            double tr = (iteration / redG) % 1;
                            double tg = (iteration / greenG) % 1;
                            double tb = (iteration / blueG) % 1;
                            int R = (int)(255 * tr + 0.5);
                            int G = (int)(255 * tg + 0.5);
                            int B = (int)(255 * tb + 0.5);


                                bitmap.SetPixel(pixelX, pixelY, Color.FromArgb(R, G, B));

                        }
                        else
                        {
                            bitmap.SetPixel(pixelX, pixelY, Color.FromArgb((int)colorA.Value, (int)colorB.Value, (int)colorC.Value));
                        }

                        
                    }
                }
                // Als mouse magnification >5 is gezet dan de zoom center en een tekst wijzen
                using(Graphics gr = Graphics.FromImage(bitmap))
                if (mouseMag.Value > 5)
                {
                    gr.DrawString("The zoom will be at the center for magnification > 5", Arial, Brushes.Red, new Point(10, bitmapHeight - 50));
                    gr.DrawRectangle(new Pen(Color.Red,3), (bitmapWidth / 2)-5, (bitmapHeight / 2)-5, 10, 10);
                }

                // Als center ON, zoom center wijzen
                if (center)
                {

                    using (Graphics gr = Graphics.FromImage(bitmap))
                    gr.DrawRectangle(new Pen(Color.Red,3), (bitmapWidth / 2) - 5, (bitmapHeight / 2) - 5, 10, 10);

                }

                figuur.Image = bitmap;
                figuur.Refresh();


            }

            achtergrond.Image = background;

            figuur.MouseClick += mouseZoom;

            random.Click += randomG;
            render.Click += renderPicture;
            showC.Click += centerOn;
            refresh.Click += Reset;

            view1.Click += renderView1;
            view2.Click += renderView2;
            view3.Click += renderView3;
            view4.Click += renderView4;


            Application.Run(screen);


        }


    }

}




