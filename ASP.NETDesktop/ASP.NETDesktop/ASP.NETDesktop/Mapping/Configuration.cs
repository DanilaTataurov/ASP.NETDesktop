using AutoMapper;

namespace ASP.NETDesktop.Mapping {
    public class Configuration {
        public static MapperConfiguration Create() {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new ApiModelsMapping());
            });
        }
    }
}
