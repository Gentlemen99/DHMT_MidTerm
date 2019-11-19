using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;





namespace WindowsFormsApplication2
{

    public partial class Form1 : Form
    {
        Color colorUser;
        int want_to_draw;
        OpenGL gl;
        Point pStart, pEnd;

        Object mylines, mycircles, myrectangles, myellipses, myEqua_triangles, myRegular_Five_angles, myRegular_Six_angles;
        public Form1()
        {
            InitializeComponent();
            gl = openGLControl.OpenGL;
            want_to_draw = -1;
            colorUser = Color.White;
            mylines = new MyLine();
            mycircles = new MyCircle();
            myrectangles = new MyRectangle();
            //myellipses = new Ellipse();
            myEqua_triangles = new MyEqua_Triangle();
            myRegular_Five_angles = new MyRegular_Five_Angle();
            myRegular_Six_angles = new MyRegular_Six_Angle();
        }
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ctrl_OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            pStart = e.Location;
            pEnd = pStart;
        }



        private void ctrl_openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            pEnd = e.Location;
            if (want_to_draw == 0) // ve duong thang
            {
                mylines.append(pStart);
                mylines.append(pEnd);
            }
            else if (want_to_draw == 1) // ve duong tron
            {
                mycircles.append(pStart);
                mycircles.append(pEnd);
            }
            else if (want_to_draw == 2) // ve hinh chu nhat
            {
                myrectangles.append(pStart);
                myrectangles.append(pEnd);
            }
            else if (want_to_draw == 3)// ve ellipse
            {

            }
            else if (want_to_draw == 4) // ve tam giac deu
            {
                myEqua_triangles.append(pStart);
                myEqua_triangles.append(pEnd);
            }
            else if (want_to_draw == 5) // ve ngu giac deu
            {
                myRegular_Five_angles.append(pStart);
                myRegular_Five_angles.append(pEnd);
            }
            else if (want_to_draw == 6) // ve luc giac deu
            {

            }
        }



        private void bt_Exit_Click(object sender, EventArgs e)
        {
            want_to_draw = -1;
        }

        private void bt_ColorTable_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorUser = colorDialog1.Color;
            }
        }

        private void bt_Circle_Click(object sender, EventArgs e)
        {
            want_to_draw = 1;
        }

        private void bt_Rectangle_Click(object sender, EventArgs e)
        {
            want_to_draw = 2;
        }
        private void bt_Line_Click(object sender, EventArgs e)
        {
            want_to_draw = 0;
        }
        private void bt_Equilateral_Triangle_Click(object sender, EventArgs e)
        {
            want_to_draw = 4;
        }
        private void bt_Regular_Five_Polygon_Click(object sender, EventArgs e)
        {
            want_to_draw = 5;
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            if (want_to_draw != -1)
            {
                // Clear the color and depth buffer.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                // select color
                gl.Color(Color.White.R / 255.0, Color.White.G / 255.0, Color.White.B / 255.0, 0);

                mylines.DrawObject(gl);
                mycircles.DrawObject(gl);
                myrectangles.DrawObject(gl);
                myEqua_triangles.DrawObject(gl);
                myRegular_Five_angles.DrawObject(gl);
            }
        }


    }
    public abstract class Object
    {

        public abstract void append(Point mypoint);

        public abstract void DrawObject(OpenGL gl);
    }

    public class MyLine : Object
    {

        public List<Point> line_pointlist = new List<Point>();

        public override void append(Point mypoint)
        {
            line_pointlist.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
            if (line_pointlist != null)
            {


                int n = line_pointlist.Count();
                for (int i = 0; i < n; i += 2)
                {
                    gl.Begin(OpenGL.GL_LINES);
                    gl.Vertex(line_pointlist[i].X, gl.RenderContextProvider.Height - line_pointlist[i].Y);
                    gl.Vertex(line_pointlist[i + 1].X, gl.RenderContextProvider.Height - line_pointlist[i + 1].Y);
                    gl.End();
                    gl.Flush();
                }
            }
        }
    }
    public class MyCircle : Object
    {
        public List<Point> circle_pointlist = new List<Point>();

        public override void append(Point mypoint)
        {
            circle_pointlist.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
            double dRadius;
            if (circle_pointlist != null)
            {
                int n = circle_pointlist.Count();

                for (int i = 0; i < n; i += 2)
                {
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    dRadius = dRadius = Math.Sqrt((circle_pointlist[i].X - circle_pointlist[i + 1].X) * (circle_pointlist[i].X - circle_pointlist[i + 1].X) + (circle_pointlist[i].Y - circle_pointlist[i + 1].Y) * (circle_pointlist[i].Y - circle_pointlist[i + 1].Y));
                    double x, y, theta;
                    for (int j = 0; j < 50; j++)
                    {
                        theta = 2.0f * 3.1415926f * (double)j / (double)50;//get the current angle

                        x = dRadius * Math.Cos(theta);//calculate the x component
                        y = dRadius * Math.Sin(theta);//calculate the y component

                        gl.Vertex(x + circle_pointlist[i].X, gl.RenderContextProvider.Height - (y +  circle_pointlist[i].Y));//output vertex
                    }
                    gl.End();
                    gl.Flush();
                }
            }
        }
    }

    public class MyRectangle : Object
    {
        public List<Point> retangle_pointlist = new List<Point
            >();

        public override void append(Point mypoint)
        {
            retangle_pointlist.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
            if (retangle_pointlist != null)
            {
                // Clear the color and depth buffer.
                //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                // select color
               // gl.Color(Color.White.R / 255.0, Color.White.G / 255.0, Color.White.B / 255.0, 0);


                int n = retangle_pointlist.Count();
                for (int i = 0; i < n - 1; i += 2)
                {
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    gl.Vertex(retangle_pointlist[i].X, gl.RenderContextProvider.Height - retangle_pointlist[i].Y);
                    gl.Vertex(retangle_pointlist[i + 1].X, gl.RenderContextProvider.Height - retangle_pointlist[i].Y);
                    gl.Vertex(retangle_pointlist[i + 1].X, gl.RenderContextProvider.Height - retangle_pointlist[i + 1].Y);
                    gl.Vertex(retangle_pointlist[i].X, gl.RenderContextProvider.Height - retangle_pointlist[i + 1].Y);
                    gl.End();
                    gl.Flush();
                }
            }
        }
    }


    //tam giac deu
    public class MyEqua_Triangle : Object 
    {
        public List<Point> E_TriAngle_pointlist = new List<Point>();

        public override void append(Point mypoint)
        {
            E_TriAngle_pointlist.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
            if (E_TriAngle_pointlist != null)
            {
                int n = E_TriAngle_pointlist.Count();
                double mid_X, mid_Y, delta_X, delta_Y, x, y;
                for (int i = 0; i < n; i += 2)
                {
                    gl.Begin(OpenGL.GL_LINE_LOOP);

                    // tìm kiếm trung điểm cạnh đã có
                    mid_X = (E_TriAngle_pointlist[i].X + E_TriAngle_pointlist[i + 1].X) / (double)2;
                    mid_Y = (E_TriAngle_pointlist[i].Y + E_TriAngle_pointlist[i + 1].Y) / (double)2;

                    // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                    delta_X = E_TriAngle_pointlist[i].X - mid_X;
                    delta_Y = E_TriAngle_pointlist[i].Y - mid_Y;

                    // tính hệ số góc của đường thẳng vuông góc với cạnh đã có
                    if (delta_X != 0)
                    {
                        double tmp = -delta_X;
                        delta_X = delta_Y;
                        delta_Y = tmp;

                    }
                    else
                    {
                        double tmp = -delta_Y;
                        delta_Y = delta_X;
                        delta_X = tmp;
                    }
                    //tính điểm còn lại theo công thức x = mid_X + căn(3)*delta_X, y = mid_y + căn(3)*delta_Y
                    x = mid_X + Math.Sqrt(3) * delta_X;
                    y = mid_Y + Math.Sqrt(3) * delta_Y;

                    gl.Vertex(E_TriAngle_pointlist[i].X, gl.RenderContextProvider.Height - E_TriAngle_pointlist[i].Y);//output verte
                    gl.Vertex(E_TriAngle_pointlist[i + 1].X, gl.RenderContextProvider.Height - E_TriAngle_pointlist[i + 1].Y);//output verte
                    gl.Vertex(x, gl.RenderContextProvider.Height  - y);//output verte
                    gl.End();
                    gl.Flush();
                }
            }
        }
    }
    public class MyRegular_Five_Angle : Object
    {
        public List<Point> Regular_Polygon_pointlist = new List<Point>();

        public override void append(Point mypoint)
        {
            Regular_Polygon_pointlist.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
            if (Regular_Polygon_pointlist != null)
            {
                int n = Regular_Polygon_pointlist.Count();

                double mid_X, mid_Y, delta_X, delta_Y;
                double mid_coorinate_x, mid_coorinate_y;
                double left_coorinate_x, right_coorinate_x, left_coorinate_y, right_coorinate_y;

                double length, mid_length;

                for (int i = 0; i < n; i += 2)
                {
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    
                    length = Math.Sqrt((Regular_Polygon_pointlist[i].X - Regular_Polygon_pointlist[i + 1].X) * 
                        (Regular_Polygon_pointlist[i].X - Regular_Polygon_pointlist[i + 1].X) + 
                        (Regular_Polygon_pointlist[i].Y - Regular_Polygon_pointlist[i + 1].Y) * 
                        (Regular_Polygon_pointlist[i].Y - Regular_Polygon_pointlist[i + 1].Y));
                    /////////////////// giai đoạn 1
                    // tìm kiếm trung điểm cạnh đã có
                    mid_X = (Regular_Polygon_pointlist[i].X + Regular_Polygon_pointlist[i + 1].X) / (double)2;
                    mid_Y = (Regular_Polygon_pointlist[i].Y + Regular_Polygon_pointlist[i + 1].Y) / (double)2;

                    // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                    delta_X = Regular_Polygon_pointlist[i].X - mid_X;
                    delta_Y = Regular_Polygon_pointlist[i].Y - mid_Y;

                    // tính hệ số góc của đường thẳng vuông góc với cạnh đã có
                    if (delta_X != 0)
                    {
                        double tmp = -delta_X;
                        delta_X = delta_Y;
                        delta_Y = tmp;

                    }
                    else
                    {
                        double tmp = -delta_Y;
                        delta_Y = delta_X;
                        delta_X = tmp;
                    }
                    //tính điểm còn lại theo công thức x = mid_X + "Ratio"*delta_X, y = mid_y + "Ratio"*delta_Y
                    double ratio = Math.Sqrt(2*(1 - Math.Cos(3.1415926f * (double)3 / (double)5)) - (double)1/(double)4) / ((double)1/(double)2);
                    mid_coorinate_x = mid_X + ratio * delta_X;
                    mid_coorinate_y = mid_Y + ratio * delta_Y;

                    /////////////////// giai đoạn 2
                    // tìm kiếm trung điểm cạnh đã có
                    mid_X = (Regular_Polygon_pointlist[i].X + mid_coorinate_x) / (double)2;
                    mid_Y = (Regular_Polygon_pointlist[i].Y + mid_coorinate_y) / (double)2;

                    // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                    delta_X = mid_coorinate_x - mid_X;
                    delta_Y = mid_coorinate_y - mid_Y;

                    mid_length = Math.Sqrt(delta_X * delta_X + delta_Y * delta_Y);

                    // tính hệ số góc của đường thẳng vuông góc với cạnh đã có
                    if (delta_X != 0)
                    {
                        double tmp = -delta_X;
                        delta_X = delta_Y;
                        delta_Y = tmp;

                    }
                    else
                    {
                        double tmp = -delta_Y;
                        delta_Y = delta_X;
                        delta_X = tmp;
                    }
                    //tính điểm còn lại theo công thức x = mid_X + "Ratio"*delta_X, y = mid_y + "Ratio"*delta_Y
                    ratio = length * length * Math.Sin(3.1415926f * (double)3 / (double)5) / ((double)2* mid_length*mid_length);
                    left_coorinate_x = mid_X - ratio * delta_X;
                    left_coorinate_y = mid_Y - ratio * delta_Y;

                    /////////////////// giai đoạn 2=3
                    // tìm kiếm trung điểm cạnh đã có
                    mid_X = (Regular_Polygon_pointlist[i + 1].X + mid_coorinate_x) / (double)2;
                    mid_Y = (Regular_Polygon_pointlist[i + 1].Y + mid_coorinate_y) / (double)2;

                    // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                    delta_X = mid_coorinate_x - mid_X;
                    delta_Y = mid_coorinate_y - mid_Y;

                    mid_length = Math.Sqrt(delta_X * delta_X + delta_Y * delta_Y);

                    // tính hệ số góc của đường thẳng vuông góc với cạnh đã có
                    if (delta_X != 0)
                    {
                        double tmp = -delta_X;
                        delta_X = delta_Y;
                        delta_Y = tmp;

                    }
                    else
                    {
                        double tmp = -delta_Y;
                        delta_Y = delta_X;
                        delta_X = tmp;
                    }
                    //tính điểm còn lại theo công thức x = mid_X + "Ratio"*delta_X, y = mid_y + "Ratio"*delta_Y
                    ratio = length*length*Math.Sin(3.1415926f * (double)3 / (double)5) / ((double)2 * mid_length * mid_length);
                    right_coorinate_x = mid_X + ratio * delta_X;
                    right_coorinate_y = mid_Y + ratio * delta_Y;

                    gl.Vertex(Regular_Polygon_pointlist[i].X, gl.RenderContextProvider.Height - Regular_Polygon_pointlist[i].Y);//output vertex
                    gl.Vertex(Regular_Polygon_pointlist[i + 1].X, gl.RenderContextProvider.Height - Regular_Polygon_pointlist[i + 1].Y);//output vertex
                    gl.Vertex(right_coorinate_x, gl.RenderContextProvider.Height - right_coorinate_y);//output vertex
                    gl.Vertex(mid_coorinate_x, gl.RenderContextProvider.Height - mid_coorinate_y);//output verte
                    gl.Vertex(left_coorinate_x, gl.RenderContextProvider.Height - left_coorinate_y);//output verte
                    gl.End();
                    gl.Flush();
                }
            }
        }
    }
    public class MyRegular_Six_Angle : Object
    {
        public List<Point> pointList = new List<Point>();

        public override void append(Point mypoint)
        {
            pointList.Add(mypoint);
        }
        public override void DrawObject(OpenGL gl)
        {
         
        }
    }
}
