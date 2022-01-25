using DAL.EF;
using Models.DataViewModels;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PackageRep : IPackageRep
    {
        private bool disposedValue;
        AutoDBContext db = new AutoDBContext();

        public bool AddPackage(PackageDTO package)
        {
            if (db.Packages.Any(i => i.Pack_Name.Equals(package.Pack_Name)))
                throw new Exception($"Package with name {package.Pack_Name} already exists!");

            Package udt = new Package
            {
                Pack_Name = package.Pack_Name,
                Description = package.Description,
                Price = package.Price
            };
            db.Packages.Add(udt);
            db.SaveChanges();
            return true;

        }

        public IEnumerable<PackageDTO> GetAllPackage()
        {
            return db.Packages.Select(i => new PackageDTO
            {
                Pack_Name = i.Pack_Name,
                Description = i.Description,
                Price = i.Price
            });
        }

        public PackageDTO GetPackage(string pack_Name)
        {
            return db.Packages.Where(i => i.Pack_Name.Equals(pack_Name)).Select(i => new PackageDTO
            {
                Pack_Name = i.Pack_Name,
                Description = i.Description,
                Price = i.Price
            }).FirstOrDefault();
        }

        public bool DeletePackage(string pack_Name)
        {
            if (!db.Packages.Any(i => i.Pack_Name.Equals(pack_Name)))
                throw new Exception($"Package with name {pack_Name} not found!");

            var udt = db.Packages.Where(i => i.Pack_Name.Equals(pack_Name)).First();
            db.Packages.Remove(udt);
            db.SaveChanges();
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PackageRep()
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
