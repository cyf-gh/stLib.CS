using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ICSharpCode.SharpZipLib.dll;

namespace stLib_CS {
    namespace Compress {
        class Zip {
            //public static bool ZipFiles( string dirPath, string zipFilePath ) {
            //    try {
            //        string[] filenames = Directory.GetFiles( dirPath );
            //        using( ZipOutputStream s = new ZipOutputStream( File.Create( zipFilePath ) ) ) {
            //            s.SetLevel( 9 );//0-9 值越大压缩率越高
            //            byte[] buffer = new byte[4096];
            //            foreach( string file in filenames ) {
            //                ZipEntry entry = new ZipEntry( Path.GetFileName( file ) );
            //                entry.DateTime = DateTime.Now;
            //                s.PutNextEntry( entry );
            //                using( FileStream fs = File.OpenRead( file ) ) {
            //                    int sourceBytes;
            //                    do {
            //                        sourceBytes = fs.Read( buffer, 0, buffer.Length );
            //                        s.Write( buffer, 0, sourceBytes );
            //                    } while( sourceBytes > 0 );
            //                }
            //            }
            //            s.Finish();
            //            s.Close();
            //        }
            //    } catch( Exception ex ) {

            //        return false;
            //    }
            //    return true;
            //}
        }
    }
}
