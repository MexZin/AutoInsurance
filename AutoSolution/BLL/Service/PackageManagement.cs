using BLL.Contract;
using Models.DataViewModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PackageManagement : IPackageManagement
    {
        private bool disposedValue;
        PackageRep pkg = new PackageRep();

        public bool AddPackage(PackageDTO package)
        {
            try
            {
                pkg.AddPackage(package);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<PackageDTO> GetAllPackage()
        {
            return pkg.GetAllPackage();
        }

        public PackageDTO GetPackage(string packName)
        {
            return pkg.GetPackage(packName);
        }

        public bool DeletePackage(string pack_Name)
        {
            return pkg.DeletePackage(pack_Name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    pkg.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PackageManagement()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
