using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPackageRep:IDisposable
    {
        IEnumerable<PackageDTO> GetAllPackage();
        PackageDTO GetPackage(string pack_Name);
        bool AddPackage(PackageDTO package);
        bool DeletePackage(string pack_Name);
    }
}
