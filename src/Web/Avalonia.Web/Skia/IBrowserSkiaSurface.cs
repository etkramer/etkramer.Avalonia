namespace Avalonia.Web.Skia
{
    internal interface IBrowserSkiaSurface
    {
        public PixelSize Size { get; set; }

        public double Scaling { get; set; }
    }
}
