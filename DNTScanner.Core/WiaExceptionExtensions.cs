using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DNTScanner.Core
{
    /// <summary>
    /// WiaException Extensions
    /// </summary>
    public static class WiaExceptionExtensions
    {
        private static readonly IDictionary<int, string> _messageMappings = new Dictionary<int, string>
        {
            { -2145320959, "An unknown error has occurred with the Microsoft Windows Image Acquisition (WIA) device."},
            { -2145320958, "Paper is jammed in the scanner's document feeder."},
            { -2145320957, "The user requested a scan and there are no documents left in the document feeder." },
            { -2145320956, "An unspecified problem occurred with the scanner's document feeder." },
            { -2145320955, "The WIA device is not online."},
            { -2145320954, "The WIA device is busy."},
            { -2145320953, "The WIA device is warming up."},
            { -2145320952, "An unspecified error has occurred with the WIA device that requires user intervention. The user should ensure that the device is turned on, online, and any cables are properly connected."},
            { -2145320951, "The WIA device was deleted. It can no longer be accessed." },
            { -2145320950, "An unspecified error occurred during an attempted communication with the WIA device." },
            { -2145320949, "The device does not support this command." },
            { -2145320948, "There is an incorrect setting on the WIA device." },
            { -2145320947, "The scanner head is locked." },
            { -2145320946 , "The device driver threw an exception." },
            { -2145320945 , "The response from the driver is invalid." },
            { -2145320944 , "The scanner's cover is opened." },
            { -2145320943 , "The scanner's lamp is off." },
            { -2145320942 , "Destination Error."},
            { -2145320941 , "Network Reservation Failed."},
            { -2145320939 , "No WIA device of the selected type is available."},
            { -2145320853 , "COM arrays are 1-based arrays instead of zero-based arrays." }
        };

        /// <summary>
        /// Returns a friendly COM-Error message.
        /// </summary>
        public static string GetComErrorMessage(this COMException ex)
        {
            return _messageMappings.TryGetValue(ex.ErrorCode, out var message) ? message : ex.Message;
        }
    }
}