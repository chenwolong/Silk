using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace WeChat.Common
{
    public class HttpDownLoad
    {
        private long fileLength;
        private long downLength;//已经下载文件大小，外面想用就改成公共属性
        private static bool stopDown;
        public HttpDownLoad()
        {
            fileLength = 0;
            downLength = 0;
            stopDown = false;
            //
            //   TODO:   在此处添加构造函数逻辑
            //
        }

        ///   <summary>
        ///   文件下载
        ///   </summary>
        ///   <param   name="url">连接</param>
        ///   <param   name="fileName">本地保存文件名</param>
        public void httpDownFile(string url, string fileName)
        {
            stopDown = false;
            Stream str = null, fs = null;
            try
            {
                //获取下载文件长度
                fileLength = getDownLength(url);
                downLength = 0;
                if (fileLength > 0)
                {
                    WebClient DownFile = new WebClient();
                    str = DownFile.OpenRead(url);
                    //判断并建立文件
                    if (createFile(fileName))
                    {
                        byte[] mbyte = new byte[1024];
                        int readL = str.Read(mbyte, 0, 1024);
                        fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                        //读取流
                        while (readL != 0)
                        {
                            if (stopDown)
                                break;
                            downLength += readL;//已经下载大小
                            fs.Write(mbyte, 0, readL);//写文件
                            readL = str.Read(mbyte, 0, 1024);//读流
                            //progressBar.Value = (int)(downLength * 100 / fileLength);
                            //label.Text = progressBar.Value.ToString() + "%";
                            //System.Windows.Forms.Application.DoEvents();
                        }
                        str.Close();
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (str != null)
                    str.Close();
                if (fs != null)
                    fs.Close();
                
            }
        }
        ///   <summary>
        ///   文件下载
        ///   </summary>
        ///   <param   name="url">连接</param>
        ///   <param   name="fileName">本地保存文件名</param>
        public void httpDownFile2(string url, string fileName)
        {
            try
            {
                WebClient DownFile = new WebClient();
                DownFile.DownloadFile(url, fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///   <summary>
        ///   获取下载文件大小
        ///   </summary>
        ///   <param   name="url">连接</param>
        ///   <returns>文件长度</returns>
        private long getDownLength(string url)
        {
            try
            {
                WebRequest wrq = WebRequest.Create(url);
                WebResponse wrp = (WebResponse)wrq.GetResponse();
                wrp.Close();
                return wrp.ContentLength;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
                
            }
        }
        ///   <summary>
        ///   建立文件(文件如已经存在，删除重建)
        ///   </summary>
        ///   <param   name="fileName">文件全名(包括保存目录)</param>
        ///   <returns></returns>
        private bool createFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                Stream s = File.Create(fileName);
                s.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
               
            }
        }
        public void downClose()
        {
            stopDown = true;
        }


    }
}

