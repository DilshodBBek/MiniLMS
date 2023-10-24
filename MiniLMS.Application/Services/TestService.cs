using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Application.Services;
public class TestService : ITestService
{

    public Guid Random { get; set; } = Guid.NewGuid();

}
