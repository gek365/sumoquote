using System.IO;

namespace BestDealWebApp.Services.Service3
{
    public interface IXmlMapper<T, TT>
    {
        public string ToString(T request);

        public TT FromStream(Stream stream);
    }
}
