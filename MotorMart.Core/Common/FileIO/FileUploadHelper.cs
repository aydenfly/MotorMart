using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// Upload a file to a specified directory
/// </summary>

namespace MotorMart.Core.Common.FileIO
{
    public class FileUploadHelper
    {
        private HttpPostedFileBase _inputFile;

        public HttpPostedFileBase InputFile
        {
            get { return _inputFile; }
            set { _inputFile = value; }
        }

        private string _uploadDir;

        public string UploadDir
        {
            get { return _uploadDir; }
            set { _uploadDir = value; }
        }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = safeFilename(value); }
        }

        private int _fileSize;

        public int FileSize
        {
            get { return _fileSize; }
        }

        private string _contentType;

        public string ContentType
        {
            get { return _contentType; }
        }

        private string _fileContent;

        public string FileStream
        {
            get { return _fileContent; }
        }

        private ArrayList _messages;

        public ArrayList Messages
        {
            get { return _messages; }
        }

        private bool _uploadComplete;

        public bool UploadComplete
        {
            get { return _uploadComplete; }
        }

        public bool ContainsFileToUpload { get; set; }

        public FileUploadHelper()
        {
            _messages = new ArrayList();
            _uploadComplete = false;
        }

        public void uploadFile(string _uploadDir, HttpPostedFileBase _inputFile)
        {
            this._inputFile = _inputFile;
            this._uploadDir = _uploadDir;

            if (_uploadDir == "")
            {
                _messages.Add("Specify a directory");
                return;
            }

            if (_inputFile != null && _inputFile.FileName != string.Empty)
            {
                ContainsFileToUpload = true;
                this._contentType = _inputFile.ContentType;
                this._fileSize = _inputFile.ContentLength;

                if (_fileName == null || _fileName == "")
                {
                    _fileName = safeFilename(_inputFile.FileName);
                }
                else
                {
                    string[] parts = _inputFile.FileName.Split(Convert.ToChar("."));
                    string extension = parts[parts.Length - 1];

                    // Check filename for extension
                    if (!_fileName.EndsWith(extension)) _fileName += "." + extension;
                }


                try
                {
                    _inputFile.SaveAs(_uploadDir + _fileName);
                    _messages.Add("File " + _uploadDir + _fileName + " uploaded successfully");
                    _uploadComplete = true;
                }
                catch (Exception e)
                {
                    _messages.Add("Error saving <b>" + _uploadDir + _fileName + e.ToString());
                }
            }
            else
            {
                ContainsFileToUpload = false;
                _messages.Add("No file to upload");
            }
        }

        public void getFileContent()
        {
            if (_inputFile != null)
            {
                try
                {
                    const int BUFFER_SIZE = 255;
                    int nBytesRead = 0;
                    Byte[] Buffer = new Byte[BUFFER_SIZE];
                    StringBuilder strUploadedContent = new StringBuilder();

                    Stream theStream = _inputFile.InputStream;
                    nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);

                    while (0 != nBytesRead)
                    {
                        strUploadedContent.Append(Encoding.ASCII.GetString(Buffer, 0, nBytesRead));
                        nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);
                    }

                    this._fileContent = strUploadedContent.ToString();
                }
                catch (Exception e)
                {
                    _messages.Add("Error saving " + _inputFile.FileName + e.ToString());
                }
            }
        }

        private string safeFilename(string _fileName)
        {
            return _fileName;
        }
    }
}
