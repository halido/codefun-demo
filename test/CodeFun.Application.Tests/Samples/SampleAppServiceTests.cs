﻿using System.Threading.Tasks;
using Xunit;

namespace CodeFun.Samples
{
    /* This is just an example test class.
     * Normally, you don't test code of the modules you are using
     * (like IIdentityUserAppService here).
     * Only test your own application services.
     */
    public class SampleAppServiceTests : CodeFunApplicationTestBase
    {

        public SampleAppServiceTests()
        {
        }

        [Fact]
        public async Task Initial_Data_Should_Contain_Admin_User()
        {
            //Act
        
        }
    }
}