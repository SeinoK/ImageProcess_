using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rename_
{
    class Program
    {
        //用来更改某个制定目录里面的文件夹指定的名字
        static void Main(string[] args)
        {
            String path = @"C:\详情-修改";
            Dictionary<string, long> FileList = new Dictionary<string, long> { };
            FileList = GetFile(path, FileList);

            //foreach (var fil in FileList)
            //{
            //    Console.WriteLine(fil);
            //}

            Console.ReadKey();


            //DirectoryInfo folder = new DirectoryInfo(path);

            //foreach (FileInfo file in folder.GetFiles("*.txt"))
            //{
            //    Console.WriteLine(file.FullName);
            //}



        }
        public static Dictionary<string, long> GetFile(string path, Dictionary<string, long> FileList)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                //int size = Convert.ToInt32(f.Length);  
                long size = f.Length;
                FileList.Add(f.FullName, size);//添加文件路径到列表中
                //Console.WriteLine(f.Name);

                if (f.FullName.Contains("主图1") || f.FullName.Contains("主图2") || f.FullName.Contains("主图3") || f.FullName.Contains("主图4") || f.FullName.Contains("主图5"))
                {

                    f.MoveTo(f.FullName.Replace("主图1", "1"));
                    f.MoveTo(f.FullName.Replace("主图2", "2"));
                    f.MoveTo(f.FullName.Replace("主图3", "3"));
                    f.MoveTo(f.FullName.Replace("主图4", "4"));
                    f.MoveTo(f.FullName.Replace("主图5", "5"));

                    Console.WriteLine(f.FullName);
                    //Console.WriteLine(f.Name);
                }




            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo d in dii)
            {
                GetFile(d.FullName, FileList);
                //Console.WriteLine(d.FullName);
            }
            return FileList;
        }
        //public static Dictionary<string, long> getFileName(string path, Dictionary<string, long> FileList)
        //{
        //    return FileList;
        //}


    }
}
