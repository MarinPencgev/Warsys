using System;
using System.Collections.Generic;
using System.Text;

namespace Warsys.Services
{
    public interface ISeederService
    {
        bool SeedFromExcel(string filePath);   
        bool SeedTest();
    }
}
