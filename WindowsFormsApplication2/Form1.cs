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
using Timer = System.Timers.Timer;
using System.Timers;





namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Color colorUser, colorUser1;
        float sizeUser;

        OpenGL gl;
        Point pStart, pEnd;

        static List<Point> pointList = new List<Point>();
        static List<int> typeList = new List<int>();
        
        static int want_to_draw;
        int state_down_mouse;

        Object mylines, mycircles, myrectangles, myellipses, myEqua_triangles, myRegular_Five_angles, myRegular_Six_angles;
        public Form1()
        {
            InitializeComponent();
            gl = openGLControl.OpenGL;


            want_to_draw = -1;

            sizeUser = 2.0f;
            colorUser = Color.White;
            colorUser1 = Color.White;

            //các mảng lưu các hình vẽ
            mylines = new MyLine();
            mycircles = new MyCircle();
            myrectangles = new MyRectangle();
            //myellipses = new Ellipse();
            myEqua_triangles = new MyEqua_Triangle();
            myRegular_Five_angles = new MyRegular_Five_Angle();
            myRegular_Six_angles = new MyRegular_Six_Angle();

            //trạng thái vẽ hình
            state_down_mouse = 0;
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
            if (want_to_draw != -1)
            {
                pStart = e.Location;
                pEnd = pStart;
                state_down_mouse = 1;
            }
        }



        private void ctrl_openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            pEnd = e.Location;
            if (want_to_draw != -1)
            {
                if (want_to_draw == 0) // ve duong thang
                {
                    mylines.append(pStart, pEnd);
                }
                else if (want_to_draw == 1) // ve duong tron
                {
                    mycircles.append(pStart, pEnd);
                }
                else if (want_to_draw == 2) // ve hinh chu nhat
                {
                    myrectangles.append(pStart, pEnd);
                }
                else if (want_to_draw == 3)// ve ellipse
                {

                }
                else if (want_to_draw == 4) // ve tam giac deu
                {
                    myEqua_triangles.append(pStart, pEnd);
                }
                else if (want_to_draw == 5) // ve ngu giac deu
                {
                    myRegular_Five_angles.append(pStart, pEnd);
                }
                else if (want_to_draw == 6) // ve luc giac deu
                {
                    myRegular_Six_angles.append(pStart, pEnd);
                }
                state_down_mouse = 0;
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

        private void ctrl_openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (want_to_draw != -1 && state_down_mouse == 1) // trang thai ve
            {
                pEnd = e.Location;
                if (want_to_draw == 0) // ve duong thang
                {
                    mylines.append(pStart, pEnd);
                }
                else if (want_to_draw == 1) // ve duong tron
                {
                    mycircles.append(pStart, pEnd);
                }
                else if (want_to_draw == 2) // ve hinh chu nhat
                {
                    myrectangles.append(pStart, pEnd);
                }
                else if (want_to_draw == 3)// ve ellipse
                {

                }
                else if (want_to_draw == 4) // ve tam giac deu
                {
                    myEqua_triangles.append(pStart, pEnd);
                }
                else if (want_to_draw == 5) // ve ngu giac deu
                {
                    myRegular_Five_angles.append(pStart, pEnd);
                }
                else if (want_to_draw == 6) // ve luc giac deu
                {
                    myRegular_Six_angles.append(pStart, pEnd);
                }
            }
            else // trang thai khac
            {

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

        private void bt_Regular_Six_Polygon_Click(object sender, EventArgs e)
        {
            want_to_draw = 6;
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            if (want_to_draw != -1)
            {
                // Clear the color and depth buffer.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                mylines.DrawObject(gl, colorUser, sizeUser);
                mycircles.DrawObject(gl, colorUser, sizeUser);
                myrectangles.DrawObject(gl, colorUser, sizeUser);
                myEqua_triangles.DrawObject(gl, colorUser, sizeUser);
                myRegular_Five_angles.DrawObject(gl, colorUser, sizeUser);
                myRegular_Six_angles.DrawObject(gl, colorUser, sizeUser);
            }
        }


        public abstract class Object
        {

            public void append(Point first_point, Point second_point)
            {
                int n = pointList.Count();
                for (int i = 0; i < n; i++)
                {
                    if (pointList[i] == first_point && typeList[i/2] == want_to_draw)
                    {
                        pointList[i + 1] = second_point;
                        return;
                    }
                }
                pointList.Add(first_point);
                pointList.Add(second_point);
                typeList.Add(want_to_draw);
            }

            public abstract void DrawObject(OpenGL gl, Color colorUser, float sizeUser);
        }

        public class MyLine : Object
        {
            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                if (pointList != null && typeList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);

                    int n = pointList.Count();
                    for (int i = 0; i < n; i += 2)
                    {
                        if (typeList[i / 2] == 0)
                        {
                            gl.Begin(OpenGL.GL_LINES);
                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i].Y);
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }
        public class MyCircle : Object
        {
            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                double dRadius;
                if (pointList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);

                    int n = pointList.Count();

                    for (int i = 0; i < n; i += 2)
                    {
                        if (typeList[i / 2] == 1)
                        {
                            gl.Begin(OpenGL.GL_LINE_LOOP);
                            dRadius = dRadius = Math.Sqrt((pointList[i].X - pointList[i + 1].X) * (pointList[i].X - pointList[i + 1].X) + (pointList[i].Y - pointList[i + 1].Y) * (pointList[i].Y - pointList[i + 1].Y));
                            double x, y, theta;
                            for (int j = 0; j < 50; j++)
                            {
                                theta = 2.0f * 3.1415926f * (double)j / (double)50;//get the current angle

                                x = dRadius * Math.Cos(theta);//calculate the x component
                                y = dRadius * Math.Sin(theta);//calculate the y component

                                gl.Vertex(x + pointList[i].X, gl.RenderContextProvider.Height - (y + pointList[i].Y));//output vertex
                            }
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }

        public class MyRectangle : Object
        {
            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                if (pointList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);

                    int n = pointList.Count();
                    for (int i = 0; i < n - 1; i += 2)
                    {
                        if (typeList[i / 2] == 2)
                        {
                            gl.Begin(OpenGL.GL_LINE_LOOP);
                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i].Y);
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i].Y);
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);
                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }


        //tam giac deu
        public class MyEqua_Triangle : Object
        {
            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                if (pointList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);

                    int n = pointList.Count();
                    double mid_X, mid_Y, delta_X, delta_Y, x, y;
                    for (int i = 0; i < n; i += 2)
                    {
                        if (typeList[i / 2] == 4)
                        {
                            gl.Begin(OpenGL.GL_LINE_LOOP);

                            // tìm kiếm trung điểm cạnh đã có
                            mid_X = (pointList[i].X + pointList[i + 1].X) / (double)2;
                            mid_Y = (pointList[i].Y + pointList[i + 1].Y) / (double)2;

                            // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                            delta_X = pointList[i].X - mid_X;
                            delta_Y = pointList[i].Y - mid_Y;

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

                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i].Y);//output verte
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);//output verte
                            gl.Vertex(x, gl.RenderContextProvider.Height - y);//output verte
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }
        public class MyRegular_Five_Angle : Object
        {

            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                if (pointList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);
                    int n = pointList.Count();

                    double mid_X, mid_Y, delta_X, delta_Y;
                    double mid_coorinate_x, mid_coorinate_y;
                    double left_coorinate_x, right_coorinate_x, left_coorinate_y, right_coorinate_y;

                    double length, mid_length;

                    for (int i = 0; i < n; i += 2)
                    {
                        if (typeList[i / 2] == 5)
                        {
                            gl.Begin(OpenGL.GL_LINE_LOOP);

                            length = Math.Sqrt((pointList[i].X - pointList[i + 1].X) *
                                (pointList[i].X - pointList[i + 1].X) +
                                (pointList[i].Y - pointList[i + 1].Y) *
                                (pointList[i].Y - pointList[i + 1].Y));
                            /////////////////// giai đoạn 1
                            // tìm kiếm trung điểm cạnh đã có
                            mid_X = (pointList[i].X + pointList[i + 1].X) / (double)2;
                            mid_Y = (pointList[i].Y + pointList[i + 1].Y) / (double)2;

                            // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                            delta_X = pointList[i].X - mid_X;
                            delta_Y = pointList[i].Y - mid_Y;

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
                            double ratio = Math.Sqrt(2 * (1 - Math.Cos(3.1415926f * (double)3 / (double)5)) - (double)1 / (double)4) / ((double)1 / (double)2);
                            mid_coorinate_x = mid_X + ratio * delta_X;
                            mid_coorinate_y = mid_Y + ratio * delta_Y;

                            /////////////////// giai đoạn 2
                            // tìm kiếm trung điểm cạnh đã có
                            mid_X = (pointList[i].X + mid_coorinate_x) / (double)2;
                            mid_Y = (pointList[i].Y + mid_coorinate_y) / (double)2;

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
                            ratio = length * length * Math.Sin(3.1415926f * (double)3 / (double)5) / ((double)2 * mid_length * mid_length);
                            left_coorinate_x = mid_X - ratio * delta_X;
                            left_coorinate_y = mid_Y - ratio * delta_Y;

                            /////////////////// giai đoạn 2=3
                            // tìm kiếm trung điểm cạnh đã có
                            mid_X = (pointList[i + 1].X + mid_coorinate_x) / (double)2;
                            mid_Y = (pointList[i + 1].Y + mid_coorinate_y) / (double)2;

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
                            ratio = length * length * Math.Sin(3.1415926f * (double)3 / (double)5) / ((double)2 * mid_length * mid_length);
                            right_coorinate_x = mid_X + ratio * delta_X;
                            right_coorinate_y = mid_Y + ratio * delta_Y;

                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i].Y);//output vertex
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);//output vertex
                            gl.Vertex(right_coorinate_x, gl.RenderContextProvider.Height - right_coorinate_y);//output vertex
                            gl.Vertex(mid_coorinate_x, gl.RenderContextProvider.Height - mid_coorinate_y);//output verte
                            gl.Vertex(left_coorinate_x, gl.RenderContextProvider.Height - left_coorinate_y);//output verte
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }
        public class MyRegular_Six_Angle : Object
        {
            public override void DrawObject(OpenGL gl, Color colorUser, float sizeUser)
            {
                if (pointList != null)
                {
                    gl.LineWidth(sizeUser);
                    gl.Color(colorUser.R / 255.0, colorUser.G / 255.0, colorUser.B / 255.0, 0);

                    int n = pointList.Count();
                    double mid_X, mid_Y, delta_X, delta_Y;
                    double Third_x, Third_y, Fourth_x, Fourth_y, Fifth_x, Fifth_y, Sixth_x, Sixth_y;

                    for (int i = 0; i < n - 1; i += 2)
                    {
                        if (typeList[i / 2] == 6)
                        {
                            gl.Begin(OpenGL.GL_LINE_LOOP);

                            //////////////////////tính điểm thứ 5
                            mid_X = pointList[i].X;
                            mid_Y = pointList[i].Y;


                            // tính hệ số góc
                            delta_X = pointList[i + 1].X - mid_X;
                            delta_Y = pointList[i + 1].Y - mid_Y;

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

                            double ratio = Math.Sqrt(3);
                            Fifth_x = mid_X - ratio * delta_X;
                            Fifth_y = mid_Y - ratio * delta_Y;

                            ////////////////////////// tính điểm thứ 4
                            mid_X = pointList[i + 1].X;
                            mid_Y = pointList[i + 1].Y;

                            // tính hệ số góc
                            delta_X = pointList[i].X - mid_X;
                            delta_Y = pointList[i].Y - mid_Y;

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

                            Fourth_x = mid_X + ratio * delta_X;
                            Fourth_y = mid_Y + ratio * delta_Y;

                            ////////////////// tính điểm thứ 3
                            mid_X = (pointList[i + 1].X + Fourth_x) / (double)2;
                            mid_Y = (pointList[i + 1].Y + Fourth_y) / (double)2;

                            // tính hệ số góc
                            delta_X = pointList[i + 1].X - mid_X;
                            delta_Y = pointList[i + 1].Y - mid_Y;

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
                            ratio = (double)1 / Math.Sqrt(3);
                            Third_x = mid_X - ratio * delta_X;
                            Third_y = mid_Y - ratio * delta_Y;

                            ////////////////// tính điểm thứ 4
                            ratio = (double)3 / Math.Sqrt(3);
                            Sixth_x = mid_X + ratio * delta_X;
                            Sixth_y = mid_Y + ratio * delta_Y;

                            gl.Vertex(pointList[i].X, gl.RenderContextProvider.Height - pointList[i].Y);//output vertex
                            gl.Vertex(pointList[i + 1].X, gl.RenderContextProvider.Height - pointList[i + 1].Y);//output vertex
                            gl.Vertex(Third_x, gl.RenderContextProvider.Height - Third_y);//output vertex
                            gl.Vertex(Fourth_x, gl.RenderContextProvider.Height - Fourth_y);//output verte
                            gl.Vertex(Fifth_x, gl.RenderContextProvider.Height - Fifth_y);//output verte
                            gl.Vertex(Sixth_x, gl.RenderContextProvider.Height - Sixth_y);//output verte
                            gl.End();
                            gl.Flush();
                        }
                    }
                }
            }
        }
    }
    
}
