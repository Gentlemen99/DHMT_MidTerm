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
        public struct mypolygon
        {
            public List<Point> polygon;
            public Color color;
            public float size;
            
            public mypolygon(List<Point> d_polygon, Color d_color, float d_size)
            {
                polygon = d_polygon;
                color = d_color;
                size = d_size;
            }
        }


        static Color colorUser;

        static Color color_fill;
        static float sizeUser;

        OpenGL gl;
        Point pStart, pEnd;



        static List<Point> line_list = new List<Point>();
        static List<Point> circle_list = new List<Point>();
        static List<Point> ellipse_list = new List<Point>();
        static List<Point> rectangle_list = new List<Point>();
        static List<Point> triangle_list = new List<Point>();
        static List<Point> pentagon_list = new List<Point>();
        static List<Point> hexagon_list = new List<Point>();
        static List<mypolygon> polygon_list = new List<mypolygon>();

        static List<Color> color_line_list = new List<Color>();
        static List<Color> color_circle_list = new List<Color>();
        static List<Color> color_ellipse_list = new List<Color>();
        static List<Color> color_rectangle_list = new List<Color>();
        static List<Color> color_triangle_list = new List<Color>();
        static List<Color> color_pentagon_list = new List<Color>();
        static List<Color> color_hexagon_list = new List<Color>();

        static List<float> size_line_list = new List<float>();
        static List<float> size_circle_list = new List<float>();
        static List<float> size_ellipse_list = new List<float>();
        static List<float> size_rectangle_list = new List<float>();
        static List<float> size_triangle_list = new List<float>();
        static List<float> size_pentagon_list = new List<float>();
        static List<float> size_hexagon_list = new List<float>();

        static int want_to_draw;

        static int state_fill_color;
        static int index_fill_color;
        static Point point_to_fill;
        static int type_fill_color;

        static int state_down_mouse;
        static int state_right_click_mouse_down;

        public struct RGB_color
        {
            public byte r;
            public byte g;
            public byte b;
        }

        public struct floodfill
        {
            public RGB_color color;
            public Point bloodfill_mousedown;
            public int type;

            public floodfill(RGB_color set_color, Point mousedown, int dtype)
            {
                color = set_color;
                bloodfill_mousedown = mousedown;
                type = dtype;
            }
        }

        static List<floodfill> floodfill_list = new List<floodfill>();

        
        public struct Edge
        {
            public Point A;
            public Point B;
            public Edge(Point d_A, Point d_B)
            {
                A = d_A;
                B = d_B;
            }
        }

        public struct AEL
        {
            public int y_upper;
            public double x_int;
            public double reci_slope;
            public int y_lower;

            public AEL(int d_y_upper, double d_x_int, double d_reci_slope, int d_y_lower)
            {
                y_upper = d_y_upper;
                x_int = d_x_int;
                reci_slope = d_reci_slope;
                y_lower = d_y_lower;
            }
        }

        static Object mylines, mycircles, myrectangles, myellipses, myEqua_triangles, myRegular_Five_angles, myRegular_Six_angles;
        public Form1()
        {
            InitializeComponent();
            gl = openGLControl.OpenGL;


            want_to_draw = -1;
            state_fill_color = 0;

            sizeUser = 1.0f;
            colorUser = Color.White;

            //các mảng lưu các hình vẽ
            mylines = new MyLine();
            mycircles = new MyCircle();
            myrectangles = new MyRectangle();
            myellipses = new MyEllipse();
            myEqua_triangles = new MyEqua_Triangle();
            myRegular_Five_angles = new MyRegular_Five_Angle();
            myRegular_Six_angles = new MyRegular_Six_Angle();

            //trạng thái vẽ hình
            state_down_mouse = 0;

            //trang thai to mau theo dong quet, neu 0 la to mau loang
            type_fill_color = 1;

            //trang thai hinh to mau, nhan gia tri tu "0->6" va "-1"
            state_fill_color = -1;

            //trang thai right_click cua ve polygon
            state_right_click_mouse_down = 0;
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


        public RGB_color convert_ColorSystem_to_RGBColor(Color systemcolor)
        {
            RGB_color color;
            color.r = systemcolor.R;
            color.g = systemcolor.G;
            color.b = systemcolor.B;

            return color;
        }

        private void ctrl_OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (want_to_draw == -2) //trang thai to mau loang
            {
                point_to_fill = e.Location;
                floodfill tmp = new floodfill(convert_ColorSystem_to_RGBColor(color_fill), point_to_fill, type_fill_color);
                floodfill_list.Add(tmp);
            }
            else if (want_to_draw != -1)
            {
                pStart = e.Location;
                pEnd = pStart;
                state_down_mouse = 1;
                if (want_to_draw == 7)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        state_right_click_mouse_down = 1;
                }

            }
            else
            {

            }
        }



        private void ctrl_openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            pEnd = e.Location;
            if (want_to_draw == -2) //trang thai to mau 
            {

            }
            else if (want_to_draw != -1)
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
                    myellipses.append(pStart, pEnd);
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
                else if (want_to_draw == 7)
                {
                    if (state_right_click_mouse_down == 0)
                    {

                    }
                    else
                    {

                    }
                }
                state_down_mouse = 0;
            }
        }//end opengl mouse up

        private void bt_Polygon_Click(object sender, EventArgs e)
        {
            want_to_draw = 7;
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
                    myellipses.append(pStart, pEnd);
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
            else// trang thai khac
            {

            }
        } // end opengl mouse move

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

        private void bt_Ellipse_Click(object sender, EventArgs e)
        {
            want_to_draw = 3;
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

        public bool isSameColor(RGB_color A, RGB_color B)
        {
            if ((A.r >> 1)  == ((B.r >> 1))
                && (A.g >> 1) == ((B.g >> 1))
                && (A.b >> 1) == ((B.b >> 1)))
                return true;

            return false;
        }

        private RGB_color GetPixel(int x, int y)
        {
            byte[] read_pixel = new byte[3];

            gl.ReadPixels(x, gl.RenderContextProvider.Height - y, 1, 1, OpenGL.GL_RGB, OpenGL.GL_BYTE, read_pixel);

            RGB_color color;
            color.r = (byte)(read_pixel[0] << 1);
            color.g = (byte)(read_pixel[1] << 1);
            color.b = (byte)(read_pixel[2] << 1);


            return color;
        }

        private void PutPixel(int x, int y, RGB_color color)
        {
            byte[] read_pixel = { color.r, color.g, color.b };
            //read_pixel[0] = color.r;
            //read_pixel[1] = color.g;
            //read_pixel[2] = color.b;

            gl.RasterPos(x, gl.RenderContextProvider.Height - y);
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, read_pixel);

            gl.Flush();
        }

        //private bool isvisited(List<Point> tmpList, Point P)
        //{
        //    for (int i =0; i < tmpList.Count(); i++)
        //    {
        //        if (P.X == tmpList[i].X && P.Y == tmpList[i].Y)
        //            return true;
        //    }
        //    return false;
        //}

        private bool is_inside_object(int x, int y, int index)
        {
            Point point = new Point(x, y);
            bool tmp = false;

            if (state_fill_color == 0)
                tmp = mylines.is_inside_object(point, index);
            else if (state_fill_color == 1)
                tmp = mycircles.is_inside_object(point, index);
            else if (state_fill_color == 2)
                tmp = myrectangles.is_inside_object(point, index);
            else if (state_fill_color == 3)
                tmp = myellipses.is_inside_object(point, index);
            else if (state_fill_color == 4)
                tmp = myEqua_triangles.is_inside_object(point, index);
            else if (state_fill_color == 5)
                tmp = myRegular_Five_angles.is_inside_object(point, index);
            else if (state_fill_color == 6)
                tmp = myRegular_Six_angles.is_inside_object(point, index);

            return tmp;
        }

        private void BoundaryFill(int x, int y, RGB_color F_color, int index)
        {
            RGB_color current_color;
            Queue<Point> myqueue = new Queue<Point>();

            current_color = GetPixel(x, y);
            Point S;

            if (!isSameColor(current_color, F_color) && is_inside_object(x, y, index))
            {
                S = new Point(x, y);
                PutPixel(x, y, F_color);
                myqueue.Enqueue(S);
            }

            

            int current_x, current_y, new_x, new_y;
            int count = 0;
            while (myqueue.Count() > 0)
            {
                count++;
                //if (count == (int)1e7)
                //    break;
                S = myqueue.Dequeue();

                current_x = S.X;
                current_y = S.Y;
                if (current_x >= 0 && current_y >= 0)
                {
                    

                    int[] direction = { -1, 0, 1, 0, -1 };
                    int[] d_x = {-1, 0, 1, 0 };
                    int[] d_y = {0, 1, 0, -1 };
                    for (int i = 0; i < 4; i++)
                    {
                        new_x = (current_x + d_x[i]);
                        new_y = (current_y + d_y[i]);

                        current_color = GetPixel(new_x, new_y);

                        if (!isSameColor(current_color, F_color) && is_inside_object(new_x, new_y, index))
                        {
                            PutPixel(new_x, new_y, F_color);
                            Point P = new Point(new_x, new_y);
                            myqueue.Enqueue(P);
                        }
                    }
                }
            }
        }


        // main process
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {

            // Get the OpenGL object.
            if (want_to_draw != -1)
            {
                // Clear the color and depth buffer.
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                mylines.DrawObject(gl);
                mycircles.DrawObject(gl);
                myrectangles.DrawObject(gl);
                myellipses.DrawObject(gl);
                myEqua_triangles.DrawObject(gl);
                myRegular_Five_angles.DrawObject(gl);
                myRegular_Six_angles.DrawObject(gl);


                /*
                 * fill color
                 * 
                 */
                int n_floodfill = floodfill_list.Count();
                if (n_floodfill > 0) 
                {
                    for (int k = 0; k < n_floodfill; k++)
                    {
                        floodfill tmp = floodfill_list[k];
                        index_fill_color = checking_is_inside_object(tmp.bloodfill_mousedown);

                        if (index_fill_color != -1 && state_fill_color != - 1)
                        {
                            RGB_color color = tmp.color;

                            if (tmp.type == 0) // to loang (blood fill)
                                BoundaryFill(tmp.bloodfill_mousedown.X, tmp.bloodfill_mousedown.Y, color, index_fill_color);
                            else // to theo dong quet (scan line)
                            {
                                List<Point> object_point = new List<Point>();
                                object_point = get_list_point_of_one_object(index_fill_color, state_fill_color);

                                int max_y = -9999999, min_y = 9999999;


                                /*
                                 * luu lai cac canh cua da giac,loai bo canh co do doc = 0 va dong thoi lam ngan canh tai diem khong cuc tri
                                 */
                                int n_object_point = object_point.Count();


                                List<Edge> object_edge = new List<Edge>();
                                for (int it = 0; it < n_object_point; it++) 
                                {
                                    //tim max va min cua y  
                                    max_y = Math.Max(object_point[it].Y, max_y);
                                    min_y = Math.Min(object_point[it].Y, min_y);

                                    int it_cur = (it) % n_object_point;
                                    int it_next = (it + 1) % n_object_point;
                                    int it_prev = (it - 1 + n_object_point) % n_object_point;

                                    //do doc (1 / reci_slope) = 0 thi bo qua
                                    if (object_point[it_cur].Y == object_point[it_next].Y)
                                        continue;

                                    // kiem tra diem cuc tri dong thoi lam ngan canh neu khong cuc tri
                                    if (object_point[it_prev].Y <= object_point[it_cur].Y && object_point[it_cur].Y <= object_point[it_next].Y)
                                    {
                                        Point P = new Point(object_point[it_cur].X, object_point[it_cur].Y + 1);
                                        object_point[it_cur] = P;
                                    }
                                    else if (object_point[it_cur].Y >= object_point[it_next].Y && object_point[it_cur].Y <= object_point[it_prev].Y)
                                    {
                                        Point P = new Point(object_point[it_cur].X, object_point[it_cur].Y - 1);
                                        object_point[it_cur] = P;
                                    }

                                    Edge tmp_Edge = new Edge(object_point[it_cur], object_point[it_next]);
                                    object_edge.Add(tmp_Edge);
                                }

                                int n_object_edge = object_edge.Count();

                                //gl.Color(color.r / 255.0, color.g / 255.0, color.b / 255.0, 0);
                                //for (int it = 0; it < n_object_edge; it++)
                                //{
                                //    int it_next = (it + 1) % n_object_edge;
                                //    gl.Begin(OpenGL.GL_LINES);
                                //    gl.Vertex(object_edge[it].A.X, gl.RenderContextProvider.Height - object_edge[it].A.Y);
                                //    gl.Vertex(object_edge[it].B.X, gl.RenderContextProvider.Height - object_edge[it].B.Y);

                                //}

                                ////tao et
                                //List<List<int>> ET = new List<List<int>>();
                                //List<int> tmp_AEL_list_index;
                                //for (int ite_y = min_y; ite_y <= max_y; ite_y++)
                                //{
                                //    tmp_AEL_list_index = new List<int>();
                                //    for (int it = 0; it < n_object_edge; it++)
                                //    {
                                //        if (Math.Min(object_edge[it].A.Y, object_edge[it].B.Y) == ite_y)
                                //            tmp_AEL_list_index.Add(it);
                                //    }
                                //    ET.Add(tmp_AEL_list_index);
                                //}

                                /*
                                 * luu cac canh da duoc tinh che vao AEL list
                                 */


                                List<AEL> AEL_list = new List<AEL>();
                                AEL tmp_AEL;

                                for (int it = 0; it < n_object_edge; it++)
                                {

                                    double x_int;
                                    double reci_slope;
                                    if (object_edge[it].A.Y < object_edge[it].B.Y)
                                    {
                                        reci_slope = 1.0d*(object_edge[it].B.X - object_edge[it].A.X) / ((object_edge[it].B.Y - object_edge[it].A.Y)* 1.0d);
                                        x_int = 1.0d*object_edge[it].A.X;
                                    }
                                    else
                                    {
                                        reci_slope = 1.0d*(object_edge[it].A.X - object_edge[it].B.X) / ((object_edge[it].A.Y - object_edge[it].B.Y) * 1.0d);
                                        x_int = 1.0d*object_edge[it].B.X;
                                    }
                                    tmp_AEL = new AEL(Math.Max(object_edge[it].A.Y, object_edge[it].B.Y), x_int, reci_slope, Math.Min(object_edge[it].A.Y, object_edge[it].B.Y));
                                    AEL_list.Add(tmp_AEL);
                                }

                                /*
                                 * Scan line
                                 *
                                 */
                                gl.Color(color.r / 255.0, color.g / 255.0, color.b / 255.0, 0);

                                List<AEL> BegList = new List<AEL>();
                                for (int it_y = min_y; it_y <= max_y; it_y++)
                                {
                                    // them vao beglist nhung canh co y_lower = dong quet => chi them 1 lan duy nhat (do dong quet tang dan)
                                    for (int it = 0; it < AEL_list.Count(); it++)
                                        if (AEL_list[it].y_lower == it_y)
                                            BegList.Add(AEL_list[it]);

                                    // sap xep lai toa do giao diem
                                    BegList = Sort_Beglist(BegList);

                                    // ve tu giao diem le den giao diem chan
                                    for (int it = 0; it < BegList.Count(); it++)
                                    {
                                        int it_next = it + 1;
                                        if (it % 2 == 0 && it_next < BegList.Count())
                                        {
                                            gl.Begin(OpenGL.GL_LINES);
                                            gl.Vertex(BegList[it].x_int, gl.RenderContextProvider.Height - it_y);
                                            gl.Vertex(BegList[it_next].x_int, gl.RenderContextProvider.Height - it_y);
                                        }

                                    }
                                    //loai bo canh co dong quet cham den y_upper
                                    for (int it = 0; it < BegList.Count(); it++)
                                    {
                                        if (BegList[it].y_upper == it_y)
                                            BegList.RemoveAt(it);
                                    }
                                    // cap nhat lai toa do giao diem x_int = reci_slope
                                    for (int it = 0; it < BegList.Count(); it++)
                                    {
                                        tmp_AEL = new AEL(BegList[it].y_upper, (BegList[it].x_int + BegList[it].reci_slope), BegList[it].reci_slope, BegList[it].y_lower);
                                        BegList[it] = tmp_AEL;
                                    }

                                }
                                gl.End();
                                gl.Flush();
                            }// end scanline
                            state_fill_color = -1;
                            index_fill_color = -1;
                        } // end of checking state
                    }// end loop
                }
            }
            else
            {

            }
        }

        public List<AEL> Sort_Beglist(List<AEL> tmp_AEL_list)
        {
            AEL tmp_AEL_1, tmp_AEL_2;

            int n = tmp_AEL_list.Count();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++) 
                {
                    if (tmp_AEL_list[i].x_int > tmp_AEL_list[j].x_int)
                    {

                        tmp_AEL_1 = new AEL(tmp_AEL_list[i].y_upper, tmp_AEL_list[i].x_int, tmp_AEL_list[i].reci_slope, tmp_AEL_list[i].y_lower);

                        tmp_AEL_2 = new AEL(tmp_AEL_list[j].y_upper, tmp_AEL_list[j].x_int, tmp_AEL_list[j].reci_slope, tmp_AEL_list[j].y_lower);

                        tmp_AEL_list[i] = tmp_AEL_2;
                        tmp_AEL_list[j] = tmp_AEL_1;
                    }
                }
            }

            return tmp_AEL_list;
        }

        public int checking_is_inside_object(Point point)
        {
            int index = -1;
            //tim trong danh sach cac duong thang
            index = mylines.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 0;
                return index;
            }

            //tim trong danh sach cac duong tron
            index = mycircles.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 1;
                return index;
            }

            //tim trong danh sach cac hinh chu nhat
            index = myrectangles.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 2;
                return index;
            }

            ////tim trong danh sach cac hinh ellipse
            index = myellipses.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 3;
                return index;
            }

            //tim trong danh sach cac tam giac DEU
            index = myEqua_triangles.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 4;
                return index;
            }
            //tim trong danh sach cac ngu giac deu
            index = myRegular_Five_angles.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 5;
                return index;
            }
            //tim trong danh sach cac luc giac deu
            index = myRegular_Six_angles.find_index_of_object(point);
            if (index != -1)
            {
                state_fill_color = 6;
                return index;
            }

            return -1;
        }

        public List<Point> get_list_point_of_one_object(int index, int type)
        {
            List<Point> tmp = new List<Point>();

            if (type == 0)
                tmp = mylines.get_list_point_of_object(index);
            else if (type == 1)
                tmp = mycircles.get_list_point_of_object(index);
            else if (type == 2)
                tmp = myrectangles.get_list_point_of_object(index);
            else if (type == 3)
                tmp = myellipses.get_list_point_of_object(index);
            else if (type == 4)
                tmp = myEqua_triangles.get_list_point_of_object(index);
            else if (type == 5)
                tmp = myRegular_Five_angles.get_list_point_of_object(index);
            else if (type == 6)
                tmp = myRegular_Six_angles.get_list_point_of_object(index);

            return tmp;
        }

        private void bt_Color_Fill_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                want_to_draw = -2;
                color_fill = colorDialog2.Color;
            }
        }

        private void bt_BloodFill_Click(object sender, EventArgs e)
        {
            type_fill_color = 0;
        }

        private void bt_ScanLine_Click(object sender, EventArgs e)
        {
            type_fill_color = 1;
        }

        public abstract class Object
        {



            protected double euclid_distance(Point A, Point B)
            {
                return Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
            }

            protected double Herong(Point A, Point B, Point C)
            {
                double AB = euclid_distance(A, B);
                double AC = euclid_distance(A, C);
                double BC = euclid_distance(B, C);
                double p = (AB + AC + BC) / 2;

                return Math.Sqrt((p * (p - AB) * (p - AC) * (p - BC)));
            }
            protected double area_of_object(List<Point> list, int _want_to_draw)
            {
                if (_want_to_draw == 0)
                    return 0.0f;
                else if (_want_to_draw == 1) // duong tron
                {
                    double r = euclid_distance(list[0], list[1]);
                    double pi = 3.14;

                    return pi * r * r;
                }
                else if (_want_to_draw == 3) // ellipse
                {
                    //////////////
                    return 0.0f;
                }
                else
                {
                    Point A = list[0];
                    int n = list.Count();
                    double sum = 0;
                    for (int i = 1; i < n - 1; i++)
                    {
                        Point B = list[i];
                        Point C = list[i + 1];
                        sum += Herong(A, B, C);
                    }
                    return sum;

                }
                return 0.0f;
            }


            //// the position of 3 points
            //static int orientation(Point A, Point B, Point C)
            //{
            //    double d = (B.Y - A.Y) * (C.X - B.X) - (B.X - A.X) * (C.Y - B.Y);
            //    if (d == 0)
            //        return 0; //thang hang
            //    if (d > 0)
            //        return 1;
            //    return 2;
            //}


            ////if A, B, C is a line then check whether B is between A and C or not?
            //static bool is_on_the_line(Point A, Point B, Point C)
            //{
            //    int max_x = Math.Max(A.X, C.X);
            //    int min_x = Math.Min(A.X, C.X);
            //    int max_y = Math.Max(A.Y, C.Y);
            //    int min_y = Math.Min(A.Y, C.Y);

            //    if (B.X >= min_x && B.X <= max_x && B.Y >= min_y && B.Y <= max_y)
            //        return true;

            //    return false;
            //}

            //static bool is_segment(Point A, Point B, Point C, Point D)
            //{
            //    int o1 = orientation(A, B, C);
            //    int o2 = orientation(A, B, D);
            //    int o3 = orientation(C, D, A);
            //    int o4 = orientation(C, D, B);

            //    if (o1 != o2 && o3 != o4)
            //    {
            //        return true;
            //    }

            //    // A, B, C thang hang, kiem tra xem C co nam tren doan AB hay khong
            //    if (o1 == 0 && is_on_the_line(A, C, B))
            //        return true;
            //    // A, B, D thang hang, kiem tra xem D co nam tren doan AB hay khong
            //    if (o2 == 0 && is_on_the_line(A, D, B))
            //        return true;

            //    // C, D, A thang hang, kiem tra xem A co nam tren doan CD hay khong
            //    if (o3 == 0 && is_on_the_line(C, A, D))
            //        return true;
            //    // C, D, B thang hang, kiem tra xem B co nam tren doan CD hay khong
            //    if (o4 == 0 && is_on_the_line(C, B, D))
            //        return true;


            //    return false; 
            //}

            static public bool onSegment(Point p, Point q, Point r)
            {
                if (q.X <= Math.Max(p.X, r.X) &&
                    q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) &&
                    q.Y >= Math.Min(p.Y, r.Y))
                {
                    return true;
                }
                return false;
            }

            // To find orientation of ordered triplet (p, q, r). 
            // The function returns following values 
            // 0 --> p, q and r are colinear 
            // 1 --> Clockwise 
            // 2 --> Counterclockwise 
            static public int orientation(Point p, Point q, Point r)
            {
                int val = (q.Y - p.Y) * (r.X - q.X) -
                          (q.X - p.X) * (r.Y - q.Y);

                if (val == 0)
                {
                    return 0; // colinear 
                }
                return (val > 0) ? 1 : 2; // clock or counterclock wise 
            }

            // The function that returns true if  
            // line segment 'p1q1' and 'p2q2' intersect. 
            static public bool doIntersect(Point p1, Point q1,
                                    Point p2, Point q2)
            {
                // Find the four orientations needed for  
                // general and special cases 
                int o1 = orientation(p1, q1, p2);
                int o2 = orientation(p1, q1, q2);
                int o3 = orientation(p2, q2, p1);
                int o4 = orientation(p2, q2, q1);

                // General case 
                if (o1 != o2 && o3 != o4)
                {
                    return true;
                }

                // Special Cases 
                // p1, q1 and p2 are colinear and 
                // p2 lies on segment p1q1 
                if (o1 == 0 && onSegment(p1, p2, q1))
                {
                    return true;
                }

                // p1, q1 and p2 are colinear and 
                // q2 lies on segment p1q1 
                if (o2 == 0 && onSegment(p1, q2, q1))
                {
                    return true;
                }

                // p2, q2 and p1 are colinear and 
                // p1 lies on segment p2q2 
                if (o3 == 0 && onSegment(p2, p1, q2))
                {
                    return true;
                }

                // p2, q2 and q1 are colinear and 
                // q1 lies on segment p2q2 
                if (o4 == 0 && onSegment(p2, q1, q2))
                {
                    return true;
                }

                // Doesn't fall in any of the above cases 
                return false;
            }


            public abstract bool is_inside_object(Point P, int index);


            public abstract void DrawObject(OpenGL gl);
            public abstract void append(Point first, Point second);
            public abstract int find_index_of_object(Point point);

            public abstract List<Point> get_list_point_of_object(int index);
        } // end class object



        public class MyLine : Object
        {
            public override void append(Point first, Point second)
            {
                int n = line_list.Count();
                for (int i = 0; i < n; i++)
                {
                    if (line_list[i] == first && state_down_mouse == 1)
                    {
                        line_list[i + 1] = second;
                        return;
                    }
                }
                line_list.Add(first);
                line_list.Add(second);

                color_line_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                int n = line_list.Count();
                if (line_list != null && n > 0)
                {
                    for (int i = 0; i < n; i += 2)
                    {
                        Color line_color = color_line_list[i / 2];
                        gl.Color(line_color.R / 255.0, line_color.G / 255.0, line_color.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINES);


                        gl.Vertex(line_list[i].X, gl.RenderContextProvider.Height - line_list[i].Y);
                        gl.Vertex(line_list[i + 1].X, gl.RenderContextProvider.Height - line_list[i + 1].Y);


                        gl.End();
                        gl.Flush();
                    }
                }
            }

            public override int find_index_of_object(Point point)
            {
                int n = line_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 2; i >= 0; i -= 2)
                {
                    if (mylines.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = line_list[index];
                Point B = line_list[index + 1];

                // kiem tra xem point co nam giua A va B hay khong
                if (euclid_distance(A, P) + euclid_distance(P, B) == euclid_distance(A, B))
                    return true;

                return false;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(line_list[index]);
                tmp.Add(line_list[index + 1]);
                return tmp;
            }
        }//end MyLine


        public class MyCircle : Object
        {
            public override void append(Point first, Point second)
            {
                int n = circle_list.Count();
                for (int i = 0; i < n; i++)
                {
                    if (circle_list[i] == first && state_down_mouse == 1)
                    {
                        circle_list[i + 1] = second;
                        return;
                    }
                }
                circle_list.Add(first);
                circle_list.Add(second);

                color_circle_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                double dRadius;
                int n = circle_list.Count();
                if (circle_list != null && n > 0)
                {
                    for (int i = 0; i < n; i += 2)
                    {
                        Color tmpColor = color_circle_list[i / 2];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);
                        dRadius = Math.Sqrt((circle_list[i].X - circle_list[i + 1].X) * (circle_list[i].X - circle_list[i + 1].X) + (circle_list[i].Y - circle_list[i + 1].Y) * (circle_list[i].Y - circle_list[i + 1].Y));
                        double x, y, theta;
                        for (int j = 0; j < 50; j++)
                        {
                            theta = 2.0f * 3.1415926f * (double)j / (double)50;//get the current angle

                            x = dRadius * Math.Cos(theta);//calculate the x component
                            y = dRadius * Math.Sin(theta);//calculate the y component

                            gl.Vertex(x + circle_list[i].X, gl.RenderContextProvider.Height - (y + circle_list[i].Y));//output vertex
                        }
                        gl.End();
                        gl.Flush();
                    }
                }
            }
            public override int find_index_of_object(Point point)
            {
                int n = circle_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 2; i >= 0; i -= 2)
                {
                    if (mycircles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point O = circle_list[index];
                Point A = circle_list[index + 1];

                double R = euclid_distance(O, A);

                double r = euclid_distance(O, P);

                if (R >= r)
                    return true;

                return false;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(circle_list[index]);
                tmp.Add(circle_list[index + 1]);
                return tmp;
            }
        }//end MyCircle

        public class MyEllipse : Object
        {
            public override void append(Point first, Point second)
            {
                int n = ellipse_list.Count();
                for (int i = 0; i < n; i++)
                {
                    if (ellipse_list[i] == first && state_down_mouse == 1)
                    {
                        ellipse_list[i + 1] = second;
                        return;
                    }
                }
                ellipse_list.Add(first);
                ellipse_list.Add(second);

                color_ellipse_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                int n = ellipse_list.Count();
                if (ellipse_list != null && n > 0)
                {
                    for (int i = 0; i < n; i += 2)
                    {
                        Color tmpColor = color_ellipse_list[i / 2];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);

                        double d_X, d_Y;
                        d_X = Math.Sqrt((ellipse_list[i].X - ellipse_list[i + 1].X) * (ellipse_list[i].X - ellipse_list[i + 1].X));
                        d_Y = Math.Sqrt((ellipse_list[i].Y - ellipse_list[i + 1].Y) * (ellipse_list[i].Y - ellipse_list[i + 1].Y));

                        double x = 1, y = 0, theta, t;

                        
                        for (int j = 0; j < 50; j++)
                        {
                            theta = 2.0f * 3.1415926f * j / (double)50;//get the current angle

                            t = x;
                            x = d_X * (Math.Cos(theta));//calculate the x component
                            y = d_Y * (Math.Sin(theta));//calculate the y component

                            gl.Vertex(x + ellipse_list[i].X, gl.RenderContextProvider.Height - (y + ellipse_list[i].Y));//output vertex
                        }
                        gl.End();
                        gl.Flush();
                    }
                }
            }
            public override int find_index_of_object(Point point)
            {
                int n = ellipse_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 2; i >= 0; i -= 2)
                {
                    if (myellipses.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point O = ellipse_list[index];
                Point A = ellipse_list[index + 1];

                double r_X, r_Y;
                r_X = Math.Sqrt((O.X - A.X) * (O.X - A.X));
                r_Y = Math.Sqrt((O.Y - A.Y) * (O.Y - A.Y));


                double p = (Math.Pow((double)(1.0*P.X - 1.0*O.X), 2) / Math.Pow(r_X, 2)) +
                           (Math.Pow((double)(1.0 * P.Y - 1.0 * O.Y), 2) / Math.Pow(r_Y, 2));

                if (p > 1)
                    return false;

                return true;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(ellipse_list[index]);
                tmp.Add(ellipse_list[index + 1]);
                return tmp;
            }
        }//end MyEllipse

        public class MyRectangle : Object
        {
            public override void append(Point first, Point second)
            {
                int n = rectangle_list.Count();
                Point B = new Point(second.X, first.Y);
                Point D = new Point(first.X, second.Y);

                for (int i = 0; i < n; i++)
                {
                    if (rectangle_list[i] == first && state_down_mouse == 1)
                    {

                        rectangle_list[i + 1] = B;
                        rectangle_list[i + 2] = second;
                        rectangle_list[i + 3] = D;
                        return;
                    }
                }

                rectangle_list.Add(first);
                rectangle_list.Add(B);
                rectangle_list.Add(second);
                rectangle_list.Add(D);

                color_rectangle_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                if (rectangle_list != null)
                {

                    int n = rectangle_list.Count();
                    for (int i = 0; i < n; i += 4)
                    {
                        Color tmpColor = color_rectangle_list[i / 4];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);

                        gl.Begin(OpenGL.GL_LINE_LOOP);
                        gl.Vertex(rectangle_list[i].X, gl.RenderContextProvider.Height - rectangle_list[i].Y);
                        gl.Vertex(rectangle_list[i + 1].X, gl.RenderContextProvider.Height - rectangle_list[i + 1].Y);
                        gl.Vertex(rectangle_list[i + 2].X, gl.RenderContextProvider.Height - rectangle_list[i + 2].Y);
                        gl.Vertex(rectangle_list[i + 3].X, gl.RenderContextProvider.Height - rectangle_list[i + 3].Y);
                        gl.End();
                        gl.Flush();
                    }
                }
            }


            public override int find_index_of_object(Point point)
            {
                int n = rectangle_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 4; i >= 0; i -= 4)
                {
                    if (myrectangles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = new Point(rectangle_list[index].X, rectangle_list[index].Y);
                Point B = new Point(rectangle_list[index + 1].X, rectangle_list[index + 1].Y);
                Point C = new Point(rectangle_list[index + 2].X, rectangle_list[index + 2].Y);
                Point D = new Point(rectangle_list[index + 3].X, rectangle_list[index + 3].Y);
                List<Point> tmpList = new List<Point>();
                tmpList.Add(A);
                tmpList.Add(B);
                tmpList.Add(C);
                tmpList.Add(D);


                Point Inf = new Point(1000, P.Y);

                int n = tmpList.Count();
                double area = area_of_object(tmpList, 6);

                int max_y = -999999, min_y = 999999;

                for (int it = 0; it < n; it++)
                {
                    max_y = Math.Max(max_y, tmpList[it].Y);
                    min_y = Math.Min(min_y, tmpList[it].Y);
                }

                int count = 0;
                int des_count = 0;
                int i = 0;
                while (i < n)
                {
                    int i_next = (i + 1) % n;
                    // giao nhau
                    if (doIntersect(tmpList[i], tmpList[i_next], P, Inf))
                    {
                        if (P.Y == tmpList[i].Y || P.Y == tmpList[i_next].Y)
                        {
                            if (P.Y == tmpList[i_next].Y && P.Y != max_y && P.Y != min_y)
                                des_count++;
                            //double sum = 0;
                            //for (int k = 0; k < n; k++)
                            //{
                            //    sum += Herong(P, tmpList[k], tmpList[(k + 1) % n]);
                            //}
                            //if (Math.Abs(sum - area) <= 0.1)
                            //    return true;
                            //else
                            //    return false;
                        }
                        
                        //neu P thang hang voi canh
                        if (orientation(tmpList[i], P, tmpList[i_next]) == 0)
                        {
                            //tra ve true neu P nam tren canh
                            return onSegment(tmpList[i], P, tmpList[i_next]);
                        }
                        count++;
                    }
                    i++;
                }
                if (((count - des_count) % 2) == 1)
                    return true;

                return false;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(rectangle_list[index]);
                tmp.Add(rectangle_list[index + 1]);
                tmp.Add(rectangle_list[index + 2]);
                tmp.Add(rectangle_list[index + 3]);
                return tmp;
            }
        }//end MyRectangle




        //tam giac deu
        public class MyEqua_Triangle : Object
        {
            private Point cal_other_point(Point first, Point second)
            {
                double mid_X, mid_Y, delta_X, delta_Y, x, y;
                // tìm kiếm trung điểm cạnh đã có
                mid_X = (first.X + second.X) / (double)2;
                mid_Y = (first.Y + second.Y) / (double)2;

                // tính hệ số góc cạnh đã có (tính tử và mẫu riêng tránh trường hợp chia cho 0)
                delta_X = first.X - mid_X;
                delta_Y = first.Y - mid_Y;

                double tmp;
                // tính hệ số góc của đường thẳng vuông góc với cạnh đã có
                if (delta_X != 0)
                {
                    tmp = -delta_X;
                    delta_X = delta_Y;
                    delta_Y = tmp;

                }
                else
                {
                    tmp = -delta_Y;
                    delta_Y = delta_X;
                    delta_X = tmp;
                }
                //tính điểm còn lại theo công thức x = mid_X + căn(3)*delta_X, y = mid_y + căn(3)*delta_Y
                x = mid_X + Math.Sqrt(3) * delta_X;
                y = mid_Y + Math.Sqrt(3) * delta_Y;
                Point tmpPoint = new Point((int)Math.Round(x), (int)Math.Round(y));
                return tmpPoint;
            }
            public override void append(Point first, Point second)
            {
                int n = triangle_list.Count();
                Point C = cal_other_point(first, second);
                for (int i = 0; i < n; i++)
                {
                    if (triangle_list[i] == first && state_down_mouse == 1)
                    {
                        triangle_list[i + 1] = second;
                        triangle_list[i + 2] = C;
                        return;
                    }
                }

                triangle_list.Add(first);
                triangle_list.Add(second);
                triangle_list.Add(C);

                color_triangle_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                if (triangle_list != null)
                {
                    int n = triangle_list.Count();

                    for (int i = 0; i < n; i += 3)
                    {
                        Color tmpColor = color_triangle_list[i / 3];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);

                        gl.Vertex(triangle_list[i].X, gl.RenderContextProvider.Height - triangle_list[i].Y);//output vertex
                        gl.Vertex(triangle_list[i + 1].X, gl.RenderContextProvider.Height - triangle_list[i + 1].Y);//output vertex
                        gl.Vertex(triangle_list[i + 2].X, gl.RenderContextProvider.Height - triangle_list[i + 2].Y);//output vertex

                        gl.End();
                        gl.Flush();
                    }
                }
            }

            public override int find_index_of_object(Point point)
            {
                int n = triangle_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 3; i >= 0; i -= 3)
                {
                    if (myEqua_triangles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = new Point(triangle_list[index].X, triangle_list[index].Y);
                Point B = new Point(triangle_list[index + 1].X, triangle_list[index + 1].Y);
                Point C = new Point(triangle_list[index + 2].X, triangle_list[index + 2].Y);
                List<Point> tmpList = new List<Point>();
                tmpList.Add(A);
                tmpList.Add(B);
                tmpList.Add(C);


                Point Inf = new Point(1000, P.Y);

                int n = tmpList.Count();
                double area = area_of_object(tmpList, 6);

                int max_y = -999999, min_y = 999999;

                for (int it = 0; it < n; it++)
                {
                    max_y = Math.Max(max_y, tmpList[it].Y);
                    min_y = Math.Min(min_y, tmpList[it].Y);
                }

                int count = 0;
                int des_count = 0;
                int i = 0;
                while (i < n)
                {
                    int i_next = (i + 1) % n;
                    // giao nhau
                    if (doIntersect(tmpList[i], tmpList[i_next], P, Inf))
                    {
                        if (P.Y == tmpList[i].Y || P.Y == tmpList[i_next].Y)
                        {
                            if (P.Y == tmpList[i_next].Y && P.Y != max_y && P.Y != min_y)
                                des_count++;
                            //double sum = 0;
                            //for (int k = 0; k < n; k++)
                            //{
                            //    sum += Herong(P, tmpList[k], tmpList[(k + 1) % n]);
                            //}
                            //if (Math.Abs(sum - area) <= 0.1)
                            //    return true;
                            //else
                            //    return false;
                        }

                        //neu P thang hang voi canh
                        if (orientation(tmpList[i], P, tmpList[i_next]) == 0)
                        {
                            //tra ve true neu P nam tren canh
                            return onSegment(tmpList[i], P, tmpList[i_next]);
                        }
                        count++;
                    }
                    i++;
                }
                if (((count - des_count) % 2) == 1)
                    return true;

                return false;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(triangle_list[index]);
                tmp.Add(triangle_list[index + 1]);
                tmp.Add(triangle_list[index + 2]);
                return tmp;
            }
        }// end triangle
        public class MyRegular_Five_Angle : Object
        {
            private Point[] cal_other_point(Point first, Point second)
            {
                List<Point> tmpList = new List<Point>();
                tmpList.Add(first);
                tmpList.Add(second);
                double x, y;
                for (int i = 0; i < 3; i++)
                {
                    x = tmpList[i].X + (1 + Math.Cos(Math.PI * 72 / 180)) * (tmpList[i + 1].X - tmpList[i].X) + Math.Sin(Math.PI * 72 / 180) * (tmpList[i].Y - tmpList[i + 1].Y);
                    y = tmpList[i].Y + (1 + Math.Cos(Math.PI * 72 / 180)) * (tmpList[i + 1].Y - tmpList[i].Y) + Math.Sin(Math.PI * 72 / 180) * (tmpList[i + 1].X - tmpList[i].X);
                    Point P = new Point((int)Math.Round(x), (int)Math.Round(y));
                    tmpList.Add(P);
                }

                Point[] ArrayPoint = { tmpList[2], tmpList[3], tmpList[4] };

                return ArrayPoint;
            }
            public override void append(Point first, Point second)
            {
                int n = pentagon_list.Count();
                Point[] ArrayPoint = cal_other_point(first, second);
                for (int i = 0; i < n; i++)
                {
                    if (pentagon_list[i] == first && state_down_mouse == 1)
                    {
                        pentagon_list[i + 1] = second;
                        pentagon_list[i + 2] = ArrayPoint[0];
                        pentagon_list[i + 3] = ArrayPoint[1];
                        pentagon_list[i + 4] = ArrayPoint[2];
                        return;
                    }
                }

                pentagon_list.Add(first);
                pentagon_list.Add(second);
                pentagon_list.Add(ArrayPoint[0]);
                pentagon_list.Add(ArrayPoint[1]);
                pentagon_list.Add(ArrayPoint[2]);

                color_pentagon_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                if (pentagon_list != null)
                {
                    int n = pentagon_list.Count();
                    for (int i = 0; i < n; i += 5)
                    {

                        Color tmpColor = color_pentagon_list[i / 5];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);


                        gl.Vertex(pentagon_list[i].X, gl.RenderContextProvider.Height - pentagon_list[i].Y);//output vertex
                        gl.Vertex(pentagon_list[i + 1].X, gl.RenderContextProvider.Height - pentagon_list[i + 1].Y);//output vertex
                        gl.Vertex(pentagon_list[i + 2].X, gl.RenderContextProvider.Height - pentagon_list[i + 2].Y);//output vertex
                        gl.Vertex(pentagon_list[i + 3].X, gl.RenderContextProvider.Height - pentagon_list[i + 3].Y);//output vertex
                        gl.Vertex(pentagon_list[i + 4].X, gl.RenderContextProvider.Height - pentagon_list[i + 4].Y);//output vertex


                        gl.End();
                        gl.Flush();

                    }
                }
            }

            public override int find_index_of_object(Point point)
            {
                int n = pentagon_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 5; i >= 0; i -= 5)
                {
                    if (myRegular_Five_angles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = new Point(pentagon_list[index].X, pentagon_list[index].Y);
                Point B = new Point(pentagon_list[index + 1].X, pentagon_list[index + 1].Y);
                Point C = new Point(pentagon_list[index + 2].X, pentagon_list[index + 2].Y);
                Point D = new Point(pentagon_list[index + 3].X, pentagon_list[index + 3].Y);
                Point E = new Point(pentagon_list[index + 4].X, pentagon_list[index + 4].Y);
                List<Point> tmpList = new List<Point>();
                tmpList.Add(A);
                tmpList.Add(B);
                tmpList.Add(C);
                tmpList.Add(D);
                tmpList.Add(E);
                Point Inf = new Point(1000, P.Y);

                int n = tmpList.Count();
                double area = area_of_object(tmpList, 6);

                int max_y = -999999, min_y = 999999;

                for (int it = 0; it < n; it++)
                {
                    max_y = Math.Max(max_y, tmpList[it].Y);
                    min_y = Math.Min(min_y, tmpList[it].Y);
                }

                int count = 0;
                int des_count = 0;
                int i = 0;
                while (i < n)
                {
                    int i_next = (i + 1) % n;
                    // giao nhau
                    if (doIntersect(tmpList[i], tmpList[i_next], P, Inf))
                    {
                        if (P.Y == tmpList[i].Y || P.Y == tmpList[i_next].Y)
                        {
                            if (P.Y == tmpList[i_next].Y && P.Y != max_y && P.Y != min_y)
                                des_count++;
                            //double sum = 0;
                            //for (int k = 0; k < n; k++)
                            //{
                            //    sum += Herong(P, tmpList[k], tmpList[(k + 1) % n]);
                            //}
                            //if (Math.Abs(sum - area) <= 0.1)
                            //    return true;
                            //else
                            //    return false;
                        }

                        //neu P thang hang voi canh
                        if (orientation(tmpList[i], P, tmpList[i_next]) == 0)
                        {
                            //tra ve true neu P nam tren canh
                            return onSegment(tmpList[i], P, tmpList[i_next]);
                        }
                        count++;
                    }
                    i++;
                }
                if (((count - des_count) % 2) == 1)
                    return true;

                return false;
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(pentagon_list[index]);
                tmp.Add(pentagon_list[index + 1]);
                tmp.Add(pentagon_list[index + 2]);
                tmp.Add(pentagon_list[index + 3]);
                tmp.Add(pentagon_list[index + 4]);
                return tmp;
            }
        } //end pentagon
        public class MyRegular_Six_Angle : Object
        {
            private Point[] cal_other_point(Point first, Point second)
            {
                List<Point> tmpList = new List<Point>();
                tmpList.Add(first);
                tmpList.Add(second);
                double x, y;
                for (int i = 0; i < 4; i++)
                {
                    x = tmpList[i].X + (1 + Math.Cos(Math.PI * 60 / 180)) * (tmpList[i + 1].X - tmpList[i].X) + Math.Sin(Math.PI * 60 / 180) * (tmpList[i].Y - tmpList[i + 1].Y);
                    y = tmpList[i].Y + (1 + Math.Cos(Math.PI * 60 / 180)) * (tmpList[i + 1].Y - tmpList[i].Y) + Math.Sin(Math.PI * 60 / 180) * (tmpList[i + 1].X - tmpList[i].X);
                    Point P = new Point((int)Math.Round(x), (int)Math.Round(y));
                    tmpList.Add(P);
                }

                Point[] ArrayPoint = { tmpList[2], tmpList[3], tmpList[4], tmpList[5] };

                return ArrayPoint;
            }
            public override void append(Point first, Point second)
            {
                int n = hexagon_list.Count();
                Point[] ArrayPoint = cal_other_point(first, second);
                for (int i = 0; i < n; i++)
                {
                    if (hexagon_list[i] == first)
                    {
                        hexagon_list[i + 1] = second;
                        hexagon_list[i + 2] = ArrayPoint[0];
                        hexagon_list[i + 3] = ArrayPoint[1];
                        hexagon_list[i + 4] = ArrayPoint[2];
                        hexagon_list[i + 5] = ArrayPoint[3];
                        return;
                    }
                }

                hexagon_list.Add(first);
                hexagon_list.Add(second);
                hexagon_list.Add(ArrayPoint[0]);
                hexagon_list.Add(ArrayPoint[1]);
                hexagon_list.Add(ArrayPoint[2]);
                hexagon_list.Add(ArrayPoint[3]);

                color_hexagon_list.Add(colorUser);
            }
            public override void DrawObject(OpenGL gl)
            {
                int n = hexagon_list.Count();
                if (pentagon_list != null && n > 0)
                {
                    
                    for (int i = 0; i < n; i += 6)
                    {

                        Color tmpColor = color_hexagon_list[i / 6];
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);


                        gl.Vertex(hexagon_list[i].X, gl.RenderContextProvider.Height - hexagon_list[i].Y);//output vertex
                        gl.Vertex(hexagon_list[i + 1].X, gl.RenderContextProvider.Height - hexagon_list[i + 1].Y);//output vertex
                        gl.Vertex(hexagon_list[i + 2].X, gl.RenderContextProvider.Height - hexagon_list[i + 2].Y);//output vertex
                        gl.Vertex(hexagon_list[i + 3].X, gl.RenderContextProvider.Height - hexagon_list[i + 3].Y);//output vertex
                        gl.Vertex(hexagon_list[i + 4].X, gl.RenderContextProvider.Height - hexagon_list[i + 4].Y);//output vertex
                        gl.Vertex(hexagon_list[i + 5].X, gl.RenderContextProvider.Height - hexagon_list[i + 5].Y);//output vertex

                        gl.End();
                        gl.Flush();

                    }
                }
            }

            public override int find_index_of_object(Point point)
            {
                int n = hexagon_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 6; i >= 0; i -= 6)
                {
                    if (myRegular_Six_angles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = new Point(hexagon_list[index].X, hexagon_list[index].Y);
                Point B = new Point(hexagon_list[index + 1].X, hexagon_list[index + 1].Y);
                Point C = new Point(hexagon_list[index + 2].X, hexagon_list[index + 2].Y);
                Point D = new Point(hexagon_list[index + 3].X, hexagon_list[index + 3].Y);
                Point E = new Point(hexagon_list[index + 4].X, hexagon_list[index + 4].Y);
                Point F = new Point(hexagon_list[index + 5].X, hexagon_list[index + 5].Y);
                List<Point> tmpList = new List<Point>();
                tmpList.Add(A);
                tmpList.Add(B);
                tmpList.Add(C);
                tmpList.Add(D);
                tmpList.Add(E);
                tmpList.Add(F);


                Point Inf = new Point(1000, P.Y);

                int n = tmpList.Count();

                double area = area_of_object(tmpList, 6);

                int max_y = -999999, min_y = 999999;

                for (int it = 0; it < n; it++)
                {
                    max_y = Math.Max(max_y, tmpList[it].Y);
                    min_y = Math.Min(min_y, tmpList[it].Y);
                }

                int count = 0;
                int des_count = 0;
                int i = 0;
                while (i < n)
                {
                    int i_next = (i + 1) % n;
                    // giao nhau
                    if (doIntersect(tmpList[i], tmpList[i_next], P, Inf))
                    {
                        if (P.Y == tmpList[i].Y || P.Y == tmpList[i_next].Y)
                        {
                            if (P.Y == tmpList[i_next].Y && P.Y != max_y && P.Y != min_y)
                                des_count++;
                            //double sum = 0;
                            //for (int k = 0; k < n; k++)
                            //{
                            //    sum += Herong(P, tmpList[k], tmpList[(k + 1) % n]);
                            //}
                            //if (Math.Abs(sum - area) <= 0.1)
                            //    return true;
                            //else
                            //    return false;
                        }
                        //neu P thang hang voi canh
                        if (orientation(tmpList[i], P, tmpList[i_next]) == 0)
                        {
                            //tra ve true neu P nam tren canh
                            return onSegment(tmpList[i], P, tmpList[i_next]);
                        }
                        count++;
                    }
                    i++;
                }
                if (((count - des_count)% 2) == 1)
                    return true;

                return false;
                
            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(hexagon_list[index]);
                tmp.Add(hexagon_list[index + 1]);
                tmp.Add(hexagon_list[index + 2]);
                tmp.Add(hexagon_list[index + 3]);
                tmp.Add(hexagon_list[index + 4]);
                tmp.Add(hexagon_list[index + 5]);
                return tmp;
            }
        } //end hexagon

        public class MyPolygon : Object
        {
            public override void append(Point first, Point second)
            {
                int n = polygon_list.Count();
                for (int i = 0; i < n; i++)
                {
                    int m = polygon_list[i].polygon.Count();
                    for (int j = 0; j < m; j++)
                    {
                        if (polygon_list[i].polygon[j] == first)
                        {
                            if ((j + 1) >= m)
                                polygon_list[i].polygon.Add(second);
                            else
                                polygon_list[i].polygon[j + 1] = second;
                            return;
                        }
                    }
                }
                List<Point> tmp_list = new List<Point>();
                tmp_list.Add(first);
                tmp_list.Add(second);
                mypolygon tmp = new mypolygon(tmp_list, colorUser, sizeUser);

                polygon_list.Add(tmp);
            }
            public override void DrawObject(OpenGL gl)
            {
                int n = polygon_list.Count();
                if (polygon_list != null && n > 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        int m = polygon_list[i].polygon.Count();
                        Color tmpColor = polygon_list[i].color;
                        gl.Color(tmpColor.R / 255.0, tmpColor.G / 255.0, tmpColor.B / 255.0, 0);
                        gl.Begin(OpenGL.GL_LINE_LOOP);
                        for (int j = 0; j < m; j++)
                        {
                            gl.Vertex(polygon_list[i].polygon[j].X, gl.RenderContextProvider.Height - polygon_list[i].polygon[i].Y);//output vertex
                        }
                        gl.End();
                        gl.Flush();


                    }
                }
            }

            public override int find_index_of_object(Point point)
            {
                int n = hexagon_list.Count();
                if (n < 0)
                    return -1;
                for (int i = n - 6; i >= 0; i -= 6)
                {
                    if (myRegular_Six_angles.is_inside_object(point, i))
                        return i;
                }

                return -1;
            }
            public override bool is_inside_object(Point P, int index)
            {
                Point A = new Point(hexagon_list[index].X, hexagon_list[index].Y);
                Point B = new Point(hexagon_list[index + 1].X, hexagon_list[index + 1].Y);
                Point C = new Point(hexagon_list[index + 2].X, hexagon_list[index + 2].Y);
                Point D = new Point(hexagon_list[index + 3].X, hexagon_list[index + 3].Y);
                Point E = new Point(hexagon_list[index + 4].X, hexagon_list[index + 4].Y);
                Point F = new Point(hexagon_list[index + 5].X, hexagon_list[index + 5].Y);
                List<Point> tmpList = new List<Point>();
                tmpList.Add(A);
                tmpList.Add(B);
                tmpList.Add(C);
                tmpList.Add(D);
                tmpList.Add(E);
                tmpList.Add(F);


                Point Inf = new Point(1000, P.Y);

                int n = tmpList.Count();

                double area = area_of_object(tmpList, 6);

                int max_y = -999999, min_y = 999999;

                for (int it = 0; it < n; it++)
                {
                    max_y = Math.Max(max_y, tmpList[it].Y);
                    min_y = Math.Min(min_y, tmpList[it].Y);
                }

                int count = 0;
                int des_count = 0;
                int i = 0;
                while (i < n)
                {
                    int i_next = (i + 1) % n;
                    // giao nhau
                    if (doIntersect(tmpList[i], tmpList[i_next], P, Inf))
                    {
                        if (P.Y == tmpList[i].Y || P.Y == tmpList[i_next].Y)
                        {
                            if (P.Y == tmpList[i_next].Y && P.Y != max_y && P.Y != min_y)
                                des_count++;
                            //double sum = 0;
                            //for (int k = 0; k < n; k++)
                            //{
                            //    sum += Herong(P, tmpList[k], tmpList[(k + 1) % n]);
                            //}
                            //if (Math.Abs(sum - area) <= 0.1)
                            //    return true;
                            //else
                            //    return false;
                        }
                        //neu P thang hang voi canh
                        if (orientation(tmpList[i], P, tmpList[i_next]) == 0)
                        {
                            //tra ve true neu P nam tren canh
                            return onSegment(tmpList[i], P, tmpList[i_next]);
                        }
                        count++;
                    }
                    i++;
                }
                if (((count - des_count) % 2) == 1)
                    return true;

                return false;

            }

            public override List<Point> get_list_point_of_object(int index)
            {
                List<Point> tmp = new List<Point>();
                tmp.Add(hexagon_list[index]);
                tmp.Add(hexagon_list[index + 1]);
                tmp.Add(hexagon_list[index + 2]);
                tmp.Add(hexagon_list[index + 3]);
                tmp.Add(hexagon_list[index + 4]);
                tmp.Add(hexagon_list[index + 5]);
                return tmp;
            }
        } //end hexagon
    } // end class
}//end Form
