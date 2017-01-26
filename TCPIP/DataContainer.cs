using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPIP
{
    /// <summary>
    /// The Container of data and a datatype which is used for data-bytearray conversion.  
    /// </summary>
    class DataContainer
    {
        /// <summary>
        /// Data in arbitary form.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Type of contained data.
        /// </summary>
        public string DataType { get; set; }

        public DataContainer(string dataType, object data)
        {
            DataType = dataType;
            Data = data;
        }
    }
}
