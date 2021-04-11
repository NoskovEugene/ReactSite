import SettingsStore from "./SettingsStore";
import AuthenticationStore from "./AuthenticationStore";
import MetadataStore from "./MetadataStore";


export default class RootStore{

    public settings!: SettingsStore;

    public authenticationStore!: AuthenticationStore;

    public metadataStore!: MetadataStore;

    public constructor() {
        this.metadataStore = new MetadataStore();
    }

    public static build = async () => {
        const instance = new RootStore();
        instance.settings = await SettingsStore.build();
        instance.authenticationStore = await AuthenticationStore.build();
        return instance;
    }
}