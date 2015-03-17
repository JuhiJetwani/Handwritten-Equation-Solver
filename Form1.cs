using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Linq.Expressions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.ML;
using Emgu.CV.ML.Structure;
using NCalc;

namespace IAProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string FinalEquation;
        double convexity;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Image<Bgr, byte> temp = new Image<Bgr, byte>(@"C:\Users\SONY\Desktop\Sem5\IA\project\pictures\5.jpg"); //single 7, works with threshold mask=25
            Image<Bgr, byte> temp = new Image<Bgr, byte>(@"C:\Users\SONY\Desktop\Sem5\IA\project\pictures\10.jpg"); 
            temp = temp.Resize(500, 500, Emgu.CV.CvEnum.INTER.CV_INTER_NN, true);
            imageBox1.Image = temp;
            String equation = "";
            #region covert image to grayscale and apply Adaptive Thresholding
            Image<Gray, byte> gray = temp.Convert<Gray, byte>();
            gray = gray.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_GAUSSIAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY_INV, 25, new Gray(25));
            //gray = gray.ThresholdBinary(new Gray(100), new Gray(255));
            imageBox2.Image = gray;
            #endregion

            #region Created Rectangle to store the bounding rectangles to count the number of entities in the image
            List<Rectangle> boundingBox = new List<Rectangle>();
            #endregion

            #region Created Points to store Contour Points
            List<Point[]> digitContours = new List<Point[]>();
            List<Point[]> horizontalContours = new List<Point[]>();
            List<Point[]> verticalContours = new List<Point[]>();
            List<Point[]> dotContours = new List<Point[]>();
            #endregion

            #region Data structures to store segmented Image and List of segmented Images
            Image<Gray, Byte> segmented = new Image<Gray,byte>(0,0);
            List<Image<Gray, Byte>> segmentedImageList = new List<Image<Gray, Byte>>();
            #endregion

            SortedDictionary<float, string> sortedEquation = new SortedDictionary<float, string>();

            #region FINDINNG CONTOURS
            int iks = 0;
            using (MemStorage storage = new MemStorage())
            {
                for (Contour<Point> contours = gray.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, storage); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);
                    #region subtraction sign
                    if (currentContour.BoundingRectangle.Height < 15 && currentContour.BoundingRectangle.Height > 0 && currentContour.BoundingRectangle.Width > 15)
                    {
                        Console.Out.WriteLine("Horizontal");
                        CvInvoke.cvDrawContours(temp, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        temp.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 255), 1);
                        //segmented_image.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 255), 1);
                        //segmented_image.Draw(currentContour, 2);
                        boundingBox.Add(currentContour.BoundingRectangle);
                        float xcentroid = xCentroid(currentContour.BoundingRectangle.Left, currentContour.BoundingRectangle.Width);
                        Point[] contourPoints = currentContour.ToArray(); //put Seq<Point> to Point[], ToList() is also available ?
                        Console.Out.WriteLine(contourPoints[1]);
                        horizontalContours.Add(contourPoints);
                        equation += "-";
                        Console.Out.WriteLine("After digit recognize, Recognized Digit is : - ");
                        //equation += x;
                        //iks++;
                        sortedEquation.Add(xcentroid, "-");
                    }
                    #endregion

                    #region  Numbers and addition sign
                    else  if (currentContour.BoundingRectangle.Height > 50  )
                    {
                        Console.Out.WriteLine("Digit");
                        CvInvoke.cvDrawContours(temp, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        temp.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 0), 1);
                        boundingBox.Add(currentContour.BoundingRectangle);
                        Point[] contourPoints = currentContour.ToArray(); //put Seq<Point> to Point[], ToList() is also available ?
                        digitContours.Add(contourPoints);
                        float xcentroid = xCentroid(currentContour.BoundingRectangle.Left, currentContour.BoundingRectangle.Width);
                        convexity = (currentContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).Area) / (currentContour.BoundingRectangle.Height * currentContour.BoundingRectangle.Width);
                        int length = currentContour.BoundingRectangle.Right - currentContour.BoundingRectangle.Left + 50;
                        int width = currentContour.BoundingRectangle.Bottom - currentContour.BoundingRectangle.Top + 70;
                        Console.Out.WriteLine(length);
                        Console.Out.WriteLine(width);
                        segmented = new Image<Gray, byte>(length, width);
                        for (int i = currentContour.BoundingRectangle.Top-15   , k = 20; i <= currentContour.BoundingRectangle.Bottom+15; i++, k++)
                        {
                            for (int j = currentContour.BoundingRectangle.Left-5, l = 20; j <= currentContour.BoundingRectangle.Right+5; j++, l++)
                            {
                                segmented.Data[k, l, 0] = gray.Data[i, j, 0];

                            }
                        }
                        
                        imageBox2.Image = segmented;
                        //Image<Gray, Byte> resizedImage = resize(segmented.ToBitmap());
                        //imageBox2.Image = resizedImage;
                        segmented = segmented.Resize(28, 28, Emgu.CV.CvEnum.INTER.CV_INTER_NN, false);           
                        segmentedImageList.Add(segmented);
                        //later
                        segmented = segmented.ThresholdBinary(new Gray(200), new Gray(255));
                        Matrix<float> picture = new Matrix<float>(1, 784);
                        int count = 0;
                        for (int i = 0; i < 28; i++)
                        {
                            for (int j = 0; j < 28; j++)
                            {
                                picture.Data[0, count] = segmented.Data[i, j, 0];
                                count++;
                            }
                        }
                        segmented1.Image = segmented;
                        segmented = thinning(segmented);
                        string x;
                        x = digitRecognize(segmented);
                        Console.Out.WriteLine("After digit recognize, Recognized Digit is : " + x);
                        equation += x;
                        iks++;
                        sortedEquation.Add(xcentroid, x);
                        
                    }
                    #endregion

                    

                    #region dots
                    else if (currentContour.BoundingRectangle.Height < 10 && currentContour.BoundingRectangle.Width < 10 && currentContour.BoundingRectangle.Height > 5 && currentContour.BoundingRectangle.Width > 5)
                    {
                        Console.Out.WriteLine("Dot");
                        CvInvoke.cvDrawContours(temp, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        temp.Draw(currentContour.BoundingRectangle, new Bgr(255, 0, 255), 1);
                        //segmented_image.Draw(currentContour.BoundingRectangle, new Bgr(255, 0, 255), 1);
                        //boundingBox.Add(currentContour.BoundingRectangle);
                        Point[] contourPoints = currentContour.ToArray(); //put Seq<Point> to Point[], ToList() is also available ?
                        Console.Out.WriteLine(contourPoints[1]);
                        float xcentroid = xCentroid(currentContour.BoundingRectangle.Left, currentContour.BoundingRectangle.Width);
                        dotContours.Add(contourPoints);
                        equation += "*";
                        Console.Out.WriteLine("After digit recognize, Recognized Digit is : - ");
                        //equation += x;
                        //iks++;
                        sortedEquation.Add(xcentroid, "*");
                    }
                    #endregion

                    #region Vertical sign
                    else if (currentContour.BoundingRectangle.Width < 15 && currentContour.BoundingRectangle.Width > 3 && currentContour.BoundingRectangle.Height > 20)
                    {
                        Console.Out.WriteLine("Vertical");
                        CvInvoke.cvDrawContours(temp, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        temp.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 255), 1);
                        //boundingBox.Add(currentContour.BoundingRectangle);
                        Point[] contourPoints = currentContour.ToArray(); //put Seq<Point> to Point[], ToList() is also available ?
                        Console.Out.WriteLine(contourPoints[1]);
                        verticalContours.Add(contourPoints);
                    }
                    #endregion
                }
            }
            #endregion
                
            imageBox1.Image = temp;
            Console.Out.WriteLine(equation);
            foreach (float i in sortedEquation.Keys)
            {
                displayEquation.AppendText(sortedEquation[i]);
                Console.Out.WriteLine(sortedEquation[i]);
            }
            //Console.Out.WriteLine(sortedEquation.Keys); ;
            Console.Out.WriteLine("End of program");
            }// end of Form_load Function



        #region Calculate X centroid of bounding box
        private float xCentroid(float xCoordinate, float length)
        {
            float x1 = xCoordinate;
            float x2 = xCoordinate + length;
            float centroid = ((x1 + x2) / 2);
            //MessageBox.Show("centroid - " + centroid);
            return centroid;
        }
        #endregion

        #region read images - Commented as this was used in svm model function itself
        //void readPictures()
        //{
        //    Image<Gray,Byte> image1 = new Image<Gray,Byte>(@"C:\Users\SONY\Desktop\Sem5\IA\project\pictures\three.jpg");
        //    Image<Gray, Byte> image2 = new Image<Gray, Byte>(@"C:\Users\SONY\Desktop\Sem5\IA\project\pictures\nine.jpg");
        //    Matrix<float> picture1 = new Matrix<float>(1, 784);
        //    Matrix<float> picture2 = new Matrix<float>(1, 784);
        //    int count = 0;
        //    for (int i = 0; i < 28; i++)
        //    {
        //        for (int j = 0; j < 28; j++)
        //        {
        //            picture1.Data[0, count] = image1.Data[i, j, 0];
        //            count++;
        //        }
        //    }
        //    for (int i = 0; i < 28; i++)
        //    {
        //        for (int j = 0; j < 28; j++)
        //        {
        //            if (image1.Data[i, j, 0] == 255)
        //                Console.Write(".");
        //            else
        //                Console.Write("/");

        //        }
        //        Console.WriteLine();
        //    }

        //    count = 0;
        //    for (int i = 0; i < 28; i++)
        //    {
        //        for (int j = 0; j < 28; j++)
        //        {
        //            picture2.Data[0, count] = image2.Data[i, j, 0];
        //            count++;
        //        }
        //    }
        //    for (int i = 0; i < 28; i++)
        //    {
        //        for (int j = 0; j < 28; j++)
        //        {
        //            if (image2.Data[i, j, 0] == 255)
        //                Console.Write(".");
        //            else
        //                Console.Write("/");
        //        }
        //        Console.WriteLine();
        //    }

        //}
        #endregion

        #region Thinning Algorithm

        private Image<Gray,Byte> thinning(Image<Gray,Byte> image)
            {
                segmented1.Image = image;
              image = image.Dilate(1);
              segmented2.Image = image;
              Image<Gray,Byte> thinned = thinningGuoHall(image);
              Console.WriteLine("------------------------------------------end-----------------------------------------");      
              segmented3.Image = thinned;
              Image<Gray, Byte> final = thinned.Dilate(1);
              //Image<Gray, Byte> final = thinned;
              segmented4.Image = final;
                return final;
            }



            Image<Gray, Byte> thinningGuoHallIteration(Image<Gray, Byte> image1, int iter)
            {
    
                Image<Gray,Byte> image2 = new Image<Gray,byte>(image1.Size);
                for (int i = 0; i < image1.Height; i++)
                 {
                    for (int j = 0; j < image1.Width; j++)
                    {
                        image2.Data[i, j, 0] = 0;
                    }
                 }
                for (int i = 1; i < image1.Height-1; i++)
                {
                    for (int j = 1; j < image1.Width-1; j++)
                    {
                        //Console.WriteLine(i + " " + j);
                        int p2 = image1.Data[i-1, j,0];
                        int p3 = image1.Data[i-1, j+1,0];
                        int p4 = image1.Data[i, j+1,0];
                        int p5 = image1.Data[i+1, j+1,0];
                        int p6 = image1.Data[i+1, j,0];
                        int p7 = image1.Data[i+1, j-1,0];
                        int p8 = image1.Data[i, j-1,0]; 
                        int p9 = image1.Data[i-1, j-1,0];

                        int C  = (~p2 & (p3 | p4)) + (~p4 & (p5 | p6)) +
                                 (~p6 & (p7 | p8)) + (~p8 & (p9 | p2));
                        int N1 = (p9 | p2) + (p3 | p4) + (p5 | p6) + (p7 | p8);
                        int N2 = (p2 | p3) + (p4 | p5) + (p6 | p7) + (p8 | p9);
                        int N  = N1 < N2 ? N1 : N2;
                        int m  = iter == 0 ? ((p6 | p7 | ~p9) & p8) : ((p2 | p3 | ~p5) & p4);
                        //Console.WriteLine(C);
                        if (C == 1 && (N >= 2 && N <= 3) & m == 0)
                            image2.Data[i,j,0] = 1;
                    }
                }

                //im &= ~marker;
                for (int i = 0; i < image1.Height; i++)
                {
                     for (int j = 0; j < image1.Width; j++)
                     {
                        image1.Data[i, j, 0] = (byte)((image1.Data[i,j,0])&(~(image2.Data[i,j,0])));
                     }
                }
                
                //Console.WriteLine("-----------------------------------------------------------------------------------");
                return image1;
}

        Image<Gray, Byte> thinningGuoHall(Image<Gray, Byte> image1)
        {
        for (int i = 0; i < image1.Height; i++)
        {
            for (int j = 0; j < image1.Width; j++)
            {
                image1.Data[i, j, 0] = (byte)((image1.Data[i, j, 0])/255);
            }
        }
        
    
        Image<Gray,Byte> prev = new Image<Gray,byte>(image1.Size);
        Image<Gray,Byte> diff = new Image<Gray,byte>(image1.Size);

            do {
                Console.WriteLine("start:"+CvInvoke.cvCountNonZero(diff));
                image1 = thinningGuoHallIteration(image1, 0);
                image1 = thinningGuoHallIteration(image1, 1);
                diff = image1.AbsDiff(prev);
                //cv::absdiff(im, prev, diff);
                image1.CopyTo(prev);
                Console.WriteLine("end:"+CvInvoke.cvCountNonZero(diff));
            } while (CvInvoke.cvCountNonZero(diff) > 1000);
           
            for (int i = 0; i < image1.Height; i++)
            {
                for (int j = 0; j < image1.Width; j++)
                {
                    image1.Data[i, j, 0] = (byte)((image1.Data[i, j, 0]) * 255);
                }
            }
            return image1;

}


            #endregion


        #region SVM function
        private string digitRecognize(Image<Gray, Byte> image1)
        {
            Console.WriteLine("Begin");
            #region making FileStream - Commented because test data was used already for training and now we are usig saved trained model
            //FileStream ifsLabels = new FileStream(@"C:\Users\SONY\Desktop\Sem5\IA\project\MNIST\train-labels.idx1-ubyte", FileMode.Open); // test labels
            //FileStream ifsImages = new FileStream(@"C:\Users\SONY\Desktop\Sem5\IA\project\MNIST\train-images.idx3-ubyte", FileMode.Open); // test images
            //FileStream ifstest = new FileStream(@"C:\Users\SONY\Desktop\Sem5\IA\project\MNIST\t10k-images.idx3-ubyte", FileMode.Open);
            //BinaryReader brLabels = new BinaryReader(ifsLabels);
            //BinaryReader brtest = new BinaryReader(ifstest);
            //BinaryReader brImages = new BinaryReader(ifsImages);
            #endregion

            #region discarding making FileStream - Commented because test data was used already for training and now we are usig saved trained model
            //int magic1 = brImages.ReadInt32(); // discard in IMAGE TRAINING SET
            //int numImages = brImages.ReadInt32();
            //int numRows = brImages.ReadInt32();
            //int numCols = brImages.ReadInt32();
            //int magic3 = brtest.ReadInt32(); // discard in IMAGE TRAINING SET
            //int numImage3 = brtest.ReadInt32();
            //int numRow3 = brtest.ReadInt32();
            //int numCol3 = brtest.ReadInt32();
            //int magic2 = brLabels.ReadInt32();//discard in LABEL TRAINIing SET
            //int numLabels = brLabels.ReadInt32();
            #endregion
            
            #region Making a matrix from the image data
            Matrix<float> picture1 = new Matrix<float>(1, 784);
            Matrix<float> picture2 = new Matrix<float>(1, 784);
            int count = 0;
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    picture1.Data[0, count] = image1.Data[i, j, 0];
                    count++;
                }
            }
            #endregion

            #region creatingtraidataclass - Commented right now as we are loading the saved model
            //Matrix<float> traindata = new Matrix<float>(60000, 784);
            //Matrix<float> test = new Matrix<float>(1, 784);

            //Matrix<float> trainclasses = new Matrix<float>(60000, 1);
            //Console.WriteLine(traindata.Size);
            //Console.WriteLine(trainclasses.Size);

            //for (int i = 0; i < traindata.Rows; i++)
            //{

            //    int index = 0;

            //    for (int j = 0; j < 28; j++)
            //    {
            //        for (int k = 0; k < 28; k++)
            //        {
            //            byte b = brImages.ReadByte();

            //            traindata.Data[i, index] = b;
            //            index++;
            //        }
            //    }
            //    //Console.WriteLine(traindata.Data[i, 0]);
            //}

            //for (int i = 0; i < trainclasses.Rows; i++)
            //{


            //    trainclasses.Data[i, 0] = brLabels.ReadByte();
            //}
            #endregion

            #region SVM prediction
            float response = -1;
            using (SVM model = new SVM())
            {
                SVMParams p = new SVMParams();
                p.KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR;
                p.Gamma = 1;
                p.SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC;
                p.C = 1;
                p.TermCrit = new MCvTermCriteria(100, 0.00001);
                //MessageBox.Show("I am here before train");                
                try
                {
                    //bool trained = model.TrainAuto(traindata, trainclasses, null, null, p.MCvSVMParams, 10);
                    //model.Save(@"C:\Users\SONY\Desktop\Sem5\IA\project\classifier.xml");
                    //MessageBox.Show(trained.ToString());
                    model.Load(@"C:\Users\SONY\Desktop\Sem5\IA\project\classifier.xml");
                    
                    response = model.Predict(picture1);
                    //MessageBox.Show(response.ToString());
                }
                catch (Exception e)
                {
                
                   // MessageBox.Show(e.Message);
                
                }
                
            }
            #endregion

            #region FileStream Variables Close - Commented because test data was used already for training and now we are usig saved trained model
            //ifsLabels.Close();
            //ifsImages.Close();
            //ifstest.Close();
            #endregion

            #region Check for difference between + and 4.. If + then return +
            if (response == 4 || response == 6 || response == 2 || response == 5 || response == 7 || response == 9)
            {
                //MessageBox.Show("response = "+ response + "! Can be + , So We will check");
                int whitepixel = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (image1.Data[i, j, 0] == 255)
                            whitepixel++;
                    }
                }
                if (whitepixel < 3)
                {
                  //  MessageBox.Show("It indeed is a +");
                    return "+";
                }
            }
            if (response == 1)
            {
                if (convexity == 0)
                {
                    return "1";
                }
                else
                {
                    int whitepixel = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (image1.Data[i, j, 0] == 255)
                                whitepixel++;
                        }
                    }
                    if (whitepixel < 3)
                    {
                    //    MessageBox.Show("THIS IS +");
                        return "+";
                    }
                    else
                    {
                        return response.ToString();
                    }


                }

            }

            #endregion

            return response.ToString();

        }

        #endregion

        private void calculate_Click(object sender, EventArgs e)
        {
            FinalEquation = displayEquation.Text;
            Console.Out.WriteLine(FinalEquation);
            //get the user passed string
            
            //pass string in the evaluation object declaration.
            Expression z = new Expression(FinalEquation);
            //command to evaluate the value of the **************string expression
            var result = z.Evaluate();
            Double results = Convert.ToDouble(result.ToString());
            Console.Out.WriteLine("Result = "+results);
            textBox1.Text = results.ToString(); 
        }
    }
 }

