using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyRegister.Interfaces
{
    interface IClosable
    {
        /// <summary>
        /// Becsukás kezdeményezése
        /// </summary>
        /// <returns>true - h asikerült</returns>
        bool Close();

        /// <summary>
        /// Becsukható-e az ablak?
        /// </summary>
        /// <returns>true - ha igen</returns>
        bool IsCloseEnabled();
    }
}
