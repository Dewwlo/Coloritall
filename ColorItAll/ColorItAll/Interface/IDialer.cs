using System.Threading.Tasks;

namespace ColorItAll.Interface
{
    public interface IDialer
    {
        Task<bool> DialAsync(string number);
    }
}
