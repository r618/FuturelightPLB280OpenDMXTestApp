//
// Futurelight PLB 280 + OpenDMX test app
//
// Copyright (c) 2017 Martin Cvengros
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

// DMX protocol reference from http://media.steinigke.de/documents_t/51838966-DMX-1.00-de-en_00092323.xls

using System;

namespace r618
{
    static class FuturelightPLB280OpenDMX
    {
        const byte CHANNEL_PAN_VALUE        = 1;
        const byte CHANNEL_TILT_VALUE       = 2;
        const byte CHANNEL_SHUTTER_VALUE    = 5;
        const byte CHANNEL_DIMMER_VALUE     = 6;
        const byte CHANNEL_COLOR_VALUE      = 8;
        const byte CHANNEL_PRISM_VALUE      = 15;
        const byte CHANNEL_FOCUS_VALUE      = 18;
        const byte CHANNEL_ZOOM_VALUE       = 19;
        const byte CHANNEL_RESET            = 20;

        public static void Pan(byte value)
        {
            var channel = CHANNEL_PAN_VALUE;
            Console.WriteLine(string.Format("PAN [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Tilt(byte value)
        {
            var channel = CHANNEL_TILT_VALUE;
            Console.WriteLine(string.Format("TILT [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Shutter_Open()
        {
            var channel = CHANNEL_SHUTTER_VALUE;
            byte value = 255;
            Console.WriteLine(string.Format("SHUTTER OPEN [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Shutter_Close()
        {
            var channel = CHANNEL_SHUTTER_VALUE;
            byte value = 0;
            Console.WriteLine(string.Format("SHUTTER CLOSE [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Dimmer(byte value)
        {
            var channel = CHANNEL_DIMMER_VALUE;
            Console.WriteLine(string.Format("DIMMER [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Color(byte value)
        {
            var channel = CHANNEL_COLOR_VALUE;
            Console.WriteLine(string.Format("COLOR [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Prism_Open()
        {
            var channel = CHANNEL_PRISM_VALUE;
            byte value = 0;                     // 0 - 63
            Console.WriteLine(string.Format("PRISM OPEN [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Prism_6Facet()
        {
            var channel = CHANNEL_PRISM_VALUE;
            byte value = 64;                    // 64 - 127
            Console.WriteLine(string.Format("PRISM 6 FACET [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Prism_8Facet()
        {
            var channel = CHANNEL_PRISM_VALUE;
            byte value = 128;                   // 128 - 191
            Console.WriteLine(string.Format("PRISM 8 FACET [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Prism_Frost()
        {
            var channel = CHANNEL_PRISM_VALUE;
            byte value = 255;                   // 192 - 255
            Console.WriteLine(string.Format("PRISM FROST [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Focus(byte value)
        {
            var channel = CHANNEL_FOCUS_VALUE;
            Console.WriteLine(string.Format("FOCUS [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Zoom(byte value)
        {
            var channel = CHANNEL_ZOOM_VALUE;
            Console.WriteLine(string.Format("ZOOM [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Lamp_ON()
        {
            var channel = CHANNEL_RESET;
            byte value = 87;                    // range [ 80 - 87 ]
            Console.WriteLine(string.Format("LAMP ON [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }

        public static void Lamp_OFF()
        {
            var channel = CHANNEL_RESET;
            byte value = 79;                    // range [ 72 - 79 ]
            Console.WriteLine(string.Format("LAMP ON [CHANNEL:{0}] [VALUE:{1}]", channel, value));
            OpenDMX.SetDmxValue(channel, value);
        }
    }
}
