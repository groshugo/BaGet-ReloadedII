using System.Threading;
using System.Threading.Tasks;

namespace BaGet.Core
{
    public interface IAuthenticationService
    {
        bool Authenticate(string apiKey, string packageKey, CancellationToken cancellationToken);
    }
}
