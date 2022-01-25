using BLL.Service;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoService.Controllers
{
    public class PackageController : ApiController
    {
        PackageManagement pkg = new PackageManagement();

        [Route("api/GetAllPackage")]
        [HttpGet]
        public IEnumerable<PackageDTO> GetAllPackage() => pkg.GetAllPackage();

        [Route("api/GetPackage/{packName}")]
        [HttpGet]
        public PackageDTO GetPackage(string packName) => pkg.GetPackage(packName);

        [Route("api/AddPackage")]
        [HttpPost]
        public bool GetAllPackage([FromBody] PackageDTO p) => pkg.AddPackage(p);

        [Route("api/DeletePackage/{packName}")]
        [HttpDelete]
        public bool DeletePackage(string packName) => pkg.DeletePackage(packName);

    }
}
