using Coypu;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.IO;

namespace Zukini.UI
{
    // TODO: remove once added to Zukini
    public static class ElementScopeExtension
    {
        public static Rectangle Rectangle(this ElementScope element)
        {
            var native = (IWebElement)element.Native;
            return new Rectangle(native.Location, native.Size);
        }

        /// <summary>
        /// Takes a screenshot of Element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="browser"></param>
        /// <param name="shiftPoint"></param>
        /// <returns>Bitmap</returns> in memory image
        public static Bitmap ScreenShot(this ElementScope element, Driver browser,
            Point shiftPoint = new Point())
        {
            var scrn = browser.Native as ITakesScreenshot;
            if (scrn == null) throw new NotSupportedException("Driver does not support screenshots");
            var shot = scrn.GetScreenshot().AsByteArray;

            Bitmap bmp;
            using (var ms = new MemoryStream(shot))
            {
                bmp = new Bitmap(ms);
            }

            var wbElmnt = element.AsWebElement();
            int width = wbElmnt.Size.Width;
            int height = wbElmnt.Size.Height;
            Point point = wbElmnt.Location;

            var rect = new Rectangle(point.X + shiftPoint.X,
                                        point.Y + shiftPoint.Y,
                                        width, height);
            if (rect.Width < 0) throw new Exception("Wrong width.");
            if (rect.Height < 0) throw new Exception("Wrong height.");
            Bitmap img = new Bitmap(rect.Width, rect.Height);
            using (Graphics gph = Graphics.FromImage(img))
            {
                gph.DrawImage(bmp,
                    new Rectangle(0, 0, img.Width, img.Height), rect, GraphicsUnit.Pixel);
            }

            return img;
        }

        /// <summary>
        /// Converts to a native selenium IWebElement.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Thrown when element is not IWebElement</exception>
        public static IWebElement AsWebElement(this ElementScope element)
        {
            var el = element.Native as IWebElement;
            if (el == null)
            {
                throw new ArgumentException("Element is not IWebElement");
            }
            return el;
        }

        /// <summary>
        /// Clears input field.
        /// </summary>
        /// <param name="element"></param>
        public static void Clear(this ElementScope element)
        {
            element.AsWebElement().Clear();
        }
    }
}
