using GrpcSharedProtoFile;
using Mapster;
using Model.Entities;

namespace GrpcService.Mapper
{
    public static class MapsterConfig
    {
        public static void RegisterMapping()
        {
            TypeAdapterConfig<UserRequest, User>.NewConfig()
                .Map(x => x.DateOfBirth, y => DateTime.Parse(y.BirthDate))
                .Map(x => x.FirstName, y => y.FirstName)
                .Map(x => x.LastName, y => y.LastName)
                .Map(x => x.NationalCode, y => y.NationalCode);

            TypeAdapterConfig<User, UserRequest>.NewConfig()
                .Map(x => x.BirthDate, y => y.DateOfBirth.ToString())
                .Map(x => x.FirstName, y => y.FirstName)
                .Map(x => x.LastName, y => y.LastName)
                .Map(x => x.NationalCode, y => y.NationalCode);
        }
    }
}