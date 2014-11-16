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
    public class FileInputUploadHelper
    {
        private FileUpload _inputFile;

        public FileUpload InputFile
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

        //private string _fileStream;

        //public string FileStream
        //{
        //    get { return _fileStream; }
        //}

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

        public FileInputUploadHelper()
        {
            _messages = new ArrayList();
            _uploadComplete = false;
        }

        public void uploadFile(string _uploadDir, FileUpload _inputFile)
        {
            this._inputFile = _inputFile;
            this._uploadDir = _uploadDir;

            if (_uploadDir == "")
            {
                _messages.Add("Specify a directory");
                return;
            }

            if (_inputFile.PostedFile != null && _inputFile.FileName != string.Empty)
            {
                this._contentType = _inputFile.PostedFile.ContentType;
                this._fileSize = _inputFile.PostedFile.ContentLength;

                if (_fileName == null || _fileName == "")
                {
                    _fileName = safeFilename(_inputFile.PostedFile.FileName);
                }
                else
                {
                    string[] parts = _inputFile.PostedFile.FileName.Split(Convert.ToChar("."));
                    string extension = parts[parts.Length - 1];

                    // Check filename for extension
                    if (!_fileName.EndsWith(extension)) _fileName += "." + extension;
                }

                try
                {
                    _inputFile.PostedFile.SaveAs(_uploadDir + _fileName);
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
                _messages.Add("No file to upload");
            }
        }        

        private string safeFilename(string _fileName)
        {
            return _fileName;
        }
    }
}
