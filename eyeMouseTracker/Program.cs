using System.Windows.Forms;
using System.Drawing;
using System;

class HalloForm : Form
{
    // Set global variables
    private int positionX;
    private int positionY;
    private Label coords;

    public HalloForm()
    {
        this.Text = "Hallo";
        this.BackColor = Color.Pink;
        this.Size = new Size(500, 500);
        this.Paint += this.tekenScherm;
        InitializeComponent();
    }

    void tekenScherm(object obj, PaintEventArgs pea)
    {
        drawLabel();
        drawEye(obj, pea, 50);
        drawEye(obj, pea, 120);
        drawMouth(obj, pea);
    }

    void drawLabel()
    {
        // If the label is empty create a label
        if (coords == null)
            coords = new Label();
        coords.Location = new Point(200, 0);
        coords.Font = new Font("Calibri", 10);
        coords.ForeColor = Color.Black;
        coords.Padding = new Padding(0);
        this.Controls.Add(coords);

        // Always set the label to the x and y position
        coords.Text = "X: " + positionX + " Y: " + positionY;
    }

    void drawEye(object obj, PaintEventArgs pea, int eyePosX)
    {
        int eyePosY = 50;
        int eyeSize = 50;
        int innerEyeSize = 20;
        
        // Draw outher eye
        pea.Graphics.FillEllipse(Brushes.Black, eyePosX, eyePosY, eyeSize, eyeSize);

        // Get the difference between center of the eye and the cursor
        // Get the power of the difference
        int yDifCenterEye = (eyePosY + (eyeSize/2)) - positionY;
        int yDifCenterEyeSquere = yDifCenterEye * yDifCenterEye;

        int xDifCenterEye = (eyePosX + (eyeSize/2)) - positionX;
        int xDifCenterEyeSquere = xDifCenterEye * xDifCenterEye;

        // A^2 + B^2 = C^. Se we calculate the exact diference between the center of the eye and the cursor
        int differenceCenterEyeSquere = yDifCenterEyeSquere + xDifCenterEyeSquere;

        // We want the inner eye to stay inside of the outher eye.
        // So we calculate the maximum before te inner eye leaves the outher eye
        int maxDifferenceCenter = ((eyeSize / 2) - innerEyeSize) + (innerEyeSize / 2);

        // Im most definetly doing this wrong
        // But my idioligy consists of deviding all sizes by the max differce
        // Take te power to do the same for a and b
        // This is wrong and it returns an error but its close enaugh and youtube didnt help me any further
        int multipleFactor = differenceCenterEyeSquere / maxDifferenceCenter;
        int xForInnerEye = xDifCenterEyeSquere / multipleFactor;
        int yForInnerEye = yDifCenterEyeSquere / multipleFactor;

        // If you get the power of a number its always positive
        // If the number was a negative number turn it positive
        // Im doing this the other way around because idk thats just how it is
        if (xDifCenterEye > 0)
            xForInnerEye = xForInnerEye * -1;
        
        if (yDifCenterEye > 0)
            yForInnerEye = yForInnerEye * -1;


        //Draw inner eye
        pea.Graphics.FillEllipse(Brushes.White, eyePosX + 15 + xForInnerEye, eyePosY + 15 + yForInnerEye, innerEyeSize, innerEyeSize);
    }

    void drawMouth(object obj, PaintEventArgs pea)
    {
        int happyOrSad = 180;
        int defaultSmile = 40;
        int smile = defaultSmile - (positionY/5);
        if (smile <= 0)
        {
            smile = 1 + ((positionY / 2) - 100);
            happyOrSad = -180;
        }
        Console.WriteLine(smile);

        // Now draw the mouth
        Pen thePen = new Pen(Color.FromArgb(255, 0, 0, 0), 10);
        pea.Graphics.DrawArc(thePen, 50, 150, 120, smile, 0, happyOrSad);
        //pea.Graphics.DrawArc()
    }

    private void InitializeComponent()
    {
        // 
        // HalloForm (was automatically added)
        // 
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Name = "HalloForm";
        this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HalloForm_MouseMove);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private void HalloForm_MouseMove(object sender, MouseEventArgs e)
    {
        // Change global variables
        positionX = e.X;
        positionY = e.Y;

        // Not sure what this does
        // But it makes sure te program runs again
        // That way the smiley keeps updating on mouse movement
        Invalidate();
    }
}

class HalloWin3
{
    static void Main()
    {
        HalloForm scherm;
        scherm = new HalloForm();
        Application.Run(scherm);
    }
}