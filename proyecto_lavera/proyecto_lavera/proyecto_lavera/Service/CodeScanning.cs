using System.Threading.Tasks;

namespace proyecto_lavera.Service
{
    public interface ICodeScanning
    {
        Task<string> ScanAsync();
    }
}