using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stLib_CS {
    namespace Generic {
        public static class ListHelper {
            public static bool IsIn<T>( List<T> list, T item ) {
                for( int i = 0; i < list.Count; ++i ) {
                    if( list[i].Equals( item ) ) {
                        return true;
                    }
                }
                return false;
            }
            public static bool RemoveIn<T>( List<T> list, T item ) {
                for( int i = 0; i < list.Count; ++i ) {
                    if( list[i].Equals( item ) ) {
                        list.RemoveAt( i );
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
