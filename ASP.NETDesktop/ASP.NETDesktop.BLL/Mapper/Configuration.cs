using AutoMapper;

namespace ASP.NETDesktop.BLL.Mapper {
    public class Configuration {
        public static MapperConfiguration Create() {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new DataModelsMapping());
            });
        }
    }
}
