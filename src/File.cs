using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;

namespace stLib_CS {
    namespace File {
        public static class FileHelper {
            public static List<FileInfo> GetFiles( string path ) {
            List<FileInfo> files = null;

            switch( stLib_CS.File.FileHelper.IsFileOrDirectory( path ) ) {
                case stLib_CS.File.FileHelper.eFileOrFolder.IsFile:
                    files = new List<FileInfo>();
                    files.Add( new FileInfo( path ) );
                    break;
                case stLib_CS.File.FileHelper.eFileOrFolder.IsFolder:
                    DirectoryInfo root = new DirectoryInfo( path );
                    files = new List<FileInfo>( root.GetFiles() );
                    break;
                case stLib_CS.File.FileHelper.eFileOrFolder.Neither:
                    return null;
                    break;
            }
            return files;
        }
            public enum eFileOrFolder {
                IsFile, IsFolder, Neither
            }
            public static eFileOrFolder IsFileOrDirectory( string path ) {
                if( System.IO.File.Exists( path ) ) {
                    return eFileOrFolder.IsFile;
                }
                if( System.IO.Directory.Exists( path ) ) {
                    return eFileOrFolder.IsFolder;
                }
                return eFileOrFolder.Neither;
            }
            public static bool IsPicture( string fileName ) {
                string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
                char[] separtor = { '|' };
                string[] tempFileds = StringSplit( strFilter, separtor );
                foreach( string str in tempFileds ) {
                    if( str.ToUpper() == fileName.Substring( fileName.LastIndexOf( "." ), fileName.Length - fileName.LastIndexOf( "." ) ).ToUpper() ) { return true; }
                }
                return false;
            }
            // 通过字符串，分隔符返回string[]数组 
            public static string[] StringSplit( string s, char[] separtor ) {
                string[] tempFileds = s.Trim().Split( separtor );
                return tempFileds;
            }
        }
        public class Ini {
            public string m_inipath;
            //声明API函数

            [DllImport( "kernel32" )]
            private static extern long WritePrivateProfileString( string section, string key, string val, string filePath );
            [DllImport( "kernel32" )]
            private static extern int GetPrivateProfileString( string section, string key, string def, StringBuilder retVal, int size, string filePath );
            /// <summary> 
            /// 构造方法 
            /// </summary> 
            /// <param name="INIPath">文件路径</param> 
            public Ini( string INIPath ) {
                m_inipath = INIPath;
            }

            public Ini() { }
            /// <summary> 
            /// 写入INI文件 
            /// </summary> 
            /// <param name="Section">项目名称(如 [TypeName] )</param> 
            /// <param name="Key">键</param> 
            /// <param name="Value">值</param> 
            public void WriteValue( string Section, string Key, string Value ) {
                WritePrivateProfileString( Section, Key, Value, this.m_inipath );
            }
            /// <summary> 
            /// 读出INI文件 
            /// </summary> 
            /// <param name="Section">项目名称(如 [TypeName] )</param> 
            /// <param name="Key">键</param> 
            public string ReadValue( string Section, string Key ) {
                StringBuilder temp = new StringBuilder( 500 );
                int i = GetPrivateProfileString( Section, Key, "", temp, 500, this.m_inipath );
                return temp.ToString();
            }
            /// <summary> 
            /// 验证文件是否存在 
            /// </summary> 
            /// <returns>布尔值</returns> 
            public bool Exist() {
                return System.IO.File.Exists( m_inipath );
            }
        }

        public static class CopyHelper {
            public static string Copy( string srcPath, string destPath, bool isFolder ) {
                string targetFolderPath = destPath;
                if( isFolder ) {
                    targetFolderPath += srcPath.Substring( srcPath.LastIndexOf( "\\" ) );
                    CopyDirectory( srcPath, targetFolderPath );
                } else {
                    CopyFile( srcPath, destPath );
                }
                return targetFolderPath;
            }
            public static void CopyDirectory( string srcPath, string destPath ) {
                try {
                    if( !Directory.Exists( destPath ) ) {
                        Directory.CreateDirectory( destPath );
                    }
                    DirectoryInfo dir = new DirectoryInfo( srcPath );
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                    foreach( FileSystemInfo i in fileinfo ) {
                        if( i is DirectoryInfo )     //判断是否文件夹
                        {
                            if( !Directory.Exists( destPath + "\\" + i.Name ) ) {
                                Directory.CreateDirectory( destPath + "\\" + i.Name );   //目标目录下不存在此文件夹即创建子文件夹
                            }
                            CopyDirectory( i.FullName, destPath + "\\" + i.Name );    //递归调用复制子文件夹
                        } else {
                            System.IO.File.Copy( i.FullName, destPath + "\\" + i.Name, true );      //不是文件夹即复制文件，true表示可以覆盖同名文件
                        }
                    }
                } catch( Exception e ) {
                    throw e;
                }
            }
            public static void CopyFile( string srcPath, string destPath ) {
                System.IO.File.Copy( srcPath, destPath, true );      //不是文件夹即复制文件，true表示可以覆盖同名文件                
            }
            public static void CopyDirectoryByAPI( string srcPath, string destPath ) {
                try {
                    DirectoryInfo dir = new DirectoryInfo( srcPath );
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                    foreach( FileSystemInfo i in fileinfo ) {
                        if( i is DirectoryInfo )     //判断是否文件夹
                        {
                            if( !Directory.Exists( destPath + "\\" + i.Name ) ) {
                                Directory.CreateDirectory( destPath + "\\" + i.Name );   //目标目录下不存在此文件夹即创建子文件夹
                            }
                            CopyDirectory( i.FullName, destPath + "\\" + i.Name );    //递归调用复制子文件夹
                        } else {
                            CopyFileByAPI( i.FullName, destPath + "\\" + i.Name );      //不是文件夹即复制文件，true表示可以覆盖同名文件
                        }
                    }
                } catch( Exception e ) {
                    throw e;
                }
            }
            private const int FO_COPY = 0x0002;
            private const int FOF_ALLOWUNDO = 0x00044;
            //显示进度条  0x00044 // 不显示一个进度对话框 0x0100 显示进度对话框单不显示进度条  0x0002显示进度条和对话框  
            private const int FOF_SILENT = 0x0002;//0x0100;  
                                                  //  
            [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 0 )]
            public struct SHFILEOPSTRUCT {
                public IntPtr hwnd;
                [MarshalAs(UnmanagedType.U4)]
                public int wFunc;
                public string pFrom;
                public string pTo;
                public short fFlags;
                [MarshalAs(UnmanagedType.Bool)]
                public bool fAnyOperationsAborted;
                public IntPtr hNameMappings;
                public string lpszProgressTitle;
            }
            [DllImport( "shell32.dll", CharSet = CharSet.Auto )]
            static extern int SHFileOperation( ref SHFILEOPSTRUCT FileOp );
            public static bool CopyFileByAPI( string strSource, string strTarget ) {
                SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
                fileop.wFunc = FO_COPY;
                fileop.pFrom = strSource;
                fileop.lpszProgressTitle = "process";
                fileop.pTo = strTarget;
                //fileop.fFlags = FOF_ALLOWUNDO;  
                fileop.fFlags = FOF_SILENT;
                return SHFileOperation( ref fileop ) == 0;
            }
        }
    }
}
