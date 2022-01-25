using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contract
{
    public interface IPackageManagement : IDisposable
    {
        IEnumerable<PackageDTO> GetAllPackage();
        PackageDTO GetPackage(string packName);
        bool AddPackage(PackageDTO package);
    }
}
