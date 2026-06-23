using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CRUDTests.CustomFactory
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IPersonGetterService> PersonGetterServiceMock { get; } = new();
        public Mock<IPersonAdderService> PersonAdderServiceMock { get; } = new();
        public Mock<IPersonUpdaterService> PersonUpdaterServiceMock { get; } = new();
        public Mock<IPersonDeleterService> PersonDeleterServiceMock { get; } = new();
        public Mock<IPersonSearcherService> PersonSearcherServiceMock { get; } = new();
        public Mock<IPersonSorterService> PersonSorterServiceMock { get; } = new();

        public Mock<ICountryGetterService> CountryServicesMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPersonGetterService>();
                services.RemoveAll<IPersonAdderService>();
                services.RemoveAll<IPersonUpdaterService>();
                services.RemoveAll<IPersonDeleterService>();
                services.RemoveAll<IPersonSearcherService>();
                services.RemoveAll<IPersonSorterService>();
                services.RemoveAll<ICountryGetterService>();

                services.AddScoped(_ => PersonGetterServiceMock.Object);
                services.AddScoped(_ => PersonAdderServiceMock.Object);
                services.AddScoped(_ => PersonUpdaterServiceMock.Object);
                services.AddScoped(_ => PersonDeleterServiceMock.Object);
                services.AddScoped(_ => PersonSearcherServiceMock.Object);
                services.AddScoped(_ => PersonSorterServiceMock.Object);

                services.AddScoped(_ => CountryServicesMock.Object);
            });
        }
    }
}