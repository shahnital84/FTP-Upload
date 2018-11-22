using System;
using System.IO;
using System.Net;
using System.Text;

namespace FTPUpload
{
    public class Program
    {
        static string url = "Enter your server details";
        static string username = @"Enter your details";
        static string password = "Enter your details";

        public static void Main()
        {
            string output = "";

            // Get the object used to communicate with the server.
            Console.WriteLine("Please input file name.");
            string filename = Console.ReadLine(); //create file on FTP
            if(filename == null || filename == "")
            {
                filename = "testfile.txt";
            }
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + filename + ".txt");

            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(username, password);

            // Copy the contents of the file to the request stream.
            byte[] fileContents;

            using (StreamReader sourceStream = new StreamReader("testfile.txt")) //create testfile.txt on debug folder of your application with data 
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
            Console.WriteLine("Please press any key to exit.");
            Console.ReadLine();
        }
    }
}
