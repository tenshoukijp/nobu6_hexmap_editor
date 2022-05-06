using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

partial class FieldViewerForm : Form
{

    Image[] baseImages;
    Image[] edgeImages;
    Image[] rideImages;
    Image[] roleImages;

    const String ImageDir = @"FieldEditor\images\";

    void RegistImages()
    {
        baseImages = new Image[256];
        edgeImages = new Image[256];
        rideImages = new Image[256];
        roleImages = new Image[32];
        {
            String[] files = {
                "00.png",
                "01.png",
                "02.png",
                "03.png",
                "04.png",
                "0F.png",
                "10.png",
                "11.png",
                "12.png",
                "13.png",
                "14.png",
                "15.png",
                "16.png",
                "17.png",
                "18.png",
                "19.png",
                "1A.png",
                "1B.png",
                "1C.png",
                "1D.png",
                "1E.png",
                "1F.png",
                "20.png",
                "21.png",
                "22.png",
                "23.png",
                "24.png",
                "25.png",
                "26.png",
                "27.png",
                "28.png",
                "29.png",
                "2A.png",
                "2B.png",
                "2C.png",
                "2D.png",
                "2E.png",
                "30.png",
                "31.png",
                "32.png",
                "33.png",
                "34.png",
                "35.png",
                "36.png",
                "37.png",
                "38.png",
                "39.png",
                "3A.png",
                "3B.png",
                "3C.png",
                "3D.png",
                "3E.png",
                "3F.png",
                "40.png",
                "41.png",
                "42.png",
                "43.png",
                "44.png",
                "45.png",
                "46.png",
                "47.png",
                "48.png",
                "49.png",
                "4A.png",
                "4B.png",
                "4C.png",
                "4D.png",
                "4E.png",
                "4F.png",
                "50.png",
                "51.png",
                "52.png",
                "53.png",
                "54.png",
                "55.png",
                "56.png",
                "57.png",
                "58.png",
                "59.png",
                "5A.png",
                "5B.png",
                "5C.png",
                "5D.png",
                "5E.png",
                "5F.png",
                "60.png",
                "61.png",
                "62.png",
                "63.png",
                "64.png",
                "65.png",
                "66.png",
                "67.png",
                "68.png",
                "69.png",
                "6A.png",
                "6B.png",
                "6C.png",
                "6D.png",
                "6F.png",
                "FF.png",
           };

            /*
            var r = new Regex(@"(..)\.png");
            String[] files = Directory.GetFiles(ImageDir + @"base\", "??.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {

                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        baseImages[iTipsID] = Image.FromFile(files[i]);
                        baseImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
          */

            var r = new Regex(@"(..)\.png");
            for (int i = 0; i < files.Length; i++)
            {

                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        baseImages[iTipsID] = new Bitmap(GetType(), "FieldEditor.images.base." + files[i]);
                        baseImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception /*e*/)
                    {
                    }
                }
            }

        }

        {
            String[] files = {
                "70.png",
                "71.png",
                "72.png",
                "73.png",
                "74.png",
                "75.png",
                "76.png",
                "77.png",
                "78.png",
                "79.png",
                "7A.png",
                "7B.png",
                "7C.png",
                "7D.png",
                "7E.png",
                "7F.png",
                "80.png",
                "81.png",
                "82.png",
                "83.png",
                "84.png",
                "85.png",
                "86.png",
                "87.png",
                "88.png",
                "89.png",
                "8A.png",
                "8B.png",
                "8C.png",
                "8D.png",
                "8E.png",
                "8F.png",
                "90.png",
                "91.png",
                "92.png",
                "93.png",
                "94.png",
                "95.png",
                "96.png",
                "97.png",
                "98.png",
                "99.png",
                "9A.png",
                "9B.png",
                "9C.png",
                "9D.png",
                "9E.png",
                "9F.png",
                "A0.png",
                "A2.png",
                "A3.png",
                "A4.png",
                "A7.png",
                "A8.png",
                "A9.png",
                "AB.png",
                "AC.png",
                "AD.png",
                "AE.png",
                "AF.png",
                "B0.png",
                "B1.png",
                "B2.png",
                "B3.png",
                "B4.png",
                "B5.png",
                "B6.png",
                "B7.png",
                "B8.png",
                "B9.png",
                "BA.png",
                "BB.png",
                "BC.png",
                "BD.png",
                "BE.png",
                "BF.png",
                "C1.png",
                "C2.png",
                "C3.png",
                "C4.png",
                "C5.png",
                "C6.png",
                "C8.png",
                "C9.png",
                "CA.png",
                "CB.png",
                "CC.png",
                "CD.png",
                "CE.png",
                "CF.png",
                "D0.png",
                "D1.png",
                "D2.png",
                "D3.png",
                "D4.png",
                "D7.png",
                "D8.png",
                "DA.png",
                "DB.png",
                "DC.png",
                "DD.png",
                "DE.png",
                "DF.png",
                "E0.png",
                "E1.png",
                "E2.png",
                "E3.png",
                "E4.png",
                "E5.png",
                "E6.png",
                "E7.png",
                "FF.png",
            };

            /*
            String[] files = Directory.GetFiles(ImageDir + @"edge\", "??.png", SearchOption.TopDirectoryOnly);
            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        edgeImages[iTipsID] = Image.FromFile(files[i]);
                        edgeImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            */
            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        edgeImages[iTipsID] =  new Bitmap(GetType(), "FieldEditor.images.edge." + files[i]);
                        edgeImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception /*e*/)
                    {
                    }
                }
            }

        }

        {
            String[] files = {
                "E8.png",
                "E9.png",
                "EA.png",
                "EB.png",
                "EC.png",
                "ED.png",
                "EE.png",
                "FF.png"
            };

            /*
            String[] files = Directory.GetFiles(ImageDir + @"ride\", "??.png", SearchOption.TopDirectoryOnly);
            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        rideImages[iTipsID] = Image.FromFile(files[i]);
                        rideImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            */

            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        rideImages[iTipsID] = new Bitmap(GetType(), "FieldEditor.images.ride." + files[i]);
                        rideImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception /*e*/)
                    {
                    }
                }
            }

        }

        {
            String[] files = {
                "00.png",
                "01.png",
                "02.png",
                "03.png",
                "04.png",
                "05.png",
                "06.png",
                "07.png",
                "08.png",
                "09.png",
                "0A.png",
                "0B.png",
                "0C.png"
            };
            /*
            String[] files = Directory.GetFiles(ImageDir + @"\role\", "??.png", SearchOption.TopDirectoryOnly);
            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        roleImages[iTipsID] = Image.FromFile(files[i]);
                        roleImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            */

            var r = new Regex(@"(..)\.png");

            for (int i = 0; i < files.Length; i++)
            {
                var m = r.Match(files[i]);

                if (m.Groups[1].Value.Length > 0)
                {
                    int iTipsID = -1;
                    try
                    {
                        iTipsID = Convert.ToInt32((string)m.Groups[1].Value, 16);
                        roleImages[iTipsID] = new Bitmap(GetType(), "FieldEditor.images.role." + files[i]);
                        roleImages[iTipsID].Tag = iTipsID;

                    }
                    catch (Exception /*e*/)
                    {
                    }
                }
            }

        }
    }
}