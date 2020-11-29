using System.IO;
using System.Threading.Tasks;

namespace MysteryLocation
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
