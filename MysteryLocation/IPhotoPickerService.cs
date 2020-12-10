using System.IO;
using System.Threading.Tasks;
/// <summary>
/// Interface from: https://docs.microsoft.com/en-us/samples/xamarin/xamarin-forms-samples/dependencyservice/
/// </summary>
namespace MysteryLocation
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
