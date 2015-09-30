// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using DDDSkeleton.ApplicationServices.Services;
using DDDSkeleton.Domain;
using DDDSkeleton.Infrascructure.Common.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;
using DDDSkeleton.Repository.Memory;
using DDDSkeleton.Repository.Memory.Database;
using DDDSkeleton.Repository.Memory.Repositories;
using StructureMap;
using StructureMap.Graph;

namespace DDDSkeleton.WebService.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.AssemblyContainingType<IScreeningRepository>();
                    scan.AssemblyContainingType<ScreeningRepository>();
                    scan.AssemblyContainingType<IScreeningService>();
                    scan.AssemblyContainingType<BusinessRule>();
                    scan.WithDefaultConventions();
                });
                x.For<IUnitOfWork>().Use<InMemoryUnitOfWork>();
                x.For<IObjectContextFactory>().Use<LazySingletonObjectContextFactory>();
            });

            return ObjectFactory.Container;
        }
    }
}