using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIA;

namespace Intelligob.Utiles
{
    public class Scanner
    {
        private readonly DeviceInfo _deviceInfo;

        public Scanner(DeviceInfo deviceInfo)
        {
            this._deviceInfo = deviceInfo;
        }        

        /*public const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
	    public const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";*/
	    public const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
	    /*public const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
	    public const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";*/

        public ImageFile Scan()
        {
            // Connect to the device
            var device = this._deviceInfo.Connect();

            // Start the scan
            var item = device.Items[1];
            var imageFile = (ImageFile)item.Transfer(wiaFormatJPEG);

            // Return the imageFile
            return imageFile;
        }

        public String Nombre
        {
            get
            {
                return this._deviceInfo.Properties["Name"].get_Value().ToString();
            }
        }

        public override string ToString()
        {
            return this._deviceInfo.Properties["Name"].get_Value().ToString();
        }
    }
}
